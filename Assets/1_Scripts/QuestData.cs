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
    int sceneNum = 0; // 현재 씬 넘버 & 퀘스트 코드

    private void Awake() {
      GenerateQuest();
    }

    void GenerateQuest()
    {
      // 퀘스트 딕셔너리에 값(코드당 퀘스트 내용) 삽입
      // 빌드 넘버에 따라 값 int 값 바꾸기
      
      QuestDic.Add(0, new string[] {"* 메인."});
      QuestDic.Add(1, new string[] {"* "});
      QuestDic.Add(2, new string[] {"* 밖으로 나가세요."});
      QuestDic.Add(3, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(4, new string[] {"* 모든 주민과 대화하세요.", "* 밤이 될 때까지 마을을 탐색하세요."});
      QuestDic.Add(5, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(6, new string[] {"* 모든 주민과 대화하세요.", "* 밤이 될 때까지 마을을 탐색하세요."});
      QuestDic.Add(7, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(8, new string[] {"* 모든 주민과 대화하세요.", "* 밤이 될 때까지 건물을 탐색하세요."});
      QuestDic.Add(9, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(10, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(11, new string[] {"* 모든 주민과 대화하세요.", "* 밤이 될 때까지 마을을 탐색하세요."});
      QuestDic.Add(12, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(13, new string[] {"* 모든 주민과 대화하세요.", "* 산지기를 찾으세요.", "* 마을을 탐색하세요."});
      QuestDic.Add(14, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(15, new string[] {"* 모든 좀비를 처치하세요.", "* 다음 장소로 이동하세요."});
      QuestDic.Add(16, new string[] {"* 박사는 약물로 신체를 강화한 인물입니다.", "* 박사를 물리치고 마을을 탈출하세요."});
    
      ShowQuest(sceneNum);
    }

    public void ShowQuest(int sceneNum)
    { // 씬 넘버에 따라 퀘스트 내용을 보여줌
      if(questPanel != null)
      { // 자식이 생성되어 있다면 초기화
        for(int i=0; i<questPanel.transform.childCount; i++)
        {
          var child = questPanel.transform.GetChild(i);
          child.gameObject.SetActive(false);
        }
      }

      for(int i=0; i<QuestDic[sceneNum].Length; i++)
      {// 씬 넘버에 따라 퀘스트 내용 출력
        var quest = Instantiate(questPrefab).GetComponent<Text>();
        quest.text = QuestDic[sceneNum][i];
        quest.gameObject.SetActive(true);
        quest.transform.SetParent(questPanel.transform);
      }
    }
}
