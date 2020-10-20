using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    Animator animator;
    Vector2 input;
    bool isAiming;
    void Start()
    {
        animator = GetComponent<Animator>();
        isAiming = false;
    }


    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical"); 

        animator.SetFloat("InputX", input.x);
        animator.SetFloat("InputY", input.y);
        if(Input.GetButton("Fire2"))
        {
            isAiming = true;
        }
        else isAiming = false;
        animator.SetBool("isAim", isAiming);
    }
}
