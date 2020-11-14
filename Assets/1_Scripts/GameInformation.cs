using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInformation : SingletonBase<GameInformation>
{
    // 게임 정보 관리 클래스
    // 게임 내 수치들을 관리한다
    // 최대 HP, 현재 HP, 총 탄수, 총에 장전 된 탄수, 포션 개수, 탄창 개수(삭제)
    GameManager GMI;
    SceneController SCI;
    ChatManager CMI;

    public float MAX_HP = 100;
    public float HP { get; private set; }           // HP
    private int remainAmmo;
    public int RemainAmmo
    {
        get
        {
            return remainAmmo;
        }
        set
        {
            remainAmmo = value;

            if (UpdateRemainAmmoAction != null) UpdateRemainAmmoAction();
        }
    }     // 총탄 수
    private int curAmmo;
    public int CurAmmo
    {
        get
        {
            return curAmmo;
        }
        set
        {
            curAmmo = value;

            UpdateCurAmmo();
        }
    }        // 잔탄 수
    public int NumOfPotion { get; set; }            // 보유 포션 수
    public int NumOfMagazine { get; set; }          // 보유 탄창 수
    // 기능 동작 후 보여질 UI 메서드를 담을 UnityAction
    // Player HP 변화(데미지 / 포션)
    public event UnityAction UpdateHpAction;
    // 씬 전환(삭제예정)
    public event UnityAction<int> UpdateSceneAction;
    // 현재 탄수 변화(탄 발사 / 재장전)
    public event UnityAction UpdateCurAmmoAction;
    // 총 탄수 변화(탄창 습득 / 재장전)
    public event UnityAction UpdateRemainAmmoAction;
    // 포션 아이템 습득
    public event UnityAction UpdateGetPotionAction;
    // 포션 아이템 사용
    public event UnityAction UpdateUsePotionAction;

    private void Awake()
    {
        SCI = SceneController.Instance;
        CMI = ChatManager.Instance;
        HP = 100;
        RemainAmmo = 0;  // 총 총알 수 
        NumOfPotion = 0;
        CurAmmo = 30;
    }
    // 게임 수치 초기화
    // 새 게임시 씬 로드시
    // GameData 사용
    // GameData null 가능
    // 게임 새로 시작 시 null
    // 다른 경우는 gameData에 loadData 존재
    public void GameInformationInit(GameData gameData)
    {
        if (gameData == null)
        {
            // 새로하기 시 게임 수치 초기화
            gameData = new GameData(2, 100, 0, 30, 0, new int[100], 0);
        }
        SCI.CurSceneNum = gameData.saveSceneNum;
        
        HP = gameData.saveHP;
        RemainAmmo = gameData.saveRemainAmmo;
        CurAmmo = gameData.saveCurAmmo;
        NumOfPotion = gameData.saveNumOfPotion;

        CMI.chatArray = gameData.saveChatArray;
        CMI.chatNumber = gameData.saveChatNumber;
        
    }
    // HP 변화시킬 외부 접근용 함수
    // changeValue -100.0 ~ 100
    public void UpdateHp(float changeValue)        //HP회복/차감
    {
        HP += changeValue;
        if (UpdateHpAction != null) 
            UpdateHpAction();
    }
    // 잔탄수 UI 변화시킬 함수
    // property에서 값 변화 후 UI Action 호출 함수
    public void UpdateCurAmmo()         // 총에 남아있는 총알 수 
    {
        if (UpdateCurAmmoAction != null)
        {
            UpdateCurAmmoAction();
        }
    }
    // 포션 습득 사용 시 개수 및 UI 변화시킬 함수 
    public void UpdatePotion(int change)       //포션 획득
    {
        NumOfPotion += change;
        if (change > 0)
        {
            // 습득 시
            if (UpdateGetPotionAction != null)
                UpdateGetPotionAction();
        }
        else if (change < 0)
        {
            // 사용 시
            if (UpdateUsePotionAction != null)
            {
                UpdateUsePotionAction();
            }
        }
    }
}
