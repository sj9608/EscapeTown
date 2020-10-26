using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    public GameObject option;
    public GameObject menuSet;
    public AudioSource bgmsource;
    public AudioSource gameSound_Source;
    public Button btn_Back;

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
        menuSet.SetActive(true);
    }
}


