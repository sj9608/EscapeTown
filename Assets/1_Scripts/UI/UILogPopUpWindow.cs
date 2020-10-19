using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ************* 아이템 획득 시 뜨는 팝업창에 대한 코드 ****************
public class UILogPopUpWindow : MonoBehaviour
{
    public GameObject logwindow;
    public RectTransform rectTransform;                    
    Vector3 velo = Vector3.zero;
    Vector3 target = new Vector3(870, -270, 0);
    float speed = 0.1f;

    void Start()
    {
        rectTransform = logwindow.GetComponent<RectTransform>();
        logwindow.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha3))
        { 
            LogPopUp();
        }
            
    }
    void LogPopUp()
    {
        logwindow.SetActive(true);
        
        rectTransform.position = Vector3.SmoothDamp(transform.position, target, ref velo, speed); // 현 위치, 목표지점, 참조속도, 속도
                                                                                                  // ref : 참조접근 -> 실시간으로 바뀌는 값 적용 가능
        
        //logwindow.SetActive(false);
    }
}
