using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // 대화 수첩 화면 UI
    // E 키를 누르면 현재까지 나온 대화의 모든 내용이 txt로 나옴
    // 마우스 휠로 내용을 살펴봄
    // ChatController에서 나온 내용을 ChatNote로 저장
    // E 키 누르면 창 닫힘

    bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        isOpen = GameManager.Instance.gameKeyInput.openChatNote;
        if(isOpen == false) return;
    }
}
