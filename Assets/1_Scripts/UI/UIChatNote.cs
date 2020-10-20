using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    // 대화 수첩 화면 UI
    // E키가 눌릴 때 마다 대화 수첩 열기 토글
    // 대화 수첩 스크롤 뷰에 할당되는 스크립트

    Queue<Text> poolingTempQueue = new Queue<Text>();

    bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        isOpen = Input.GetKeyDown(KeyCode.E);
        if(isOpen == false)
        {
            if(poolingTempQueue.Count > 0)
            {   // 임시 풀링 큐에 내용이 들어가 있을 때 대화 수첩을 닫기
                gameObject.SetActive(false);
                CloseChatNote();
            }
        } 
        else 
        {
            if(poolingTempQueue.Count == 0)
            {
                // 임시 풀링 큐에 내용이 들어가 있지 않으면 대화 수첩 열기 
                gameObject.SetActive(true);
                OpenChatNote(); 
            }
        }
        
    }

    public void OpenChatNote()
    {
        // 풀 큐에서 지금껏 나온 대화 수 만큼 대화를 꺼내서 출력한다.
        while(ChatManager.poolingObjectQueue.Count > 0)
        {
            poolingTempQueue.Enqueue(ChatManager.GetText());
        }
    }

    public void CloseChatNote()
    {
        // 출력한 대화를 다시 풀 큐에 리턴한다.
        while(poolingTempQueue.Count > 0)
        {
            ChatManager.poolingObjectQueue.Enqueue(poolingTempQueue.Dequeue());
        }
    }
}
