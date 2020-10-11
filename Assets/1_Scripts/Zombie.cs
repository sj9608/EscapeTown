using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Walk = 1,
        Trace = 2,
        Battle = 3,
        Attack1 = 4,
        Attack2 = 5,
        Dead = 6
    }

    // public 접근자는 기획자가 조정
    public bool isDead = false;
    public int HP;                                          // 좀비의 체
    public int ap;                                          // 좀비의 공격력
    public float walkForce;                                 // 대기상태의 걷기 속도
    public float runSpeed;                                  // 추적상태의 달리기 속도
    public float attackRange;                               // 좀비 공격의 범위 
    public float searchDistance;                            // 좀비가 주인공을 탐색할 거리
    public float searchAngle;                               // 좀비의 시야각 (플레이어를 감지할 시야각)

    public float walkDelayMin = 2.0f;
    public float walkDelayMax = 3.5f;
    public float idleDelayMin = 1.5f;
    public float idleDelayMax = 4.0f;

    private float idleWalkDelay;                            // 좀비가 주변을 걷는 시간
    private float idleDelay;                                // 좀비가 제자리에 머무는 시간
    private float timeflag;                                 // 비교 기준이 될 시간

    private State currentState = State.Idle;                // 좀비의 현 상태
    private State lastState = State.Idle;                   // 좀비의 직전 상태
    private bool isAware = false;                           // 주인공을 인지했는지 구분

    private Animator anim;
    private Rigidbody rb;
    private NavMeshAgent nmAgent;                           // 추적AI


    // 테스트용 객체 선언 (주인공으로 인식할 대상)
    public Transform playerTransform;

    void Start()
    {
        HP = 100;                            
        ap = 20;                                 
        walkForce = 240f;                         
        runSpeed = 6f;                            
        attackRange = 1.3f;                      
        searchDistance = 8f;                     
        searchAngle = 120f;
        walkDelayMin = 2.0f;
        walkDelayMax = 3.5f;
        idleDelayMin = 1.5f;
        idleDelayMax = 4.0f;

        idleWalkDelay = Random.Range(walkDelayMin, walkDelayMax);
        idleDelay = Random.Range(idleDelayMin, idleDelayMax);

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        nmAgent = GetComponent<NavMeshAgent>();

        // 코루틴 시작
        StartCoroutine(Think());

        // 플레이어를 대체할 테스트용 코드
        playerTransform = FindObjectOfType<PlayerExample>().transform;
    }

    
    void Update()
    {
        if (isDead) return;                     // 좀비가 죽어있으면 즉시 리턴

        // 물리력 이동 
        if (currentState == State.Walk)
            rb.AddForce(transform.forward * walkForce * Time.deltaTime);
        
    }

    IEnumerator Think()
    {
        while (true && !isDead)
        {
            switch (currentState)
            {
                case State.Idle:
                    if (SearchPlayer())
                    {
                        TraceRun();
                        break;
                    }

                    if (Time.time - timeflag > idleDelay)
                    {
                        WalkAround();
                    }
                    break;
                case State.Walk:
                    if (SearchPlayer())
                    {
                        TraceRun();
                        break;
                    }

                    if (Time.time - timeflag > idleWalkDelay)
                    {
                        Idle();
                    }
                    break;
                case State.Trace:
                    if (Vector3.Distance(playerTransform.position, transform.position) < attackRange)
                    {
                        Debug.Log("좀비 공격모드로");
                        Battle();
                    }
                    else if (Vector3.Distance(playerTransform.position, transform.position) > searchDistance)
                    {
                        Idle();
                    }
                    else
                    {
                        nmAgent.SetDestination(playerTransform.position);
                    }
                    break;
                case State.Battle:
                    if (Vector3.Distance(playerTransform.position, transform.position) > attackRange)
                    {
                        TraceRun();
                    }else
                    {
                        Attack();
                    }
                    break;
                case State.Attack1:
                case State.Attack2:

                    break;
                case State.Dead:

                    break;
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    void Idle()
    {
        nmAgent.isStopped = true;

        timeflag = Time.time;
        idleDelay = Random.Range(idleDelayMin, idleDelayMax);
        currentState = State.Idle;

        if (Random.Range(0, 2) == 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Random.Range(-90, 0) - 30, transform.eulerAngles.z);
        } else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Random.Range(0, 90) + 30, transform.eulerAngles.z);
        }
        //myRigidbody.MoveRotation();

        anim.SetInteger("ZombieState", (int)currentState);
    }

    void WalkAround()
    {
        nmAgent.isStopped = true;
        timeflag = Time.time;
        idleWalkDelay = Random.Range(walkDelayMin, walkDelayMax);
        currentState = State.Walk;

        anim.SetInteger("ZombieState", (int)currentState);
    }

    void TraceRun()
    {
        nmAgent.isStopped = false;
        currentState = State.Trace;
        anim.SetInteger("ZombieState", (int)currentState);
    }

    void Battle()
    {
        transform.LookAt(playerTransform);
        nmAgent.isStopped = true;
        currentState = State.Battle;
        anim.SetInteger("ZombieState", (int)State.Idle);
    }

    void Attack()
    {
        nmAgent.isStopped = true;
        //currentState = State.Attack1;
        if (Random.Range(0, 2) == 0)
        {
            anim.SetTrigger("Attack1");
        }
        else
        {
            anim.SetTrigger("Attack2");
        }
    }

    public void OnDamage(int attackPoint)
    {
        HP -= attackPoint;
        if (HP < 0)
        {
            HP = 0;
            Die();
        }
    }

    void Die()
    {
        nmAgent.isStopped = true;
        currentState = State.Dead;
        anim.SetTrigger("isDead");
        isDead = true;
        StopCoroutine(Think());
    }

    bool SearchPlayer()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, searchDistance, transform.forward, 0);

        foreach (var hit in hits)
        {
            if (hit.collider)
            {
                PlayerExample player = hit.collider.GetComponent<PlayerExample>();                // 프로젝트 적용전 객체타입 Player로  %%

                // 플레이어가 존재하고, 좀비의 시야범위 안에 있으며, 탐색거리 안에 있다면 
                if (player
                    && Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < searchAngle / 2.0f
                    && Vector3.Distance(player.transform.position, transform.position) < searchDistance)
                    return true;
            }
        }
        return false;
    }
}
