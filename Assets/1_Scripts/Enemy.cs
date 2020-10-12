using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Human
{
    private LayerMask playerLayerMask;
    public float timeBetAttack = 0.5f; // 공격 간격
    private float lastAttackTime; // 마지막 공격 시점
    public ParticleSystem hitEffect; // 피격시 재생할 파티클 효과
    // HP
    // 공격력
    // 이동속도

    // 인식 범위
    float maxDistance;
    // 대기()
    // 걷기()
    // 공격()
    // 죽음()

    // Start is called before the first frame update
    void Start()
    {
        HP = 2;
        AP = 1;
        playerLayerMask = LayerMask.GetMask("Player");
        moveSpeed = 1;
        maxDistance = 10;
        attackDistance = 2;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyRaycast();
        
    }
    void EnemyRaycast()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, maxDistance, transform.forward, 0);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                Player player = hit.collider.GetComponent<Player>();

                if (player != null)
                {
                    Debug.Log("Player 찾음!");
                    //Debug.Log("PlayereLayerMask : " + playerLayerMask.value);
                    //Debug.Log("Player Layer : " + player.gameObject.layer + "::: Player Tag : " + player.tag + " ::: Player name : " + player.name);
                    //Debug.Log("Hit Layer : " + hit.transform.gameObject.layer + "::: Hit Tag : " + hit.transform.tag + " ::: Hit name : " + hit.transform.name);
                    Vector3 toTarget = player.transform.position - transform.position;
                    Vector3 direction = toTarget.normalized;
                    if (toTarget.sqrMagnitude < Mathf.Pow(attackDistance, 2))
                    {
                        moveSpeed = 0.1f;
                    }
                    else
                    {
                        moveSpeed = 1f;
                    }
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime);
                    // https://magatron.tistory.com/56?category=874541
                    // https://flowtree.tistory.com/19
                    transform.position += direction * moveSpeed * Time.deltaTime;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Time.time >= lastAttackTime + timeBetAttack)
        {
            lastAttackTime = Time.time;
            Debug.Log(other.tag + "을 공격했다");
            GameManager.Instance.Attack(other, AP);
        }
    }
}
