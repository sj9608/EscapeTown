using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Human : MonoBehaviour
{
    // HP
    // 공격력
    // 이동속도
    // 걷기()
    // 뛰기()
    // 공격()
    // 죽음()

    // 보류
    // 대기()
    public GameObject obj;
    public Animator animator;
    // HP
    protected int hp;
    // 공격력
    protected int ap;
    // 공격 범위
    protected float attackDistance;
    // 이동속도
    protected float moveSpeed;
    // 공격중
    protected bool isAttack;
    public int HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    public int AP
    {
        get
        {
            return ap;
        }
        set
        {
            ap = value;
        }
    }
    // 공격()
    protected virtual void Attack()
    {

    }
    // 죽음()
    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
    // 대기()
    void Idle()
    {

    }
}
