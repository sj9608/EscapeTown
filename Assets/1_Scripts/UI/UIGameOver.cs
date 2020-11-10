using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    // UI는 Action에 등록하여 화면만 보여준다.
    // 기능적인 것은 GameManager나 여타 다른 곳에서 구현
    
    // 게임 오버 화면 UI
    public GameObject gameover;                                   // 게임오버 패널 창
    public Button btn_Retry;                                      // 게임오버 다시하기 버튼
                                                                  // 저장되어 있던 가장 마지막 데이터를 불러오기
    public Button btn_Main;                                       // 게임오버 메인으로 가기
                                                                  // 메인 화면으로 씬 전환
    public Button btn_Exit;                                       // 게임오버 게임종료 버튼

    void Start()
    {
        gameover.SetActive(false);
    }

    private void OnEnable() {
        // GameOver시 보여줘야 할 UI메서드 Action에 메서드 추가
        GameManager.Instance.GameOverAction += Show_GameOver_Pannel;
    }
    private void OnDisable(){
        // GameOver시 보여줘야 할 UI메서드 Action에 메서드 제거
        // null check 필요
        GameManager.Instance.GameOverAction -= Show_GameOver_Pannel;
    }
    // GameOver시 보여 줄 UI
    public void Show_GameOver_Pannel()
    {
        gameover.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    // 다시하기 버튼
    public void GameOver_BTN_Retry()
    {   // 가장 마지막으로 저장했던 데이터를 불러오기
        gameover.SetActive(false);
        // GameInfomation 에서 마지막 저장 데이터를 로드하는 메서드를 호출
        // GameInformation.Instance.Load();


        // Cursor.lockState = CursorLockMode.Locked;
        // int s = SceneManager.GetActiveScene().buildIndex; // 현재 씬의 인덱스 번호를 다시 불러옴
        // SceneManager.LoadScene(s); // 현재 씬 다시 부르기       
        // 게임매니저의 다시하기 메서드 호출
        GameManager.Instance.GameRetry();
    }
    // 메인으로 버튼
    public void GameOver_BTN_Main()
    {   
        gameover.SetActive(false);
        // 게임매니저의 메인으로 메서드 호출
        GameManager.Instance.GoMain();
    }

    public void GameOver_BTN_Exit()
    {   // 게임 종료
    
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    
    }
}