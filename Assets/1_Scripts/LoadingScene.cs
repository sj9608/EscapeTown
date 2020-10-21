using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    // 게임 로딩 화면 UI
    // 슬라이더 업데이트, 로딩 씬 뒤에 게임 씬 불러오기

    public int curSceneNum;                                       // 전환하는 씬 이름

    [Header("Editor Settings")]
    public Slider progressBar;                                     // 로딩 바 슬라이더
    public FadeController fader;
    public Text pressSpace;
    // Start is called before the first frame update
    void Start()
    {
        curSceneNum = SceneController.Instance.CurSceneNum;
        StartCoroutine(LoadingFade(curSceneNum));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadingFade(int nextSceneNum)
    {
        fader.FadeOut(1f);

        yield return new WaitForSeconds(1f);

        fader.FadeIn(1f, () =>
        {
            AsyncOperation aOperation = SceneManager.LoadSceneAsync(nextSceneNum, LoadSceneMode.Additive);
            //aOperation.allowSceneActivation = false;                   // 씬을 불러오는 중에는 화면 비활성화

            //float timer = 0.0f;
            //while (!aOperation.isDone)
            //{
            //    timer += Time.deltaTime;
            //    Debug.Log(aOperation.progress);

            //    // 로딩 값 받아서 슬라이더 이동
            //    if (aOperation.progress < 0.9f)
            //    {
            //        progressBar.value = Mathf.Lerp(progressBar.value, aOperation.progress, timer);

            //    }
            //    else
            //    {
            //        progressBar.value = Mathf.Lerp(progressBar.value, 1f, timer);

            //        pressSpace.text = "Press Space";
            //        if (Input.GetKeyDown(KeyCode.Space) && aOperation.progress >= 0.9f)
            //        {
            //            aOperation.allowSceneActivation = true;         // 씬을 다 불러오면 화면 활성화
            //        }
            //    }
            //}
        });
        gameObject.SetActive(false);
    }
}
