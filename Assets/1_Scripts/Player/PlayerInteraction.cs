using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // 공격 범위
    protected float attackDistance;
    // 적군 레이어 마스크
    private LayerMask enemyLayer;
    // 아이템 레이어 마스크
    private LayerMask interactionLayer;
    // 상호작용 거리
    float interactionDistance;

    // 
    Collider target = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        interactionLayer = LayerMask.GetMask("Interaction");
        attackDistance = 10;
        interactionDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isLoading)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetInteraction();
        }
    }
    protected void GetInteraction()
    {
        // f키를 눌렀을 때 주민이 레이캐스트 거리 안에 있으면 주민의 대화 내용을 출력 하고 대화정보를 비활성화 시킴 
        // 아이템 삭제
        // 타겟의 이름으로 item의 속성 판정 Potion, Magazine

                                                                                                                // Raycast 반경 수정
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactionDistance, transform.forward, 0, interactionLayer);
        if (hits.Length > 0)
        {
            target = hits[0].collider;
            switch (target.tag)
            {
                case "NPC":
                    ObjectData obj = hits[0].transform.GetComponent<ObjectData>();
                    if (obj != null && obj.enabled == true)
                    {
                        StartCoroutine(ChatManager.Instance.PrintNormalChat(obj.id, obj.isNpc));
                        obj.enabled = false;
                    }
                    break;
                case "Item":
                    GameManager.Instance.GetItem(target);
                    break;
                default:
                    break;
            }
        }
        else
        {
            target = null;
        }
    }
}
