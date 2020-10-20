﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100f; // 시작 체력
    public bool isDead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

    private Animator playerAnimator; // 애니메이터 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDamage(float attackPoint)
    {
        if (isDead) return;

        // HP -= attackPoint;
        GameInformation.Instance.UpdateHp(attackPoint);

        Debug.Log("플레이어 공격 받음. 남은체력" + GameInformation.Instance.HP);
        if (GameInformation.Instance.HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.PlayerDead();
        playerAnimator.SetTrigger("Die");
        isDead = true;
    }
}
