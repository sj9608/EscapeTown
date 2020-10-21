using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    // 게임 로딩 화면 UI
    // 슬라이더 업데이트, 로딩 씬 뒤에 게임 씬 불러오기

    public int sceneName;                                       // 전환하는 씬 이름

    [Header("Editor Settings")]
    public Slider progressBar;                                     // 로딩 바 슬라이더
    public Image fade;
    public Text pressSpace;

    float time = 0.0f;   // 페이드 인/아웃 지속 시간
    float fades = 1.0f;
    bool fadeout = false;

    void Start()
    {
        sceneName = SceneController.Instance.CurSceneNum;
        StartCoroutine(LoadScene());
    }

    private void Update() {
        if(!fadeout) return;

        time += Time.deltaTime;
        if(fades > 0.0f && time >= 0.1f){
            fades -= 0.1f;
            fade.color = new Color(0,0,0, fades);
            time = 0;
        } else if(fades <= 0.0f){
            fadeout = false;
            time = 0;
        }
    }

    IEnumerator LoadScene()
    {   
        // 게임 씬을 뒤에서 로드하는 동안 로딩 값을 슬라이더로 보여주는 로딩 화면

        // 비동기적 연산으로 뒤에서 씬 로드 
        AsyncOperation aOperation = SceneManager.LoadSceneAsync(sceneName);
        aOperation.allowSceneActivation = false;                   // 씬을 불러오는 중에는 화면 비활성화
        
        float timer = 0.0f;
        while(!aOperation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            Debug.Log(aOperation.progress);

            // 로딩 값 받아서 슬라이더 이동
            if(aOperation.progress < 0.9f)
            {
                progressBar.value = Mathf.Lerp(progressBar.value, aOperation.progress, timer);
                
            }
            else
            {
                progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);

                pressSpace.text = "Press Space";
                if(Input.GetKeyDown(KeyCode.Space) && aOperation.progress >= 0.9f)
                {   
                    fadeout = true;
                    yield return new WaitForSeconds(1);
                    aOperation.allowSceneActivation = true;         // 씬을 다 불러오면 화면 활성화
                    yield break;
                }
            }
        }
    }
}
