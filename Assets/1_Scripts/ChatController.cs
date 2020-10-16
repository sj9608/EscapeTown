using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ChatController : MonoBehaviour
{
    public Text chatText;
    public Text chatCharacter;
    public int SceneNumber;
    string loadFile;

    public int _intro;
    public int _body;
    public int _conclus;
    public int enemiesDic;

    XmlNodeList nodes;
    void Start()
    {
        SceneNumber = GameManager.Instance.curSceneNum;
        loadFile = string.Format("Chat_{0}", SceneNumber);
        LoadXml();
        StartCoroutine(PrintText(_intro, _body));
    }

    private void Update() {
        if(enemiesDic <= 0)
        {
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
            Debug.Log("Name :: " + node.SelectSingleNode("code").InnerText);
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

        // foreach(XmlNode node in nodes)
        // {
        //     sentence = node.SelectSingleNode("sentence").InnerText;
        //     speaker = node.SelectSingleNode("speaker").InnerText;
        //     yield return StartCoroutine(NormalChat(speaker, sentence));
        //     yield return new WaitForSeconds(3); 
        // }

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

        // foreach(XmlNode node in nodes)
        // {
        //     sentence = node.SelectSingleNode("sentence").InnerText;
        //     speaker = node.SelectSingleNode("speaker").InnerText;
        //     yield return StartCoroutine(NormalChat(speaker, sentence));
        //     yield return new WaitForSeconds(3); 
        // }
        for(XmlNode node = nodes[body]; node != nodes[conclus]; node=nodes[++body])
        {
            sentence = node.SelectSingleNode("sentence").InnerText;
            speaker = node.SelectSingleNode("speaker").InnerText;
            yield return StartCoroutine(NormalChat(speaker, sentence));
            yield return new WaitForSeconds(3); 
        }
    }
}
