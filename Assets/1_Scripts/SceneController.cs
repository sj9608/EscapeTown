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
    // Start is called before the first frame update
    void Start()
    {
        curSceneNum = 0;
        stageHUDObject.SetActive(false);
        chatUIObject.SetActive(false);
        loadingObject.SetActive(false);
        SceneManager.LoadSceneAsync(curSceneNum, LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            NextSecne();
        }
    }

    public void NextSecne()
    {
        StartCoroutine(IENextScene());
    }

    IEnumerator IENextScene()
    {
        PopupChange(true);
        AsyncOperation unloadAsync = SceneManager.UnloadSceneAsync(curSceneNum);
        curSceneNum++;

        yield return new WaitUntil(() => { return unloadAsync.isDone; });

        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(curSceneNum, LoadSceneMode.Additive);
        LoadingScene loadingScene = loadingObject.GetComponent<LoadingScene>();
        loadingScene.ShowLoading(loadAsync);
        // yield return new WaitForSeconds(1f);

        yield return new WaitUntil(() => { return loadAsync.isDone; });
        PopupChange(false);
    }

    void PopupChange(bool isLoading)
    {
        stageHUDObject.SetActive(!isLoading);
        chatUIObject.SetActive(!isLoading);
        loadingObject.SetActive(isLoading);
    }

}
