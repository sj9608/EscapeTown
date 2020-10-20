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
    private LayerMask itemLayer;
    // 상호작용 거리
    float interactionDistance;

    // 
    Collider target = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyLayer = LayerMask.GetMask("Enemy");
        itemLayer = LayerMask.GetMask("Item");
        attackDistance = 10;
        interactionDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetInteraction();
        }
    }
    protected void GetInteraction()
    {
        // 대화 추가 필요
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, interactionDistance, transform.forward, 0, itemLayer);
        if (hits.Length > 0)
        {
            target = hits[0].collider;
        }
        else
        {
            target = null;
        }
        GameManager.Instance.GetItem(target);
    }
}
