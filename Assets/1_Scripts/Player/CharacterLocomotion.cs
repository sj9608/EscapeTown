using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterLocomotion : MonoBehaviour
{
    public float normalSpeed;
    public float crouchSpeed;
    public float sprintSpeed;
    float curruntSpeed;

    bool isCrouch;
    bool isSprint;

    Animator animator;
    Vector2 input; // x축, y축 입력 받을 벡터
    GameObject rifle; // 플레이어 자식에 있는 무기 컴포넌트 받아올 용도
    public Rig weaponPoseRig; // 무기의 위치
    public Rig handIK; // 무기들었을 때 손의 위치 제어

    CharacterController cc;
    Transform tf;
    Vector3 rootMotion;

    PlayerAttack playerAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        rifle = transform.Find("Rifle").gameObject; // 라이플 오브젝트

        cc = GetComponent<CharacterController>();

        tf = GetComponent<Transform>();

        normalSpeed = 50;
        sprintSpeed = 10;
        crouchSpeed = 3;

        curruntSpeed = normalSpeed;
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
            curruntSpeed = sprintSpeed;
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
            curruntSpeed = crouchSpeed;
            animator.SetBool("isCrouch", isCrouch);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouch = false;
            animator.SetBool("isCrouch", isCrouch);
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
        cc.Move(new Vector3(input.x, 0f, input.y) * curruntSpeed * Time.fixedDeltaTime);
        //rootMotion = Vector3.zero;
    }
}
