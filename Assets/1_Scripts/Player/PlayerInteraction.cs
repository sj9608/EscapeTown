using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    GameManager GMI;
    // 공격 범위
    protected float attackDistance;
    // 적군 레이어 마스크
    private LayerMask enemyLayer;
    // 아이템 레이어 마스크
    private LayerMask interactionLayer;
    // 상호작용 거리
    float interactionDistance;
    // 상호 작용 각도
    private float currentSearchAngle;
    Collider target = null;

    private void Awake() {
        GMI = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        interactionLayer = LayerMask.GetMask("Interaction");
        attackDistance = 10f;
        interactionDistance = 2f;
        currentSearchAngle = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GMI.isGameOver || GMI.isLoading || GMI.isInteractioning)
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
        if (hits.Length > 0 &&
            Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(hits[0].transform.position)) < currentSearchAngle / 2.0f)
        {
            target = hits[0].collider;
            switch (target.tag)
            {
                case "NPC":
                    ObjectData obj = hits[0].transform.GetComponent<ObjectData>();
                    if (obj != null && obj.enabled == true)
                    {
                        GameManager.Instance.isInteractioning = true;
                        hits[0].transform.LookAt(transform);
                        StartCoroutine(ChatManager.Instance.PrintNormalChat(obj.id, obj.isNpc));
                        obj.questionMark.SetActive(false);
                        obj.enabled = false;
                        obj.gameObject.layer = 0;
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
