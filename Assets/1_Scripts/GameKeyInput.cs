using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameKeyInput : MonoBehaviour
{
    private string moveXAxisName = "Horizontal"; // 좌우 움직임을 위한 입력축 이름
    private string moveYAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름

    private int fireKey = 0; // 공격 버튼 (마우스 왼쪽)
    private int aimKey = 1; // 공격 버튼 (마우스 왼쪽)
    private KeyCode reloadKey = KeyCode.R; // leftShift key 달리기키 설정
    private KeyCode interactionKey = KeyCode.F; // leftShift key 달리기키 설정
    private KeyCode openChatNoteKey = KeyCode.P; // leftShift key 달리기키 설정
    private KeyCode changeWeaponKey = KeyCode.Q; // leftShift key 달리기키 설정
    private KeyCode quickSlot1Key = KeyCode.Alpha1; // leftShift key 달리기키 설정
    private KeyCode quickSlot2Key = KeyCode.Alpha2; // leftShift key 달리기키 설정
    private KeyCode crouchKey = KeyCode.LeftControl; // leftShift key 달리기키 설정
    private KeyCode sprintKey = KeyCode.LeftShift; // leftShift key 달리기키 설정
    private KeyCode popupMenuKey = KeyCode.Escape; // leftShift key 달리기키 설정
    // 값 할당은 내부에서만 가능
    public float moveX { get; private set; } // 감지된 움직임 입력값
    public float moveY { get; private set; } // 감지된 회전 입력값
    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값
    // 우클릭
    public bool aim { get; private set; }
    // 상호작용
    public bool interaction { get; private set; }
    // 상호작용
    public bool openChatNote { get; private set; }
    // 상호작용
    public bool changeWeapon { get; private set; }
    // 상호작용
    public bool quickSlot1 { get; private set; }
    // 상호작용
    public bool quickSlot2 { get; private set; }
    // 상호작용
    public bool crouch { get; private set; }
    // 상호작용
    public bool sprint { get; private set; }
    public bool popupMenu { get; private set; }

    // Update is called once per frame
    //void Update()
    //{
    //    // 게임오버 상태에서는 사용자 입력을 감지하지 않는다
    //    if (GameManager.Instance != null
    //        && GameManager.Instance.IsGameOver)
    //    {
    //        moveX = 0;
    //        moveY = 0;
    //        fire = false;
    //        reload = false;
    //        aim = false;
    //        interaction = false;
    //        openChatNote = false;
    //        changeWeapon = false;
    //        quickSlot1 = false;
    //        quickSlot2 = false;
    //        crouch = false;
    //        sprint = false;
    //        popupMenu = false;

    //        return;
    //    }

    //    // move에 관한 입력 감지
    //    moveX = Input.GetAxis(moveXAxisName);
    //    // rotate에 관한 입력 감지
    //    moveY = Input.GetAxis(moveYAxisName);
    //    // fire에 관한 입력 감지
    //    fire = Input.GetMouseButtonDown(0);
    //    // fire에 관한 입력 감지
    //    aim = Input.GetMouseButtonDown(aimKey);
    //    // reload에 관한 입력 감지
    //    reload = Input.GetKeyDown(KeyCode.R);
    //    // reload에 관한 입력 감지
    //    interaction = Input.GetKeyDown(KeyCode.F);
    //    // reload에 관한 입력 감지
    //    openChatNote = Input.GetKeyDown(openChatNoteKey);
    //    // reload에 관한 입력 감지
    //    changeWeapon = Input.GetKeyDown(KeyCode.Q);
    //    // reload에 관한 입력 감지
    //    quickSlot1 = Input.GetKeyDown(KeyCode.Alpha1);
    //    // reload에 관한 입력 감지
    //    quickSlot2 = Input.GetKeyDown(KeyCode.Alpha2);
    //    // reload에 관한 입력 감지
    //    crouch = Input.GetKey(KeyCode.LeftControl);
    //    // sprint에 관한 입력 감지
    //    sprint = Input.GetKey(KeyCode.LeftShift);
    //    // reload에 관한 입력 감지
    //    popupMenu = Input.GetKeyDown(popupMenuKey);
    //}
}
