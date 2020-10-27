using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    public GameObject option;
    public AudioSource bgmsource;
    public AudioSource gameSound_Source;
    public Button btn_Back;
    bool isShow = false;

    void Update()
    {
        Popup_Option();
    }

    public void Popup_Option()
    {
       if(Input.GetKeyDown(KeyCode.Escape) && isShow == false)
       {
           option.SetActive(false);
           isShow = false;
       }
    //    else
    //    {
    //        isShow  = true;
    //        option.SetActive(true);
    //    }
    }
    public void SetBGMVolume(float volume)
    {
        bgmsource.volume = volume;
    }

    public void SetGameVolume(float volume)
    {
        gameSound_Source.volume = volume;
    }
    public void BTN_Back()
    {
        option.SetActive(false);
    }
}


