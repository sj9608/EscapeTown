using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIPause : MonoBehaviour
{
    // ***************** 인게임 내 일시정지 팝업 메뉴창 ***************** //
    GameManager GMI;
    public GameObject menuSet;  // 인 게임 메뉴창
    public GameObject menuTable;
    public GameObject optionSet;   // 셋팅 창 
    public Button btn_Continue; // 계속하기 버튼
    public Button btn_GotoMain; // 메인화면으로
    public Button btn_Setting;  // 설정
    public Button btn_Quit;     // 게임종료
    UIOption uioption;
    public bool ispopup;

    private void Awake() {
        GMI = GameManager.Instance;
    }
    private void OnEnable() {
        GMI.UIPauseAction += MenuPopup;
        GMI.UIOptionToggleAction += MenuPopup;
    }
    private void OnDisable() {
        GMI.UIPauseAction -= MenuPopup;
        GMI.UIOptionToggleAction -= MenuPopup;
    }
    void Update()
    {
        // if (GMI.isGameOver || GMI.isLoading)
        // {
        //     return;
        // }
        // popUp_Menu();
    }


    void popUp_Menu()  //인 게임내 팝업 메뉴창
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           
            if (menuSet.activeSelf)
            {
                
                
            }
            else
            {
                menuSet.SetActive(true);
                GameManager.Instance.isPopupOn = true;
                ispopup = true;
                // 마우스 커서 고정을 해제
                Cursor.lockState = CursorLockMode.None;
                // 마우스 커서를 보이게 함
                Cursor.visible = true;
                // Set_pause(); // 게임 일시정지
            }
        }
    }

    public void MenuPopup(bool isPopup){
        menuSet.SetActive(isPopup);
    }

    public void BTN_Continue()  // 팝업 메뉴 <계속하기>버튼
    {
        GMI.Popup();
    }

    public void BTN_Main()  // 팝업 메뉴 <메인으로> 버튼
    {
        // SceneManager.LoadScene("Main",0); -> 씬이 멈춤
        //SceneController.Instance.NextSecne(0); -> ?????
        // SceneManager.LoadScene("Main",0);
        GMI.GoMain();
        GMI.Popup();
    }

    public void BTN_Setting()   // 팝업 메뉴 <설정> 버튼
    {   
        optionSet.SetActive(true);
        menuSet.SetActive(false);
        GMI.isOptionOn = true;
    }

    public void BTN_ReStart()   // 팝업 메뉴 <다시하기> 버튼
    {
        menuSet.SetActive(false);
        GameManager.Instance.GameRetry();
    }

    public void BTN_Exit()  // 팝업 메뉴 <게임종료>버튼
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }


    // public void Set_pause() // 일시정지
    // {
    //     Time.timeScale = 0f;
    //     //Cursor.lockState = CursorLockMode.Confined;
    //     Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격

    // }
    // public void Set_Outpause() // 일시정지 해제
    // {
    //     Time.timeScale = 1f;
    //     //Cursor.lockState = CursorLockMode.Locked;
    //     Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격
    // }

}
