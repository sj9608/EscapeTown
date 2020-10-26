using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPause : MonoBehaviour
{
    // ***************** 인게임 내 일시정지 팝업 메뉴창 ***************** //

    public GameObject menuSet;  // 인 게임 메뉴창
    public Button btn_Continue; // 계속하기 버튼
    // public Button btn_GotoMain; // 메인화면으로
    // public Button btn_Setting;  // 설정
    public Button btn_Quit;     // 게임종료
    bool isPopUp;



    void Update()
    {
        popUp_Menu();
    }


    void popUp_Menu()  //인 게임내 팝업 메뉴창
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                isPopUp = false;
                Set_Outpause(); // 일시정지 해제
            }

            else
            {
                //Cursor.lockState = CursorLockMode.Locked;
                menuSet.SetActive(true);
                isPopUp = true;
                Set_pause(); // 게임 일시정지
            }
        }
    }

    public void BTN_Continue()  // 팝업 메뉴 <계속하기>버튼
    {
        menuSet.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Set_Outpause();
    }
    public void BTN_Exit()  // 팝업 메뉴 <게임종료>버튼
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }


    public void Set_pause() // 일시정지
    {
        Time.timeScale = 0f;
        //Cursor.lockState = CursorLockMode.Confined;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격

    }
    public void Set_Outpause() // 일시정지 해제
    {
        Time.timeScale = 1f;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격
    }

}
