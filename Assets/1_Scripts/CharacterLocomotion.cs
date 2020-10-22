using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    GameObject rifle;
    public Rig weaponPoseRig;
    public Rig handIK;

    void Start()
    {
        animator = GetComponent<Animator>();
        rifle = transform.Find("Rifle").gameObject;
    }


    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); 

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);

        if(Input.GetButton("Fire3"))
        {
            animator.SetBool("isSprinting", true);
        }
        else animator.SetBool("isSprinting", false);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(weaponPoseRig.weight == 0f)
            {
                weaponPoseRig.weight = 1f;
                handIK.weight = 1f;
                rifle.SetActive(true);
            }
            else 
            {
                weaponPoseRig.weight = 0f;
                handIK.weight = 0f;
                rifle.SetActive(false);
            }
        }
    }
}
