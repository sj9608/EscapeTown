using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : SingletonBase<SceneController>
{
    private int curSceneNum;
    public int CurSceneNum
    {
        get
        {
            return curSceneNum;
        }
        set
        {
            curSceneNum = value;
        }
    }

    public GameObject stageHUDObject;
    public GameObject chatUIObject;
    public GameObject loadingObject;
    private void Awake()
    {
        curSceneNum = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        stageHUDObject.SetActive(false);
        chatUIObject.SetActive(false);
        loadingObject.SetActive(false);
        SceneManager.LoadSceneAsync(curSceneNum, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isLoading)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            // GameManager.Instance.StageClear();
            // 테스트용 다음 씬 가기
            CurSceneNum++;
            GameManager.Instance.SaveGameDataToJson();
            NextSecne(CurSceneNum-1);
        }
    }
    public void NextSecne(int current)
    {
        StartCoroutine(IENextScene(current));
    }

    IEnumerator IENextScene(int current)
    {
        GameManager.Instance.audioSource.Stop();
        PopupChange(true);
        AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(current);

        yield return new WaitUntil(() => { return unloadAsync.isDone; });

        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(curSceneNum, LoadSceneMode.Additive);
        LoadingScene loadingScene = loadingObject.GetComponent<LoadingScene>();
        loadingScene.ShowLoading(loadAsync);
        // yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => { return loadAsync.isDone; });
        PopupChange(false);

        // 사운드
        GameManager.Instance.Sc.SceneAudio.PlayOneShot(GameManager.Instance.Sc.audioLoadingComplete);

        GameManager.Instance.InitScene();
    }

    void PopupChange(bool isLoading)
    {
        GameManager.Instance.isLoading = isLoading;
        stageHUDObject.SetActive(!isLoading);
        chatUIObject.SetActive(!isLoading);
        loadingObject.SetActive(isLoading);
        GameManager.Instance.IsAimAction(false);
    }
    public void EndingScene(){
        stageHUDObject.SetActive(false);
        chatUIObject.SetActive(false);
        loadingObject.SetActive(false);
    }

}
