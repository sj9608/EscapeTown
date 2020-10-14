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
    
    void Start()
    {
      numOfPotion = 0;
      numOfMagazine = 2;   
    }

    public void UsePotion()
    {
      if(numOfPotion > 0)
      {
        numOfPotion--;
      }
    }

    public void UseMagazine()
    {
      if(numOfMagazine > 0)
      {
        numOfMagazine--;
      }
    }
}
