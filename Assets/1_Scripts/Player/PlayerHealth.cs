using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100f; // 시작 체력
    public bool isDead { get; protected set; } // 사망 상태
    public event Action onDeath; // 사망시 발동할 이벤트

    private Animator playerAnimator; // 애니메이터 컴포넌트

    AudioSource audioSource;
    [Header("-Sound")]
    public AudioClip audioDead;
    public AudioClip[] audioBeAttacked;

    GameInformation GI;

    void Start()
    {
        GI = GameInformation.Instance;
        playerAnimator = GetComponent<Animator>();
        isDead = false;

        // 사운드
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnDamage(float attackPoint)
    {
        if (isDead) return;

        // HP -= attackPoint;
        GI.UpdateHp(-attackPoint);

        // 사운드
        int random = UnityEngine.Random.Range(0, 2);
        audioSource.PlayOneShot(audioBeAttacked[random]);

        Debug.Log("플레이어 공격 받음. 남은체력" + GI.HP);
        if (GI.HP <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        GameManager.Instance.isGameOver = true;
        playerAnimator.SetTrigger("Die");

        // 사운드
        audioSource.PlayOneShot(audioDead);

        yield return new WaitForSeconds(3f);
        GameManager.Instance.PlayerDead();
    }
}
