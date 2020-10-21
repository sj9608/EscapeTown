using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ObjectData objectData;

    private void Start() {
        objectData = GetComponent<ObjectData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && GameManager.Instance.enemiesDic.Count > 0)
        {   // 전투 전
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            other.gameObject.SetActive(false);
        }
        else if(other.tag == "Player" && GameManager.Instance.enemiesDic.Count == 0)
        {   // 전투 후
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            other.gameObject.SetActive(false);
        }
    }
}
