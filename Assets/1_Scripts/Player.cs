using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 공격력
    protected float ap;
    // 공격중
    protected bool isAttack;
    public float AP
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
    // HP
    // 공격력
    // 이동속도

    public MainUI mainUI;
    // 스테미너
    private float playerStamina;

    /* 달리기 키 관련 */
    [SerializeField]
    private KeyCode sprintKey = KeyCode.LeftShift; // leftShift key 달리기키 설정

    public bool isDead;
    // 인벤토리 입력키 반복으로 열고 닫고 싶을 때
    bool isInvenOpen;
    // 대화 수첩 입력키 반복으로 열고 닫고 싶을 때
    bool isNoteOpen;
    // 대기()
    // 걷기()
    // 공격()
    // 죽음()

    // 점프()
    // 뛰기()
    // 앉기()
    // 앉아 걷기()
    // 대쉬()
    // 무기교체()

    // 점프()
    // 뛰기()
    // 앉기()
    // 앉아 걷기()
    // 대쉬()
    // 무기교체()

    // 공격 모듈
    PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();

        AP = 40;

        isDead = false;
        isInvenOpen = false;

        // 마우스 커서
        //Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {

    }

}
