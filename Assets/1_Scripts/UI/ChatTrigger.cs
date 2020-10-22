using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ObjectData objectData;

    public int enemyCnt = 0;

    Collider _collider;

    private void Start() {
        _collider = GetComponent<Collider>();
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(other.tag);
        if(other.tag == "Player" && enemyCnt > 0)
        {   // 전투 전
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            _collider.enabled = false;
            yield return new WaitForSeconds(4);
            this.gameObject.SetActive(false);
        }
        else if(other.tag == "Player" && enemyCnt == 0)
        {   // 전투 후
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            _collider.enabled = false;
            yield return new WaitForSeconds(4);
            this.gameObject.SetActive(false);
        }
    }
}
