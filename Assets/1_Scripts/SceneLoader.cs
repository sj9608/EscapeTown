using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Slider progressbar;
    public Text loadtext;
    public static int loadType;
    public Image panel;
    float time = 0f;
    float fade_Time = 1f; //페이드 인/아웃 지속 시간

    void Start()
    {
        StartCoroutine(LoadScene());
        Cursor.lockState = CursorLockMode.Locked;
    }

    public static void LoadSceneHandle(string _name, int _loadType)
    {
        //_name : 로드할 씬 이름
        //_loadType : '새 게임'인지 '이어하기'인지
        string loadScene = _name;
        int loadType = _loadType;
        SceneManager.LoadScene("LoadingScene");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync("Room");
        operation.allowSceneActivation = false; //씬을 비동기로 처리하면서 porgressbar로 상태를 표기
        while (!operation.isDone)
        {
            yield return null;
            if (loadType == 0) Debug.Log("New Game"); // 새게임 로드
            else if (loadType == 1) Debug.Log("Load Game"); // 진행했던 게임 로드

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }

            else if (operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f)
            {
                loadtext.text = "Press SpaceBar";
                
            }

            if (Input.GetKeyDown(KeyCode.Space) && progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                FadeOut();
                yield return new WaitForSeconds(1f);
                operation.allowSceneActivation = true; 
            }
            
        } 

    }

    public void FadeOut()
    {
        StartCoroutine(FadeFlow());

        IEnumerator FadeFlow()
        {
            panel.gameObject.SetActive(true);
            time = 0f;
            
            Color alpha = panel.color;

            while(alpha.a > 0f)
            {
                time += Time.deltaTime/fade_Time;
                alpha.a = Mathf.Lerp(1,0,time);

                panel.color = alpha;
                yield return null;
            }
        }
    }

    public void FadeIn()
    {
        StartCoroutine(FadeFlow());

            IEnumerator FadeFlow()
            {
                time = 0f;
                //yield return new WaitForSeconds(1f);
                Color alpha = panel.color;

            while(alpha.a < 1f)
            {
                time += Time.deltaTime/fade_Time;
                alpha.a = Mathf.Lerp(0,1,time);
                panel.color = alpha;
                yield return null;
            }
        
            panel.gameObject.SetActive(false);
            yield return null;
        }
    }
}
