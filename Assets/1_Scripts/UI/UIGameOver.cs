using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    // 게임 오버 화면 UI
    public GameObject gameover;                                   // 게임오버 패널 창
    // public Button btn_Retry;                                      // 게임오버 다시하기 버튼
    // public Button btn_Main;                                       // 게임오버 메인으로 가기
    bool isOver = false;


    void Start()
    {
        gameover.SetActive(false);
    }

    void Update()
    {
        Show_GameOver_Pannel();
    }

    public void Show_GameOver_Pannel()
    {
        if(isOver == !isOver)
            gameover.SetActive(true);
        
        else    
            return;
    }

    public void GameOver_BTN_Retry()
    {
        gameover.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        int s = SceneManager.GetActiveScene().buildIndex; // 현재 씬의 인덱스 번호를 다시 불러옴
        SceneManager.LoadScene(s); // 현재 씬 다시 부르기       
    }
    
    public void GameOver_BTN_Main()
    {
       
        SceneManager.LoadScene("Main",0);
       
        /*
        자동저장 기능 삽입 예정
        */

    }

}






















/*
    혼자라는게 내게 어울려~ 나 그렇게 마음 닫고 있는데~
    우연인지 운명인지 모를~ 너와 나의 만남이 문뜩 찾아온거야~
    워~워~워~워
    boy meets girl~ 내게 다가와~ 넌 나의 모든 것을 바꿔버렸어~
    저 하늘의 새들들도 날 보고~ 이런 내~ 마음을 눈치 챘을지도 몰라~
*/
