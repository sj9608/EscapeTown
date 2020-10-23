using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    // 독백 트리거 심는 법
    
    // 독백 트리거는 ChatObject 로 묶여있다.
    // 대사 번호대로 Trigger (번호) 형식으로 이름 지어져 있다.
    
    // 독백 트리거 오브젝트는 ObjectData 와 ChatTrigger를 컴포넌트로 가지고 있다.
    // ObjectData / id : 대사 번호 isNpc : 주민인지 독백인지 구분
    // ChatTrigger / ObjectData 는 해당 오브젝트를 하이어라키에서 끌어다 적용하면 된다.
    // 전투 전 대사는 isAbleToChat이 true, 전투 후 대사는 isAbleToChat false

    public ObjectData objectData;                                   // 채팅 정보(id, 대화 내용)를 가진 클래스
    public bool isAbleToChat;                                       // 전투 완료 상태에 따른 독백 가능 여부

    Collider _collider;                                             // 콜라이더 진입 시 독백 출력
    GameObject enemies;

    private void Start() {
        _collider = GetComponent<Collider>();
        enemies = GameObject.Find("Enemies");
    }

    private void Update() {
        // if(GameManager.Instance.enemiesDic.Count != 0) return 0;
        
        if(enemies.transform.childCount != 0) return;    
        isAbleToChat = true;
        
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isAbleToChat == true)
        {
            StartCoroutine(ChatManager.Instance.PrintNormalChat(objectData.id, objectData.isNpc));
            _collider.enabled = false;
            yield return new WaitForSeconds(4);
            this.gameObject.SetActive(false);
        }
    }
}
