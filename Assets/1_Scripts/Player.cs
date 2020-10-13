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
    // HP
    // 공격력
    // 이동속도

    // 캐릭터의 회전을 부드럽게 만들어줄 변수
    float turnSmoothTime = 0.1f;
    // 캐릭터의 부드러운 방향전환을 위한 velocity
    float turnSmoothVelocity;
    // 스테미너
    private float playerStamina;

    public float jumpHeight = 3f; // 캐릭터의 점프력
    public float gravity = -9.81f; // 중력
    /* 그라운드 체크 */
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    private LayerMask groundMask;
    Vector3 velocity; // 낙하속도 계산할 벡터(중력값에 대한 계산을 할 속도벡터)

    bool isGrounded;
    // 달리기 시작 타이머
    float runTimer;
    // 상호작용 거리
    float interactionDistance;
    // 사용할 총
    public Gun gun;
    public Transform gunPivot; // 총 배치의 기준점
    public Transform leftHandMount; // 총의 왼쪽 손잡이, 왼손이 위치할 지점
    public Transform rightHandMount; // 총의 오른쪽 손잡이, 오른손이 위치할 지점

    bool isAim;
    bool isCrouch;
    bool isDead;
    // 인벤토리 입력키 반복으로 열고 닫고 싶을 때
    bool isInvenOpen;
    // 대화 수첩 입력키 반복으로 열고 닫고 싶을 때
    bool isNoteOpen;
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
        HP = 100;
        AP = 40;
        enemyLayer = LayerMask.GetMask("Enemy");
        itemLayer = LayerMask.GetMask("Item");
        groundMask = LayerMask.GetMask("Ground");
        controller = GetComponent<CharacterController>();
        moveSpeed = 1f;
        attackDistance = 10;
        interactionDistance = 2;
        runTimer = 0;

        isDead = false;
        isAim = true;
        isCrouch = false;

        isInvenOpen = false;
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
        if (Input.GetMouseButtonDown(1))
        {
            isAim = true;
            // 조준 카메라 구현
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetItem();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (gun.Reload())
            {
                // 재장전 성공시에만 재장전 애니메이션 재생
                animator.SetTrigger("Reload");
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            OpenNote();
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouch = true;
            animator.SetBool("isCrouch", isCrouch);
        }
        else
        {
            isCrouch = false;
            animator.SetBool("isCrouch", isCrouch);
        }
    }
    protected override void Attack()
    {
        //RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackDistance, transform.forward, 0, enemyLayer);
        //Debug.Log(hits.Length + "개");
        //if (hits.Length > 0)
        //{
        //    target = hits[0].collider;
        //}
        //else
        //{
        //    target = null;
        //}
        //GameManager.Instance.Attack(target, AP);
        gun.Fire();
    }
    protected void GetItem()
    {
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
    void PlayerMove()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        
        /* 지면 체크 및 중력(낙하속도)*/
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // 캐릭터가 땅에 닿았는지 체크 닿았으면 true 반환
        //Debug.Log(isGrounded);
        //if (isGrounded && velocity.y < 0) // 땅 , 낙하속도 검사
        //{
        //    velocity.y = -2f; // 땅에 닿아있고 낙하속도가 0 보다 낮으면 낙하속도를 -2f 로 초기화
        //}

        ///* 캐릭터 중력에 대한 움직임 */
        //velocity.y += gravity * Time.deltaTime; // 중력값 계산
        //controller.Move(velocity * Time.deltaTime); // 실제 중력 적용

        ///* 점프 */
        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // 중력값에 비례한 점프력 계산 ( 중간 -2는 중력값이 마이너스 값이라서 위로 뛰려면 양수로 만들어줘야함)
        //}
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
            if (isCrouch)
            {
                moveSpeed = 1.5f;
                animator.SetFloat("Crouch", moveSpeed);
            }
            else
            {
                runTimer++;
                if (runTimer < 100)
                {
                    animator.SetFloat("Move", moveSpeed);
                    moveSpeed = (moveSpeed > 2.5f) ? 2.5f : (moveSpeed + .1f);
                }
                else
                {
                    animator.SetFloat("Move", moveSpeed);
                    moveSpeed = (moveSpeed > 5.0f) ? 5.0f : (moveSpeed + .1f);
                }
            }
            controller.Move(moveDir * moveSpeed * Time.deltaTime); // 해당 벡터의 방향으로 speed 수치만큼 frame간격마다 이동 (카메라가 바라보는 방향으로 움직이게 하기위해서 direction --> moveDir 교체)
        }
        else
        {
            moveSpeed = 1.0f;
            runTimer = 0;
            animator.SetFloat("Move", 0f);
            animator.SetFloat("Crouch", 0f);
        }
    }
    void OpenInventory()
    {
        if (!isInvenOpen)
        {
            Debug.Log("인벤토리 열림");
        }
        else
        {
            Debug.Log("인벤토리 닫힘");
        }
        isInvenOpen = !isInvenOpen;
    }
    void OpenNote()
    {
        if (!isNoteOpen)
        {
            Debug.Log("대화 수첩 열림");
        }
        else
        {
            Debug.Log("대화 수첩 닫힘");
        }
        isNoteOpen = !isNoteOpen;
    }
    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        // 총의 기준점 gunPivot을 3D 모델의 오른쪽 팔꿈치 위치로 이동
        gunPivot.position =
            animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand,
            leftHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand,
            leftHandMount.rotation);

        // IK를 사용하여 오른손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

        animator.SetIKPosition(AvatarIKGoal.RightHand,
            rightHandMount.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand,
            rightHandMount.rotation);
    }
    public void OnDamage(float attackPoint)
    {
        if (isDead)
        {
            return;
        }
        HP -= attackPoint;
        Debug.Log("플레이어 공격 받음. 남은체력"+ HP);
        if (HP < 0)
        {
            HP = 0;
            isDead = true;
            Die();
        }
    }

    public override void Die()
    {
        GameManager.Instance.PlayerDead();
        animator.SetTrigger("Die");
        // base.Die();
    }
}
