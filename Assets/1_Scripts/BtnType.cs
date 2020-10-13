using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    public Vector3 defaultScale;

    void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    
    public void OnBtnClick()
    {
        switch(currentType)
        {
            case BTNType.Start :
                SceneLoader.LoadSceneHandle("Loading",0);
                break; 
            
            case BTNType.Exit :
                SceneManager.UnloadScene("Main");
                Debug.Log("게임종료");
                break;

        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}