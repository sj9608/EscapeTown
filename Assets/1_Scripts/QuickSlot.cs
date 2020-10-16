using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSlot : SingletonBase<QuickSlot>
{
    // 포션, 탄창, 포션 개수, 탄창 개수

    public GameObject potion;
    public GameObject magazine;
    public int numOfPotion;
    public int numOfMagazine;
    public Text numOfPotion_text;
    public Text numOfMagazine_text;

    void Start()
    {
        numOfPotion = 0;
        numOfMagazine = 2;
        numOfPotion_text = numOfPotion.ToString();
        numOfMagazine_text = numOfMagazine.ToString();
    }

    public void UsePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (numOfPotion > 0)
            {
                numOfPotion--;
                numOfPotion_text = numOfPotion.ToString();
            }
        }

    }

    public void UseMagazine()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (numOfMagazine > 0)
            {
                numOfMagazine--;
                numOfMagazine_text = numOfMagazine.ToString();
            }
        }
    }
}
