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

    XmlNodeList nodes;
    void Start()
    {
        loadFile = string.Format("Chat_{0}", SceneNumber);
        LoadXml();
        StartCoroutine(PrintText());
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

    IEnumerator PrintText()
    {
        string sentence = "";
        string speaker = "";

        foreach(XmlNode node in nodes)
        {
            sentence = node.SelectSingleNode("sentence").InnerText;
            speaker = node.SelectSingleNode("speaker").InnerText;
            yield return StartCoroutine(NormalChat(speaker, sentence));
            yield return new WaitForSeconds(3); 
        }
    }
}
