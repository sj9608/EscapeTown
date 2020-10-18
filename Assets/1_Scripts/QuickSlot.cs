using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : SingletonBase<QuickSlot>
{
    // 포션, 탄창, 포션 개수, 탄창 개수

    public Image IMG_potion;
    public Image IMG_magazine;
    // public GameObejct potion;
    // public GameObject maagzine;
    public int numOfPotion;
    public int numOfMagazine;
    public Text numOfPotion_text;
    public Text numOfMagazine_text;
    private bool isUsed;
    void Start()
    {
        numOfPotion = 1;
        numOfMagazine = 2;
        isUsed = false;
        numOfPotion_text.text = numOfPotion.ToString();
        numOfMagazine_text.text = numOfMagazine.ToString();
    }

    void Update()
    {
        UsePotion();
        UseMagazine();
    }

    public void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && isUsed == false)
        {
            isUsed = !isUsed;
            if (numOfPotion > 0 && isUsed == true)
            {
                numOfPotion--;
                numOfPotion_text.text = numOfPotion.ToString();
                Show_CoolTime();
            }
        }
        // if()
        // {}

    }

    public void UseMagazine()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2) && isUsed == false)
        {
            isUsed = !isUsed;
            if (numOfMagazine > 0 && isUsed == true)
            {
                numOfMagazine--;
                numOfMagazine_text.text = numOfMagazine.ToString();
                Show_CoolTime();
            }
        }
    }

    public void Show_CoolTime() // 아이템 사용시 쿨타임 표시 UI(쿨 타임 동안 퀵슬롯 이미지 로드)
    {
        StartCoroutine(CoolTime(3f)); // 쿨타임 4초
        IEnumerator CoolTime(float coolTime) // 코루틴
        {
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            // {
            while (coolTime > 1f)
            {
                coolTime -= Time.deltaTime;            // 쿨타임에서 deltaTime을 빼 적용
                IMG_magazine.fillAmount = (1f / coolTime); // 쿨타임 동안 magazine 이미지를 수직으로 다시 그리기
                                                           //}
                yield return new WaitForFixedUpdate();
            }
            isUsed = false;
        }
    }
}
