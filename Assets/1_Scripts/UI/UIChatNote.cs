using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChatNote : MonoBehaviour
{
    // 대화 수첩 화면 UI
    // E키가 눌릴 때 마다 대화 수첩 열기 토글
    // 대화 수첩 스크롤 뷰에 할당되는 스크립트

    // 오브젝트 풀
    public Text poolingObjectPrefab;                                  // 풀링에 사용할 텍스트 프리팹
    
    // 에디터의 오브젝트
    public GameObject scrollView;                                     // 대화 수첩을 보여줄 스크롤 뷰
    public GameObject parentObject;                                   // 텍스트 프리팹이 자식으로 들어갈 스크롤 뷰의 Content

    bool isOpen;                                                      // 대화 수첩의 상태(열림/닫힘) 
    bool[] isUse = new bool[100];                                     // id에 해당하는 대화 내용을 생성 했는지 확인하는 진리 값


    AudioSource audioSource;
    public AudioClip audioTurnOfPage;

    void Start()
    {
        // 대화 수첩 닫힘 상태가 디폴트
        isOpen = false;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isLoading)
        {
            return;
        }
        // E키를 누를 때 마다 열림/닫힘 상태 토글
        if(Input.GetKeyDown(KeyCode.T))
        {
            isOpen = !isOpen;
            audioSource.PlayOneShot(audioTurnOfPage);
        }
        
        if(isOpen == false)
        {   // 닫힘 상태일 때 노트를 닫음
            scrollView.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // CloseChatNote();
        } 
        else 
        {   // 열림 상태일 때 노트를 열음
            scrollView.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            OpenChatNote(); 
        }
    }

    public void OpenChatNote()
    {
        // 열림 상태에서 chatArray에 저장된 id 에 해당하는 대화 내용을 가진 텍스트 내용을 프리팹에 넣어 활성화
        // 프리팹에 넣어서 content 밑에 자식으로 텍스트를 넣을 때 위치를 지정해서 삽입
        for(int j=0; j<ChatManager.Instance.chatNumber; j++)
        {
            // 만약 대화 내용 텍스트가 생성되어 있지 않다면
            if(!isUse[j])
            {
                CreateNewText(ChatManager.Instance.chatArray[j]); // 배열에 저장된 id를 넘겨줘서 id에 맞는 대화 내용 텍스트 생성
                isUse[j] = true;
            }
        }   
    }
    
    // Object pulling 삽입/사용/리턴 메서드
    void CreateNewText(int id)
    {   
        // content에 해당 스크립트를 할당시켜 content의 자식으로 Text가 들어오게 함
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Text>();
        newObj.text = ChatManager.Instance.talkCharacterData[id] + "\n " + ChatManager.Instance.talkData[id];
        newObj.gameObject.SetActive(true);
        newObj.transform.SetParent(parentObject.transform);
    }
}
