using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIStartGame : MonoBehaviour
{
    // 게임 시작 화면
    // 새로하기, 이어하기, 설정, 게임 종료 버튼

    public void OnClickStart()                          // 새로하기 버튼
    {
        // 씬 로딩 화면 호출
        //UIManager.Instance.sceneName = "Room";
        //SceneManager.LoadScene("LoadingScene");

        // 스테이지 1 화면 호출
        // 플레이 화면 호출
        SceneController.Instance.NextSecne();
    }

    public void OnClickLoad()                           // 이어하기 버튼
    {
        // 저장된 정보 불러오기
        // 저장된 정보에 있는 씬 불러오기

        // 씬 로딩 화면 호출
        //SceneController.Instance.CurSceneNum = 0;
        SceneManager.LoadScene("LoadingScene");
    }

    public void OnClickSettings()                       // 설정 버튼
    {
        // 설정 패널 띄우기
    }

    public void OnClickClose()                          // 게임 종료 버튼
    {
        // 게임 종료
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit(); // 어플리케이션 종료
    #endif
    }
}
