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
    // 인자로 넘길 Collider
    Collider target = null;


    // 플레이어 사운드
    AudioSource playerAudio;
    public AudioClip audioGetItem;

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

        // 사운드
        playerAudio = gameObject.AddComponent<AudioSource>();
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
    // 대화, 습득
    protected void GetInteraction()
    {
        // f키를 눌렀을 때 주민이 레이캐스트 거리 안에 있으면 주민의 대화 내용을 출력 하고 대화정보를 비활성화 시킴 
        // 아이템 삭제
        // 타겟의 이름으로 item의 속성 판정 Potion, Magazine
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactionDistance, transform.forward, 0, interactionLayer);
        if (hits.Length > 0 &&
            Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(hits[0].transform.position)) < currentSearchAngle / 2.0f)    // Raycast 각도 120도
        {
            // Raycast에 걸린 제일 가까운 Collider
            target = hits[0].collider;
            // Collider의 Tag로 대상 판단
            switch (target.tag)
            {
                // NPC
                case "NPC":
                    // NPC와 대화
                    // NPC의 ObjectData 검출
                    ObjectData obj = hits[0].transform.GetComponent<ObjectData>();
                    if (obj != null && obj.enabled == true)
                    {
                        // GameManager에 대화중 알림
                        GameManager.Instance.isInteractioning = true;
                        // NPC가 Player 바라봄
                        hits[0].transform.LookAt(transform);
                        // 대화 Coroutine
                        StartCoroutine(ChatManager.Instance.PrintNormalChat(obj.id, obj.isNpc));
                        // 마크 사라짐 // ObjectData 사용 불가 // Layer 바꿈
                        obj.questionMark.SetActive(false);
                        obj.enabled = false;
                        obj.gameObject.layer = 0;
                    }
                    break;
                case "Item":
                    // 습득 사운드
                    playerAudio.PlayOneShot(audioGetItem);
                    // 줍기
                    GameManager.Instance.GetItem(target);
                    break;
                default:
                    break;
            }
        }
        else
        {
            // Interaction target 초기화
            target = null;
        }
    }
}
