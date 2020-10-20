using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChatManager : MonoBehaviour
{
    // 1. 게임 인트로에서 전부 다 불러와서 XML파일을 딕셔너리로 파싱
    // 2. 싱글톤으로 만들어서 
    // 3. 오브젝트와 NPC에게 할당된 대사를 할 수 있게 함.
    
    // 1. XML 파일에 들어 있는 대화 내용을 읽는다.
    // 2. 읽으면서 인게임에 출력된 대사는 ChatNote에 저장된다.

    // ChatTrigger
    // 3. 낮 스테이지 : NPC와 하는 대사는 NPC에게 할당
    // 4. 밤 스테이지 : 플레이어 독백은 전투 전과 전투 후로 나뉨

    private static ChatManager instance;                                      // singleton
    public static ChatManager Instance
    {
        get{ return instance; }
    }
    
    // 대사를 저장하는 딕셔너리
    public static Dictionary<int, string> talkData;                           // talk Data 키 번호에 따른 대사
    public Dictionary<int, string> talkCharacterData;                         // 키 번호에 따른 화자

    // 대사 출력 텍스트
    public Text chatCharacter;                                                // UI Text 화자
    public Text chatText;                                                     // 화자가 말하는 내용
    
    // 오브젝트 풀
    static private Text poolingObjectPrefab;                                  // 풀링에 사용할 텍스트 프리팹
    public static Queue<Text> poolingObjectQueue = new Queue<Text>();
    

    public void Awake()
    {
        if (instance != null && instance != this)               // singleton
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        GenerateData();
    }

    private void Update() {
        // 대사 출력
        // if(낮 스테이지) - NPC 별로 고유의 대화 내용 출력
        if(GameManager.Instance.curSceneNum < 21 //&& GameManager.Instance.playerInteraction. == true 
                        && Input.GetKeyDown(KeyCode.F) == true) // 대화 가능한 사람이 있는지 확인
        {
            ObjectData objData = GetComponent<ObjectData>();
            StartCoroutine(PrintNormalChat(objData.id, objData.isNpc));
        }
    }

    public void GenerateData()
    {   // ChatFile 을 열어 딕셔너리에 대사와 화자를 기입하는 메서드
        string loadFile = "ChatFile";
        TextAsset textAsset = (TextAsset)Resources.Load(loadFile);
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("root/chat");

        foreach(XmlNode node in nodes)
        {
            talkData.Add(Convert.ToInt32(node.SelectSingleNode("code").InnerText), node.SelectSingleNode("sentence").InnerText);
            talkCharacterData.Add(Convert.ToInt32(node.SelectSingleNode("code").InnerText), node.SelectSingleNode("speaker").InnerText);
        }
    }

    public IEnumerator PrintNormalChat(int id, bool isNpc)
    {   // 대사가 한 글자씩 출력되는 연출
        string narrator = talkCharacterData[id];
        string narration = talkData[id];
        
        string writerText = "";

        if(isNpc == true) chatCharacter.text = narrator;
        
        for(int i=0; i<narration.Length; i++)
        {
            writerText += narration[i];
            chatText.text = writerText;
            yield return null;
        }

        chatCharacter.text = "";
        chatText.text = "";
        poolingObjectQueue.Enqueue(CreateNewText(id));
        SaveChatNote(id);
    }

    // 출력된 대사는 ChatNote에 저장
    public void SaveChatNote(int id)
    {   
        // string chatting = talkData[id];
        // chat.text = chatting;
        // Text text = Instantiate(chat, new Vector3(0, yValue, 0), Quaternion.identity);
        // text.transform.SetParent(GameObject.Find("Content").transform);
        // yValue -= 110;

        // 오브젝트 풀
        // 풀 큐 생성
        // 대화가 출력될 때마다 풀 큐에 넣음 
        // 대화 수첩을 열면 풀 큐에 넣은 것을 전부 꺼내 빌려줌
        // 대화 수첩을 닫으면 풀 큐에 다시 리턴
    }


    // Object pulling 삽입/사용/리턴 메서드
    public static Text CreateNewText(int id)
    {
        // 대사 출력 후 풀 큐에 삽입
        // content에 해당 스크립트를 할당시켜 content의 자식으로 Text가 들어오게 함
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Text>();
        newObj.text = talkData[id];
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(GameObject.Find("Content").transform);

        return newObj;
    }

    public static Text GetText()
    {
        // 오브젝트 풀이 가지고 있는 게임 오브젝트를 요청한 자에게 꺼내주는 역할
        // 모든 오브젝트를 꺼내서 빌려줘서 queue에 빌려줄 오브젝트가 없는 상태라면
        // 새로운 오브젝트를 생성해서 빌려준다

        if(poolingObjectQueue.Count > 0)
        {
            var obj = poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            Text newobj = CreateNewText(0);
            newobj.gameObject.SetActive(true);
            newobj.transform.SetParent(null);
            return newobj;
        }
    }

    public static void ReturnObject(Text obj)
    {   // 빌려준 오브젝트를 돌려받는 메서드
        // 돌려받은 오브젝트의 비활성화한 뒤 정리하는 일 처리
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(GameObject.Find("Content").transform);
        poolingObjectQueue.Enqueue(obj);
    }
}
