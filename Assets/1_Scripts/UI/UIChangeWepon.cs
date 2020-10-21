using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChangeWepon : MonoBehaviour
{
    PlayerAttack playerAttack;
    public GameObject Knife;
    public GameObject Gun;
    Image Selected_Gun;
    Image Selected_Knife;


    void Start()
    {
        Gun.SetActive(false);
        Knife.SetActive(false);

        Selected_Gun = Gun.GetComponent<Image>();
        Selected_Knife = Knife.GetComponent<Image>();
    }

    void Update()
    {
        Show_UsedWeapon();
    }

    void Show_UsedWeapon()
    {
        Color Gun_alpha = Selected_Gun.color;
        Color Knife_alpha = Selected_Knife.color;

        
        if(!playerAttack.isGun)
        {
            Debug.Log("************* I'm Working at Selected_Gun! **************");
            Gun.SetActive(true);
            Knife.SetActive(true);
            Knife_alpha.a = 0.3f;
            Selected_Knife.color = Knife_alpha;
            Gun_alpha.a = 1f;
            Selected_Gun.color = Gun_alpha;
        }
            
        else
        {
            Debug.Log("************* I'm Working at Selected_Knife! **************");
            Gun.SetActive(true);
            Knife.SetActive(true);
            Gun_alpha.a = 0.3f;
            Selected_Gun.color = Gun_alpha;
            Knife_alpha.a = 1f;
            Selected_Knife.color = Knife_alpha;
        }
           
    }
}
