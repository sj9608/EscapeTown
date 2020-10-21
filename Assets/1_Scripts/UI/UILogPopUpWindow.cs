using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ************* 아이템 획득 시 뜨는 팝업창에 대한 코드 ****************
public class UILogPopUpWindow : MonoBehaviour
{
    public GameObject potion_LogWindow;
    public GameObject magazine_LogWindow;
    RectTransform rectTransform_1;
    RectTransform rectTransform_2;
    Vector3 target = new Vector3(1036, 0, 0);
    float speed = 0.3f;
    float time = 0f;

    void Start()
    {
        rectTransform_1 = potion_LogWindow.GetComponent<RectTransform>();
        rectTransform_2 = magazine_LogWindow.GetComponent<RectTransform>();
        potion_LogWindow.SetActive(false);
        magazine_LogWindow.SetActive(false);
    }

    void Update()
    {
        
            StartCoroutine(IELogPopUp());
    

    }
    IEnumerator IELogPopUp()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            potion_LogWindow.SetActive(true);

        for (int i = 0; i < 10; ++i)
        {
            float a = Vector3.Distance(rectTransform_1.position, target) / 10;
            rectTransform_1.localPosition += new Vector3(-a, 0f, 0f); // 현 위치, 목표지점, 참조속도, 속도
            Debug.Log(a);                                                                                   // ref : 참조접근 -> 실시간으로 바뀌는 값 적용 가능

            yield return null;
        } 
        yield return new WaitForSeconds(1f);
        potion_LogWindow.SetActive(false);
        rectTransform_1.localPosition = new Vector3(1090,-303,0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            magazine_LogWindow.SetActive(true);

        for (int i = 0; i < 10; ++i)
        {
            float a = Vector3.Distance(rectTransform_2.position, target) / 10;
            rectTransform_2.localPosition += new Vector3(-a, 0f, 0f); // 현 위치, 목표지점, 참조속도, 속도
            Debug.Log(a);                                                                                   // ref : 참조접근 -> 실시간으로 바뀌는 값 적용 가능

            yield return null;
        } 
        yield return new WaitForSeconds(1f);
        magazine_LogWindow.SetActive(false);
        rectTransform_2.localPosition = new Vector3(1090,-303,0);
        }
    }
}
