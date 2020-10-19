using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ChatController : MonoBehaviour
{
    // 1. XML 파일에 들어 있는 대화 내용을 읽는다.
    // 2. 읽으면서 인게임에 출력된 대사는 ChatNote에 저장된다.
    // 3. 낮 스테이지 : NPC와 하는 대사는 NPC에게 할당
    // 4. 밤 스테이지 : 플레이어 독백은 전투 전과 전투 후로 나뉨

    public Text chatText;
    public Text chatCharacter;
    public int SceneNumber;
    string loadFile;

    public int _intro = 0;
    public int _body;
    public int _conclus;
    public int enemiesDic;

    XmlNodeList nodes;
    void Start()
    {
        SceneNumber = GameManager.Instance.curSceneNum;
        loadFile = string.Format("Chat_{0}", SceneNumber);
        LoadXml();

        // 전투 전 독백
        StartCoroutine(PrintText(_intro, _body));
    }

    private void Update() {
        if(enemiesDic <= 0)
        {   // 전투 후 독백
            StartCoroutine(PrintTextAfterFight(_body, _conclus));
        }
    }

    void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load(loadFile);
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        nodes = xmlDoc.SelectNodes("root/chat");
        
        foreach (XmlNode node in nodes)
        {
            Debug.Log("code :: " + node.SelectSingleNode("code").InnerText);
        }
    }

    IEnumerator NormalChat(string narrator, string narration)
    {
        chatCharacter.text = narrator;
        string writerText = "";

        for(int i=0; i<narration.Length; i++)
        {
            writerText += narration[i];
            chatText.text = writerText;
            yield return null;
        }
    }

    IEnumerator PrintText(int intro, int body)
    {
        string sentence = "";
        string speaker = "";

        // 노드 code 1 ~ 노드 code 1
        for(XmlNode node = nodes[intro]; node != nodes[body]; node=nodes[++intro])
        {
            sentence = node.SelectSingleNode("sentence").InnerText;
            speaker = node.SelectSingleNode("speaker").InnerText;
            yield return StartCoroutine(NormalChat(speaker, sentence));
            yield return new WaitForSeconds(3); 
        }
    }
    IEnumerator PrintTextAfterFight(int body, int conclus)
    {
        string sentence = "";
        string speaker = "";

        for(XmlNode node = nodes[body]; node != nodes[conclus]; node=nodes[++body])
        {
            sentence = node.SelectSingleNode("sentence").InnerText;
            speaker = node.SelectSingleNode("speaker").InnerText;
            yield return StartCoroutine(NormalChat(speaker, sentence));
            yield return new WaitForSeconds(3); 
        }
    }
}
