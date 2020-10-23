using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestData : MonoBehaviour
{
    // 퀘스트 내용은 씬당 2개 내외
    // 총 16씬 퀘스트 개수 32개 내외
    
    // 1. 딕셔너리 생성(키 값에 따라 퀘스트 내용 저장)
    // 2. 씬 넘버 당 딕셔너리 키값 저장
    // 3. 씬 시작 시 씬 넘버 당 키 값으로 퀘스트 불러오기
    // 4. 퀘스트 패널에 불러온 퀘스트 표시
    // 5. 씬 전환 시 기존 퀘스트 텍스트를 지움

    // 스크립트 적용 방법
    // 1. Canvas에 퀘스트 용 패널을 만든다.
    // 2. 패널에 Layout~Vertical, content~Fitter 컴포넌트 삽입
    // 3. 패널에 임시 퀘스트 텍스트를 작성하고 프리팹으로 만든 후 삭제
    // 4. ChatManager에 이 스크립트를 적용한다.
    // 5. questPanel에 패널 할당, 프리팹에 텍스트 프리팹 할당
    
    public GameObject questPanel;                                                                 // 퀘스트 내용을 자식으로 달아 둘 퀘스트 패널 선언
    public Text questPrefab;                                                                      // 퀘스트 내용을 담아서 보여줄 텍스트 프리팹
    public Dictionary<int, string[]> QuestDic = new Dictionary<int, string[]>();                  // 퀘스트 내용을 담아 놓을 딕셔너리 생성

    private void Awake() {
      GenerateQuest();
    }
    private void Start() {
      ShowQuest();
    }

    void GenerateQuest()
    {
      Debug.Log("Generate");
      // 퀘스트 딕셔너리에 값(코드당 퀘스트 내용) 삽입
      // 빌드 넘버에 따라 값 int 값 바꾸기
      QuestDic.Add(1, new string[] {"* 이것은 퀘스트 내용 입니다리"});
      QuestDic.Add(2, new string[] {"2"});
      QuestDic.Add(3, new string[] {"3"});
      QuestDic.Add(4, new string[] {"4"});
      QuestDic.Add(5, new string[] {"5"});
      QuestDic.Add(6, new string[] {"6"});
      QuestDic.Add(7, new string[] {"7"});
      QuestDic.Add(8, new string[] {"8"});
      QuestDic.Add(9, new string[] {"9"});
      QuestDic.Add(10, new string[] {"10"});
      QuestDic.Add(11, new string[] {"11"});
      QuestDic.Add(12, new string[] {"12"});
      QuestDic.Add(13, new string[] {"13"});
      QuestDic.Add(14, new string[] {"14"});
      QuestDic.Add(15, new string[] {"15"});
      QuestDic.Add(16, new string[] {"16"});
    }

    public void ShowQuest()
    { // 씬 넘버에 따라 퀘스트 내용을 보여줌
      int code = SceneManager.GetActiveScene().buildIndex;

      for(int i=0; i<QuestDic[code].Length; i++)
      {
        var quest = Instantiate(questPrefab).GetComponent<Text>();
        quest.text = QuestDic[code][i];
        quest.gameObject.SetActive(true);
        quest.transform.SetParent(questPanel.transform);
      }
    }
}
