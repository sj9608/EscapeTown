﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterLocomotion : MonoBehaviour
{
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower;

    bool isCrouch;
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

    int isSprintingParam = Animator.StringToHash("isSprinting");

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


        UpdateIsSprinting();
        // 스프린트 감지
        //if (Input.GetKeyDown(KeyCode.LeftShift)) 
        //{
        //    isSprint = true;
        //    animator.SetBool("isSprinting", isSprint);
        //}
        //if (Input.GetKeyUp(KeyCode.LeftShift))
        //{
        //    isSprint = false;
        //    animator.SetBool("isSprinting", isSprint);
        //}

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

    public void UpdateIsSprinting()
    {
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool(isSprintingParam, isSprinting);
    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            UpdateInAir();
        }
        else
        {
            UpdateOnGround();
        }
    }

    void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;
        cc.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;
        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }

    void Jump()
    {
        if (!isJumping)
        {
            float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVelocity);
        }
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }

    void SetInAir(float jumpVelocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVelocity;
        animator.SetBool("isJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
