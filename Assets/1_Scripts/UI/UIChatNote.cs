using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // 대화 수첩 화면 UI
    // E키가 눌릴 때 마다 대화 수첩 열기 토글
    // 대화 수첩 스크롤 뷰에 할당되는 스크립트

    bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        isOpen = GameManager.Instance.gameKeyInput.openChatNote;
        if(isOpen == false) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }
}
