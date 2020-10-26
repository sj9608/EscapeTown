using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class ChatManager : SingletonBase<ChatManager>
{
    // 1. 게임 인트로에서 전부 다 불러와서 XML파일을 딕셔너리로 파싱
    // 2. 싱글톤으로 만들어서 
    // 3. 오브젝트와 NPC에게 할당된 대사를 할 수 있게 함.
    
    // 1. XML 파일에 들어 있는 대화 내용을 읽는다.
    // 2. 읽으면서 인게임에 출력된 대사는 ChatNote에 저장된다.

    // ChatTrigger
    // 3. 낮 스테이지 : NPC와 하는 대사는 NPC에게 할당
    // 4. 밤 스테이지 : 플레이어 독백은 전투 전과 전투 후로 나뉨

    // 버그 수정 요구 사항 : ChatObject 관련 수정
    // 1. 대사 나오는 도중 씬 전환시 대사가 남아있는 현상
    // 2. 대사가 전부 출력되지 않으면 대화수첩으로 들어가지 않는 것 
    
    // 대사를 저장하는 딕셔너리
    public Dictionary<int, string> talkData = new Dictionary<int, string>();                           // talk Data 키 번호에 따른 대사
    public Dictionary<int, string> talkCharacterData = new Dictionary<int, string>();                         // 키 번호에 따른 화자

    // 대사 출력 텍스트
    public Text chatCharacter;                                                // UI Text 화자
    public Text chatText;                                                     // 화자가 말하는 내용
    
    // 오브젝트 풀링 용 배열
    public int[] chatArray = new int[100];
    public int chatNumber; 

    TextAsset textAsset;

    public void Awake()
    {
        GenerateData();
        // chatCharacter.text = "";
        // chatText.text = "";
    }

    public void GenerateData()
    {   // ChatFile 을 열어 딕셔너리에 대사와 화자를 기입하는 메서드
        string loadFile = "ChatFile";

        if(textAsset != null) return;
        
        chatNumber = 0;
        //Debug.Log("this is null state");
        textAsset = (TextAsset)Resources.Load(loadFile);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        XmlNodeList nodes = xmlDoc.SelectNodes("root/chat");

        foreach(XmlNode node in nodes)
        {
            //Debug.Log(node.SelectSingleNode("sentence").InnerText);
            talkData.Add(Convert.ToInt32(node.SelectSingleNode("code").InnerText), node.SelectSingleNode("sentence").InnerText);
            talkCharacterData.Add(Convert.ToInt32(node.SelectSingleNode("code").InnerText), node.SelectSingleNode("speaker").InnerText);
        }
    }

    public IEnumerator PrintNormalChat(int id, bool isNpc)
    {   // 대화 수첩에 저장
        chatArray[chatNumber] = id;
        chatNumber++;

        string narrator = talkCharacterData[id];
        string narration = talkData[id];
        
        // string writerText = "";

        if(isNpc == true) chatCharacter.text = narrator;

        // 대사가 한 글자씩 출력되는 연출
        // for(int i=0; i<narration.Length; i++)
        // {
        //     writerText += narration[i];
        //     chatText.text = writerText;
        //     yield return null;
        // }

        chatText.text = talkData[id];
        
        yield return new WaitForSeconds(3);
        chatCharacter.text = "";
        chatText.text = "";

        // poolingObjectQueue.Enqueue(CreateNewText(id));
    }
}
