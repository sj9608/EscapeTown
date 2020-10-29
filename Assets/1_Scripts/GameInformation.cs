using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameInformation : SingletonBase<GameInformation>
{
    // ---------- 게임 ----------
    // 현재 씬 번호
    // 대화 정보
    // ---------- 플레이어 ----------
    // HP
    // 총탄 수
    // 잔탄 수
    // ---------- 퀵슬롯 ----------
    // 보유 포션 수
    // 보유 탄창 수

    // MAX HP
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

    public event UnityAction UpdateHpAction;
    public event UnityAction<int> UpdateSceneAction;
    public event UnityAction UpdateCurAmmoAction;
    public event UnityAction UpdateRemainAmmoAction;
    public event UnityAction UpdateGetPotionAction;
    public event UnityAction UpdateUsePotionAction;

    private void Awake()
    {
        SCI = SceneController.Instance;
        CMI = ChatManager.Instance;
        HP = 100;       // 저
        RemainAmmo = 0;  // 총 총알 수 
        NumOfPotion = 0;
        CurAmmo = 30;
    }

    public void GameInformationInit(GameData gameData)
    {
        if (gameData == null)
        {
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

    public void UpdateHp(float changeValue)        //HP회복/차감
    {
        HP += changeValue;
        if (UpdateHpAction != null) 
            UpdateHpAction();
    }
    public void UpdateCurAmmo()         // 총에 남아있는 총알 수 
    {
        if (UpdateCurAmmoAction != null)
        {
            UpdateCurAmmoAction();
        }
    }
    public void UpdatePotion(int change)       //포션 획득
    {
        NumOfPotion += change;
        if (change > 0)
        {
            if (UpdateGetPotionAction != null)
                UpdateGetPotionAction();
        }
        else if (change < 0)
        {
            if (UpdateUsePotionAction != null)
            {
                UpdateUsePotionAction();
            }
        }
    }
}
