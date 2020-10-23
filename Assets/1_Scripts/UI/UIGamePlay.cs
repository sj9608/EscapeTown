using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    // 게임 플레이 화면 UI
    // 퀵슬롯, HP바, 퀘스트 목록, 시스템 로그, 총알 수, 대화 텍스트
    
    public Text numOfPotion;           // 퀵슬롯 변수
    public Text numOfBullet;        
    public float maxHP = 100;           // HP 변수
    public float curHP;
    
    // 퀘스트 목록 관련 변수
    
    // 시스템 로그 관련 변수

    //public Text remainBullet;            // 총 탄 수
    public Text curBullet;               // 잔탄 수

    public GameObject crossHair;


    //public ChatController chatController;      // 대화 텍스트
    public delegate void battle_Event();

    //int cur_Bullet;
    //int magCapacity;

    int cur_Bullet;
    // int remainBullet;
    [SerializeField] private Slider hpbar; // HP바

    private void Start() 
    {
        // numOfPotion 값 불러오기
        
        // numOfBullet 값 불러오기

        // curHP 값 불러오기
        hpbar.value = GameInformation.Instance.HP;
        
        // 총 탄 수 / 잔 탄 수 불러오기
        curBullet.text =  GameInformation.Instance.CurAmmo.ToString(); // 잔탄 수;
        //remainBullet.text = GameInformation.Instance.RemainAmmo.ToString(); // 한 탄창에 남은 총알 수
        //show_CrossHair = crossHair.GetComponent<GameObject>();

        crossHair.SetActive(false);                        
    }

    private void Update() 
    {
        // numOfPotion 값 불러오기
        // 

        // numOfBullet 값 불러오기
        // UI 반영

        // curHP 값 불러오기
        HandleHP();

        // 총 탄 수 / 잔 탄 수 불러오기
        Show_Bullet_Count();
        Show_CrossHair();
    }


    public void Show_Bullet_Count()  // 장전 후 사용하고 남은 총알 갯수
    {
        // curBullet : 현재 총알수, magCapacity : 최대 총알 수(30발)
        cur_Bullet = GameInformation.Instance.CurAmmo;
        Debug.Log("cur_Bullet : " + cur_Bullet);

        curBullet.text = cur_Bullet.ToString() + " /  30"; 
                         //string.Format("{0} / {1}",cur_Bullet, magCapacity);
        //remainBullet.text = GameInformation.Instance.RemainAmmo.ToString();
    }

    private void HandleHP() // HP바 
    {
        curHP = GameInformation.Instance.HP;
        //hpbar.value = curHP / maxHP;
        hpbar.value = Mathf.Lerp(hpbar.value, curHP / maxHP, Time.deltaTime);
    }

    public void Show_CrossHair()
    {
        if(Input.GetButton("Fire2"))
        //if(Input.GetMouseButtonDown(1))
            crossHair.SetActive(true);
        else
            crossHair.SetActive(false);
    }
}
 