using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterLocomotion : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDown;

    bool isCrouch;
    bool isSprint;
    bool isJumping;

    Animator animator;
    Vector2 input; // x축, y축 입력 받을 벡터
    GameObject rifle; // 플레이어 자식에 있는 무기 컴포넌트 받아올 용도
    public Rig weaponPoseRig; // 무기의 위치
    public Rig handIK; // 무기들었을 때 손의 위치 제어

    CharacterController cc;
    Vector3 rootMotion;
    Vector3 velocity;

    PlayerAttack playerAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        rifle = transform.Find("Rifle").gameObject; // 라이플 오브젝트

        cc = GetComponent<CharacterController>();

    }

    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); 

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        // 스프린트 감지
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            isSprint = true;
            animator.SetBool("isSprinting", isSprint);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprint = false;
            animator.SetBool("isSprinting", isSprint);
        }

        // 크라우치 감지
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouch = true;
            animator.SetBool("isCrouch", isCrouch);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouch = false;
            animator.SetBool("isCrouch", isCrouch);
        }

        // 점프감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 재장전 감지
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(playerAttack.isGun == false)
            {
                weaponPoseRig.weight = 1f;
                handIK.weight = 1f;
                rifle.SetActive(true);
                playerAttack.isGun = true;
            }
            else 
            {
                weaponPoseRig.weight = 0f;
                handIK.weight = 0f;
                rifle.SetActive(false);
                playerAttack.isGun = false;
            }
        }
    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
            cc.Move(velocity * Time.fixedDeltaTime);
            isJumping = !cc.isGrounded;
            rootMotion = Vector3.zero; 
        }
        else
        {
            cc.Move(rootMotion + velocity * stepDown);
            rootMotion = Vector3.zero;
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            isJumping = true;
            velocity = animator.velocity;
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }
    }
}
