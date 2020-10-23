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
    public const float MAX_HP = 100;
    public int CurSceneNum { get; set; }            // 현재 씬 번호
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

            if (UpdateCurAmmoAction != null) UpdateCurAmmoAction();
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

            if (UpdateCurAmmoAction != null) UpdateCurAmmoAction();
        }
    }        // 잔탄 수
    public int NumOfPotion { get; set; }            // 보유 포션 수
    public int NumOfMagazine { get; set; }          // 보유 탄창 수

    public event UnityAction<float, float> UpdateHpAction;
    public event UnityAction<int> UpdateSceneAction;
    public event UnityAction UpdateCurAmmoAction;
    public event UnityAction<int> UpdatePotionAction;
    public event UnityAction<int> UpdateMagazineAction;

    private void Awake()
    {
        HP = 100;       // 저
        RemainAmmo = 60;  // 총 총알 수 
        NumOfPotion = 5;
        CurAmmo = 20;

    }

    public void UpdateHp(float changeValue)        //HP회복/차감
    {
        if (UpdateHpAction != null) 
            UpdateHpAction(HP, changeValue);
    }
    public void UpdateScene(int changeSceneNum)
    {
        if (UpdateSceneAction != null)
        {
            UpdateSceneAction(changeSceneNum);
        }
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

        if (UpdatePotionAction != null)
        {
            UpdatePotionAction(NumOfPotion);
        }
    }
    public void UpdateMagazine(int change)  // 갖고 있는 총 총알 수(총알 획득/장전 시 업데이트)
    {
        NumOfMagazine += change;

        if (UpdateMagazineAction != null)
        {
            UpdateMagazineAction(NumOfMagazine);
        }
    }
}
