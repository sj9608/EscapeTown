using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    public Transform camTransform;
    // 적군 레이어 마스크
    private LayerMask enemyLayer;
    // 아이템 레이어 마스크
    private LayerMask itemLayer;
    // Rigidbody
    private Rigidbody playerRigidbody;
    // HP
    // 공격력
    // 이동속도

    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    // 스테미너
    private float playerStamina;
    // 점프중
    private bool isJump;
    // 점프파워
    private float jumpPower;
    // 달리기 시작 타이머
    float runTimer;
    // 
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
        moveSpeed = 1f;
        attackDistance = 2;
        runTimer = 0;
    }
    private void FixedUpdate()
    {
        PlayerMove();
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
        if (inputX != 0 || inputZ != 0)
        {
            runTimer++;
            if (runTimer < 100)
            {
                //moveSpeed = 1;
                Debug.Log("runTimer : " + runTimer);
                animator.SetFloat("Move", moveSpeed);
                moveSpeed = (moveSpeed > 2.5f) ? 2.5f : (moveSpeed + .1f);
            }
            else
            {
                //moveSpeed = 1;
                Debug.Log("runTimer : 10 넘었다.");
                animator.SetFloat("Move", moveSpeed);
                moveSpeed = (moveSpeed > 5.0f) ? 5.0f : (moveSpeed + .1f);
            }
            
        }
        if (inputZ == 0 && inputX == 0)
        {
            moveSpeed = 1.0f;
            runTimer = 0;
            animator.SetFloat("Move", 0f);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveSpeed = 1.5f;
        }

        Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized;
        Vector3 movePosition = new Vector3(inputX, 0, inputZ) * moveSpeed * Time.deltaTime;
        //if (direction.magnitude >= 0.1f)
        //{
        //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * camTransform.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        // Debug.Log("targetAngle : " + targetAngle + ", angle : " + angle);
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        Debug.Log("moveSpeed : " + moveSpeed);
        playerRigidbody.position = movePosition + transform.position;

        //}

        //Vector3 movePosition = new Vector3(inputX, 0, inputZ) * moveSpeed * Time.deltaTime;

        //playerRigidbody.position = movePosition + transform.position;

        //Vector3 velocity = new Vector3(inputX, 0, inputZ) * moveSpeed;

        //playerRigidbody.velocity = velocity;

        //Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
        //playerRigidbody.velocity = vector3;
    }
}
