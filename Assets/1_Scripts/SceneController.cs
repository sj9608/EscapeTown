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

    public FadeController fader;
    public GameObject loadingObject;
    // Start is called before the first frame update
    void Start()
    {
        curSceneNum = 3;
        loadingObject.SetActive(true);
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
            SceneManager.LoadSceneAsync(curSceneNum, LoadSceneMode.Additive);
        });
    }


}
