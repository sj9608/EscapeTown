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

    public Text remainBullet;            // 총 탄 수
    public Text curBullet;               // 잔탄 수
    

    public ChatController chatController;      // 대화 텍스트
    [SerializeField] private Slider hpbar; // HP바

    private void Start() 
    {
        // numOfPotion 값 불러오기
        int potion = GameManager.Instance.UsePotion();
        // numOfBullet 값 불러오기

        // curHP 값 불러오기
        hpbar.value = GameManager.Instance.playerHealth.HP;
        
        // 총 탄 수 / 잔 탄 수 불러오기
        curBullet.text = GameManager.Instance.playerAttack.gun.magAmmo.ToString(); // 잔탄 수;
        remainBullet.text = GameManager.Instance.playerAttack.gun.ammoRemain.ToString(); // 총탄 수;
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
    }


    public void Show_Bullet_Count()  // 장전 후 사용하고 남은 총알 갯수
    {
        // curBullet : 현재 총알수, remain_Bullet : 최대 총알 수(30발)
        int cur_Bullet = GameManager.Instance.playerAttack.gun.magAmmo;
        curBullet.text = cur_Bullet.ToString(); 
        int remainBullet = GameManager.Instance.playerAttack.gun.ammoRemain;


    }

    private void HandleHP() // HP바 
    {
        curHP = GameManager.Instance.playerHealth.HP;
        //hpbar.value = curHP / maxHP;
        hpbar.value = Mathf.Lerp(hpbar.value, curHP / maxHP, Time.deltaTime);
    }
}
