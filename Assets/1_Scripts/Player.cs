using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Human
{
    // 캐릭터 컨트롤러(특징 중력없음)
    CharacterController controller;
    // 캐릭터를 카메라가 보는 방향에 대응하게 만들기 위한 카메라의 트랜스폼을 받아올 오브젝트 // 메인카메라를 사용해야함.
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

    // 캐릭터의 회전을 부드럽게 만들어줄 변수
    float turnSmoothTime = 0.1f;
    // 캐릭터의 부드러운 방향전환을 위한 velocity
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
        controller = GetComponent<CharacterController>();
        moveSpeed = 1f;
        attackDistance = 2;
        runTimer = 0;

        // 마우스 커서
        Cursor.lockState = CursorLockMode.Locked;
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
        
        // 플레이어 이동 방향에 대한 벡터
        Vector3 direction = new Vector3(inputX, 0f, inputZ).normalized;

        // 움직임 검사
        if (direction.magnitude >= 0.1f) // 움직임이 있다면 (어느방향으로든 방향벡터값이)
        {
            // 플레이어가 움직이는 방향에 대한 회전을 하기 위해서
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camTransform.eulerAngles.y; // (x, z)벡터에 대한 각도 * 라디안 단위로 변환 + ( 카메라의 회전에 캐릭터가 대응하게 하기 위한 값)

            // 캐릭터의 회전을 부드럽게 만들기 위한 처리 과정
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // 실제 회전 진행
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // 카메라가 바라보는 방향에 대응하게 하기위한 벡터 (실제 움직임 벡터)
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
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
            if (Input.GetKey(KeyCode.LeftControl))
            {
                moveSpeed = 1.5f;
            }
            if (inputZ == 0 && inputX == 0)
            {
                moveDir = Vector3.zero;
                moveSpeed = 1.0f;
                runTimer = 0;
                animator.SetFloat("Move", 0f);
            }
            controller.Move(moveDir * moveSpeed * Time.deltaTime); // 해당 벡터의 방향으로 speed 수치만큼 frame간격마다 이동 (카메라가 바라보는 방향으로 움직이게 하기위해서 direction --> moveDir 교체)
        }


        //if (inputX != 0 || inputZ != 0)
        //{
        //    runTimer++;
        //    if (runTimer < 100)
        //    {
        //        //moveSpeed = 1;
        //        Debug.Log("runTimer : " + runTimer);
        //        animator.SetFloat("Move", moveSpeed);
        //        moveSpeed = (moveSpeed > 2.5f) ? 2.5f : (moveSpeed + .1f);
        //    }
        //    else
        //    {
        //        //moveSpeed = 1;
        //        Debug.Log("runTimer : 10 넘었다.");
        //        animator.SetFloat("Move", moveSpeed);
        //        moveSpeed = (moveSpeed > 5.0f) ? 5.0f : (moveSpeed + .1f);
        //    }
            
        //}
        //if (inputZ == 0 && inputX == 0)
        //{
        //    moveSpeed = 1.0f;
        //    runTimer = 0;
        //    animator.SetFloat("Move", 0f);
        //}
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    moveSpeed = 1.5f;
        //}

        //Vector3 movePosition = new Vector3(inputX, 0, inputZ) * moveSpeed * Time.deltaTime;
        //Debug.Log("moveSpeed : " + moveSpeed);
        //playerRigidbody.position = movePosition + transform.position;
        //if (direction.magnitude >= 0.1f)
        //{
        //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg * camTransform.eulerAngles.y;
        //float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        // Debug.Log("targetAngle : " + targetAngle + ", angle : " + angle);
        //transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        //}

        //Vector3 movePosition = new Vector3(inputX, 0, inputZ) * moveSpeed * Time.deltaTime;

        //playerRigidbody.position = movePosition + transform.position;

        //Vector3 velocity = new Vector3(inputX, 0, inputZ) * moveSpeed;

        //playerRigidbody.velocity = velocity;

        //Vector3 vector3 = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed * Time.deltaTime;
        //playerRigidbody.velocity = vector3;
    }
}
