using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
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

    GameInformation GI;

    void Start()
    {
        GI = GameInformation.Instance;

        numOfPotion = GameInformation.Instance.NumOfPotion;
        numOfMagazine = GameInformation.Instance.RemainAmmo;
        isUsed = false;
        numOfPotion_text.text = numOfPotion.ToString();
        numOfMagazine_text.text = numOfMagazine.ToString();

        //GameInformation.Instance.UpdateMagazineAction += GetMagazine;
        //GameInformation.Instance.UpdatePotionAction += GetPotion;
    }

    void Update()
    {
        // UsePotion();
        // UseMagazine();
    }

    public void UsePotion(int numOfPotion)
    {
        if (isUsed == false)
        {   
            numOfPotion_text.text = numOfPotion.ToString();
            Potion_CoolTime();
        }
    }

    public void GetPotion(int numOfPotion)
    {
        numOfPotion_text.text = numOfPotion.ToString();
    }

    //public void UseMagazine(int numOfMagazine)
    //{
    //    if (isUsed == false)
    //    {
    //        numOfPotion_text.text = numOfPotion.ToString();
    //        Bullet_CoolTime();
    //    }
        
    //}
    void GetMagazine(int numOfMagazine)
    {
        numOfMagazine_text.text = numOfMagazine.ToString();
    }

    public void Potion_CoolTime() // 아이템 사용시 쿨타임 표시 UI(쿨 타임 동안 퀵슬롯 이미지 로드)
    {
        // if (isUsed == true)
        // {
            isUsed = true;
            StartCoroutine(CoolTime(3f)); // 쿨타임 3초
            IEnumerator CoolTime(float coolTime) // 코루틴
            {
                float curTime = 0f;
                while (curTime < coolTime)
                {
                    curTime += Time.deltaTime;            // 쿨타임에서 deltaTime을 빼 적용
                    IMG_potion.fillAmount = curTime / coolTime; // 쿨타임 동안 magazine 이미지를 수직으로 다시 그리기

                    yield return new WaitForFixedUpdate();
                }
                isUsed = false;
            // }
        }
    }
    //public void Bullet_CoolTime()
    //{
    //    // if (isUsed == true)
    //    // {
    //        isUsed = true;
    //        StartCoroutine(CoolTime(2f)); // 쿨타임 3초
    //        IEnumerator CoolTime(float coolTime) // 코루틴
    //        {
    //            float curTime = 0f;
    //            while (curTime < coolTime)
    //            {
    //                curTime += Time.deltaTime;            // 쿨타임에서 deltaTime을 빼 적용
    //                IMG_magazine.fillAmount = curTime / coolTime; // 쿨타임 동안 magazine 이미지를 수직으로 다시 그리기

    //                yield return new WaitForFixedUpdate();
    //            }
    //            isUsed = false;
    //        // }
    //    }    
    //}
}

