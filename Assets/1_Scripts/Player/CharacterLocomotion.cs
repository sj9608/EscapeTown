using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input; // x축, y축 입력 받을 벡터
    GameObject rifle; // 플레이어 자식에 있는 무기 컴포넌트 받아올 용도
    public Rig weaponPoseRig; // 무기의 위치
    public Rig handIK; // 무기들었을 때 손의 위치 제어

    PlayerAttack playerAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        rifle = transform.Find("Rifle").gameObject; // 라이플 오브젝트
    }
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); 

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if(Input.GetButton("Fire3")) // left shift 키 
        {
            animator.SetBool("isSprinting", true);
        }
        else animator.SetBool("isSprinting", false);

        if(Input.GetKeyDown(KeyCode.Q))
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
}
