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
    UIPause uipause;
    public bool isShow = false;

    void Start()
    {
        uipause =  GetComponent<UIPause>();
    }
    void Update()
    {
        Popup_Option();
    }

    public void Popup_Option()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isLoading)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && uipause.ispopup == true)
        {
            option.SetActive(false);
            GameManager.Instance.isPopupOn = true;
            uipause.menuSet.SetActive(true);
            isShow = false;
        }
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
            Debug.Log("working back button!");
            GameManager.Instance.isPopupOn = true;
            uipause.menuSet.SetActive(true);
            Debug.Log("working pannel");
            isShow = false;
    }
}


