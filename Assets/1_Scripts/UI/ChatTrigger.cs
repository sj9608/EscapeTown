using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ObjectData objectData;                                   // 채팅 정보(id, 대화 내용)를 가진 클래스
    public bool isAbleToChat;                                       // 전투 완료 상태에 따른 독백 가능 여부

    Collider _collider;                                             // 콜라이더 진입 시 독백 출력

    private void Start() {
        _collider = GetComponent<Collider>();
    }

    private void Update() {
        if(GameManager.Instance.enemiesDic.Count == 0)
        {
            isAbleToChat = true;
        }
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        Debug.Log(other.tag);
        if(other.tag == "Player" && isAbleToChat == true)
        {
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            _collider.enabled = false;
            yield return new WaitForSeconds(4);
            this.gameObject.SetActive(false);
        }
    }
}
