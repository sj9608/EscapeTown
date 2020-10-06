using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    // 적군 레이어 마스크
    private LayerMask enemyLayer;
    // 아이템 레이어 마스크
    private LayerMask itemLayer;
    // Rigidbody
    private Rigidbody playerRigidbody;
    // HP
    // 공격력
    // 이동속도

    // 스테미너
    private float playerStamina;
    // 점프중
    private bool isJump;
    // 점프파워
    private float jumpPower;
    Collider target = null;
    // 대기()
    // 걷기()
    // 공격()
    // 죽음()

    // 점프()
    // 뛰기()
    // 앉기()
    // 앉아 걷기()
    // 대쉬()
    // 무기교체()

    // 점프()
    // 뛰기()
    // 앉기()
    // 앉아 걷기()
    // 대쉬()
    // 무기교체()

    // Start is called before the first frame update
    void Start()
    {
        HP = 3;
        AP = 1;
        playerRigidbody = GetComponent<Rigidbody>();
        enemyLayer = LayerMask.GetMask("Enemy");
        itemLayer = LayerMask.GetMask("Item");
        moveSpeed = 50f;
        jumpPower = 100f;
        attackDistance = 2;
    }
    private void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetItem();
        }
    }
    protected override void Attack()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackDistance, transform.forward, 0, enemyLayer);
        Debug.Log(hits.Length + "개");
        if (hits.Length > 0)
        {
            target = hits[0].collider;
        }
        else
        {
            target = null;
        }
        GameManager.Instance.Attack(target, AP);
        //foreach (var hit in hits)
        //{
        //    if (hit.collider != null)
        //    {

        //        if (target != null)
        //        {
        //            Debug.Log("collider : enemy------------" + target.name);
        //            isEnemy = true;
        //        }
        //    }
        //}
        //if (isEnemy)
        //{
        //    Debug.Log(gameObject.name + "이(가) " + target.name + "을(를) 공격 했다.");
        //    isEnemy = false;
        //}
        //else
        //{
        //    Debug.Log(gameObject.name + "이(가) 그냥 공격 했다.");
        //    target = null;
        //}
    }
    protected void GetItem()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackDistance, transform.forward, 0, itemLayer);
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
    void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 movePosition = new Vector3(inputX, 0, inputZ) * moveSpeed * Time.deltaTime;

        playerRigidbody.position = movePosition + transform.position;

        //Vector3 velocity = new Vector3(inputX, 0, inputZ) * moveSpeed;

        //playerRigidbody.velocity = velocity;

        //Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
        //playerRigidbody.velocity = vector3;
    }
    void PlayerJump()
    {
        if (isJump)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;

            //playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // animator.SetBool("IsJumping", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0f)
        {
            isJump = false;
        }
    }
}
