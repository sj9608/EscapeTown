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

    private static ChatManager instance;                        // singleton
    public static ChatManager Instance
    {
        get{ return instance; }
    }
    
    public Dictionary<int, string> talkData;                           // talk Data 키 번호에 따른 대사
    public Dictionary<int, string> talkCharacterData;                  // 키 번호에 따른 화자

    public Text chatCharacter;                                  // UI Text 화자
    public Text chatText;                                       // 화자가 말하는 내용
    
    int yValue = 0;                                             // 텍스트가 붙을 위치
    
    // 오브젝트 풀
    // public static ObjectPool objectPool;
    [SerializeField] Text chat;                                 // 텍스트 프리팹 사용해서 대화 수첩에 붙이는 텍스트

    
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

    private void Update() {                                     // 대사 출력
        // if(낮 스테이지) - NPC 별로 고유의 대화 내용 출력
        if(GameManager.Instance.curSceneNum < 21 //&& GameManager.Instance.playerInteraction. == true 
                        && GameManager.Instance.gameKeyInput.interaction == true) // 대화 가능한 사람이 있는지 확인
        {
            ObjectData objData = GetComponent<ObjectData>();
            StartCoroutine(PrintNormalChat(objData.id, objData.isNpc));
        }
    }

    public void GenerateData()
    {
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
    {
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


    }
}
