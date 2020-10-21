// //https://www.youtube.com/watch?v=RwndWebxbmo
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class QuestManager : MonoBehaviour
// {
//     public int questId;                     // 현재 진행중인 퀘스트 아이디
//     public int questActionIndex;            // 퀘스트 대화순서
//     Dictionary<int, QuestData> qeustList;   // 퀘스트 데이터를 저장할 Dictionary 변수
//     void Awake()
//     {
//         qeustList = new Dictionary<int, QuestData>();
//         GenerateData();
//     }

//     // Update is called once per frame
//     void GenerateData()
//     {
//         qeustList.Add(10, new QuestData("첫 대화",
//                                         new int[] { 1000, 2000 }));  //10 : 퀘스트아이디
//     }

//     public int GetQuestTalkIndex(int id)        // npcId를 받아 GerenateData의 퀘스트 내용을 내보냄
//     {
//         return questId + questActionIndex; 
//     }

//     public void CheckQuest(int id)                    // 퀘스트를 순서대로 완료했는지 체크
//     {
//         if(id == qeustList[questId].npcId[qeustActionIndex])
//             qeustActionIndex ++;
//         ControlObject();
//         if(qeustActionIndex == qeustList[questId].npcId.Length)
//             NextQeust();
        
//         return questList[qeustId].questName;
//     }
    
//     void NextQeust()
//     {
//         questId += 10;
//         qeustActionIndex = 0;
//     }

//     void ControlObject()
//     {
//         switch(questId)
//         {
//             case 10 : 
//                 if(questActionIndex == 2)
//                 qeustObject[0].SetActive(true);
//             break;
//             case 20 : 
//                 if(questActionIndex == 1)
//                 qeustObject[0].SetActive(false);
//             break;
//         }
//     }
// }
