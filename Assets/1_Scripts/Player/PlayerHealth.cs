using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    GameInformation GI;
    public bool isDead { get; protected set; } // 사망 상태
    private Animator playerAnimator; // 애니메이터 컴포넌트
    AudioSource audioSource;
    [Header("-Sound")]
    public AudioClip audioDead;
    public AudioClip[] audioBeAttacked;
    void Start()
    {
        GI = GameInformation.Instance;
        playerAnimator = GetComponent<Animator>();
        isDead = false;

        // 사운드
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    // 데미지를 받음
    public void OnDamage(float attackPoint)
    {
        // 죽었으면 리턴 아래 실행 안함
        if (isDead) return;
        // GameInformation의 event unityAction에 등록된 UI쪽 메서드 실행
        // HP -= attackPoint;
        GI.UpdateHp(-attackPoint);

        // 사운드
        int random = UnityEngine.Random.Range(0, 2);
        audioSource.PlayOneShot(audioBeAttacked[random]);

        Debug.Log("플레이어 공격 받음. 남은체력" + GI.HP);
        if (GI.HP <= 0)
        {
            // HP가 다 떨어지면 메서드 실행
            StartCoroutine(Die());
        }
    }
    // 죽음
    private IEnumerator Die()
    {
        // 죽음 판단 bool
        isDead = true;
        // GameManager에 게임오버 알림
        GameManager.Instance.isGameOver = true;
        // 애니메이션
        playerAnimator.SetTrigger("Die");

        // 사운드
        audioSource.PlayOneShot(audioDead);
        // 애니메이션 기다림
        yield return new WaitForSeconds(3f);
        // 죽고나면 발동 할 메서드
        GameManager.Instance.PlayerDead();
    }
}
