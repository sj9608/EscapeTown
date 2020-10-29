using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ************* 아이템 획득 시 뜨는 팝업창에 대한 코드 ****************
public class UILogPopUpWindow : MonoBehaviour
{
    GameInformation GI;
    public GameObject potion_LogWindow;
    public GameObject magazine_LogWindow;
    RectTransform rectTransform_1;
    RectTransform rectTransform_2;
    Vector3 target = new Vector3(1850, 0, 0);
    float speed = 0.3f;
    float time = 0f;
    bool isShow = false;   // 현재 로그창이 떠있는지 안떠있는지 여부

    private void Awake() {
        GI = GameInformation.Instance;
    }
    private void OnEnable() {
        GI.UpdateRemainAmmoAction += GetMagazineLog;
        GI.UpdateGetPotionAction += GetPotionLog;
    }
    private void OnDisable() {
        GI.UpdateRemainAmmoAction -= GetMagazineLog;
        GI.UpdateGetPotionAction -= GetPotionLog;
    }
    void Start()
    {
        rectTransform_1 = potion_LogWindow.GetComponent<RectTransform>();
        rectTransform_2 = magazine_LogWindow.GetComponent<RectTransform>();
        potion_LogWindow.SetActive(false);
        magazine_LogWindow.SetActive(false);
    }
    private void GetMagazineLog(){
        StartCoroutine(IELogMagazinePopUp());
    }
    private void GetPotionLog(){
        StartCoroutine(IELogPotionPopUp());
    }
    IEnumerator IELogMagazinePopUp()
    {
        magazine_LogWindow.SetActive(true);
        isShow = true;

        for (int i = 0; i < 10; ++i)
        {
            float a = Vector3.Distance(rectTransform_2.position, target) / 10;
            rectTransform_2.localPosition += new Vector3(-a, 0f, 0f); // 현 위치, 목표지점, 참조속도, 속도

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        magazine_LogWindow.SetActive(false);
        isShow = false;
        rectTransform_2.localPosition = new Vector3(1090, -303, 0);
    }
    IEnumerator IELogPotionPopUp()
    {
        potion_LogWindow.SetActive(true);
        isShow = true;

        for (int i = 0; i < 10; ++i)
        {
            float a = Vector3.Distance(rectTransform_1.position, target) / 10;
            rectTransform_1.localPosition += new Vector3(-a, 0f, 0f); // 현 위치, 목표지점, 참조속도, 속도

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        potion_LogWindow.SetActive(false);
        isShow = false;
        rectTransform_1.localPosition = new Vector3(1090, -303, 0);
    }
}
