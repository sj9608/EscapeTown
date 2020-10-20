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
    public int RemainAmmo { get; private set; }     // 총탄 수
    public int CurAmmo { get; private set; }        // 잔탄 수
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
    public void GunReload(int ammoToFill)
    {
        // 탄창에 채워야할 탄약이 남은 탄약보다 많다면,
        // 채워야할 탄약 수를 남은 탄약 수에 맞춰 줄인다
        if (RemainAmmo < ammoToFill)
        {
            ammoToFill = RemainAmmo;
        }

        // 탄창을 채운다
        CurAmmo += ammoToFill;
        // 남은 탄약에서, 탄창에 채운만큼 탄약을 뺸다
        RemainAmmo -= ammoToFill;

        UpdateCurAmmo();
    }

    public void GunFire()
    {
        CurAmmo--;

        UpdateCurAmmo();
    }
}
