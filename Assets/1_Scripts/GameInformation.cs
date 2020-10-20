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
    public int RemainAmmo;     // 총탄 수
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
        RemainAmmo = 0;
        CurAmmo = 30;
    }

    public void UpdateHp(float changeValue)
    {
        if (UpdateHpAction != null) UpdateHpAction(HP, changeValue);
    }
    public void UpdateScene(int changeSceneNum)
    {
        if (UpdateSceneAction != null)
        {
            UpdateSceneAction(changeSceneNum);
        }
    }
    public void UpdateCurAmmo()
    {
        if (UpdateCurAmmoAction != null)
        {
            UpdateCurAmmoAction();
        }
    }
    public void UpdatePotion(int change)
    {
        if (UpdatePotionAction != null)
        {
            UpdatePotionAction(change);
        }
    }
    public void UpdateMagazine(int change)
    {
        if (UpdatePotionAction != null)
        {
            UpdatePotionAction(change);
        }
    }

    // 남은 탄약을 추가하는 메서드
    public void GunAddAmmo(int ammo)
    {
        RemainAmmo += ammo;

        UpdateCurAmmo();
    }
}
