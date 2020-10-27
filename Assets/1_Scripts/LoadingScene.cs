using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    // 게임 로딩 화면 UI
    // 슬라이더 업데이트, 로딩 씬 뒤에 게임 씬 불러오기

    public Image sr;
    public Slider progressbar;
    public Text loadtext;
    private void OnEnable()
    {
        //sr.color.a = 0f;
        progressbar.value = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowLoading(AsyncOperation operation)
    {
        StartCoroutine(LoadingFade(operation));
    }

    IEnumerator LoadingFade(AsyncOperation operation) //int nextSceneNum)
    {
        loadtext.text = "Loading..";
        FadeIn(1f);
        yield return null;
        operation.allowSceneActivation = false; //씬을 비동기로 처리하면서 porgressbar로 상태를 표기
        while (!operation.isDone)
        {
            yield return null;
            //if (loadType == 0) Debug.Log("New Game"); // 새게임 로드
            //else if (loadType == 1) Debug.Log("Load Game"); // 진행했던 게임 로드

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
                FadeOut(1f);
                operation.allowSceneActivation = true;
                yield return new WaitForSeconds(1f);
            }

        }
    }
    public void FadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeOutTime, nextEvent));
    }

    public void FadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeOutTime, nextEvent));
    }

    // 투명 -> 불투명
    IEnumerator CoFadeIn(float fadeOutTime, System.Action nextEvent = null)
    {
        Color tempColor = sr.color;
        while (tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeOutTime;
            sr.color = tempColor;

            if (tempColor.a >= 1f) tempColor.a = 1f;

            yield return null;
        }

        sr.color = tempColor;
        if (nextEvent != null) nextEvent();
    }

    // 불투명 -> 투명
    IEnumerator CoFadeOut(float fadeOutTime, System.Action nextEvent = null)
    {
        Color tempColor = sr.color;
        while (tempColor.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeOutTime;
            sr.color = tempColor;

            if (tempColor.a <= 0f) tempColor.a = 0f;

            yield return null;
        }
        sr.color = tempColor;
        if (nextEvent != null) nextEvent();
    }
}

