using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class ChatController : MonoBehaviour
{
    public Text chatText;
    public Text chatCharacter;

    XmlNodeList nodes;
    void Start()
    {
        LoadXml();
        StartCoroutine(PrintText());
    }

    void LoadXml()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("Chat");
        Debug.Log(textAsset);
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(textAsset.text);

        nodes = xmlDoc.SelectNodes("root/chat");
        
        foreach (XmlNode node in nodes)
        {
            Debug.Log("Name :: " + node.SelectSingleNode("code").InnerText);
            Debug.Log("Level :: " + node.SelectSingleNode("situation").InnerText);
            Debug.Log("Exp :: " + node.SelectSingleNode("sentence").InnerText);
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
        
        foreach(XmlNode node in nodes)
        {
            sentence = node.SelectSingleNode("sentence").InnerText;
            yield return StartCoroutine(NormalChat("당신", sentence));
            yield return new WaitForSeconds(3); 
        }
    }
}
