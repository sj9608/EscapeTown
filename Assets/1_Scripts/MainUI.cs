using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BTNType
{
    Start,
    Exit
}

public class MainUI : MonoBehaviour
{
    public GameObject menuSet; // 인 게임 메뉴창
    public Text bullet_Count; // 총알 수
    public Image bullet_Counter;
    public Button btn_Continue; // 계속하기 버튼
    [SerializeField] private Slider hpbar; // HP바
    public float maxHP = 100; // 플레이어 최대 hp
    public float curHP;
    //public Human hp;
    private bool ispause = false; //menuSet 호출시 true
    int cur_Bullet; // 플레이어의 공격횟수를 받아 올 것 (현재는 임의 값 10을 준 상태)

    
    
    void Start()
    {
        hpbar.value = curHP;
    }

    public void Update()
    {
        
        HandleHP();    
         
        Show_Bullet_Count();

        popUp_Menu();

    }

    private void HandleHP() // HP바 
    {
        curHP = Player.Instance.HP;
        //hpbar.value = curHP / maxHP;
        hpbar.value = Mathf.Lerp(hpbar.value, curHP / maxHP, Time.deltaTime);
    }

    public void Show_Bullet_Count()  // 현재 총알 수 함수
    {
        
        // curBullet : 현재 총알수, max_Bullet : 최대 총알 수(10발)
        cur_Bullet = Player.Instance.gun.magAmmo;
        bullet_Count.text = cur_Bullet.ToString(); 
        
        // if(cur_Bullet < 7 && cur_Bullet >= 4)
        // {
        //     bullet_Counter.color = new Color(200,96,35,1);
        // }

        // 남은 총알 수가 0 이거나 장전키(R)을 눌렀을 때 총알 수 10발로 초기화
        // if(Input.GetKeyDown(KeyCode.R))
        // {
        //     cur_Bullet = 10;
        //     bullet_Count.text = cur_Bullet.ToString();
        // }
    }

    void popUp_Menu()  //인 게임내 팝업 메뉴창
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuSet.activeSelf)
            {
                menuSet.SetActive(false);
                Set_Outpause();
            }
            
            else
            {
                menuSet.SetActive(true);
                Set_pause();
            }
        }
    } 

    public void BTN_Continue()  // 팝업 메뉴 <계속하기>버튼
    {
        popUp_Menu();
        Set_Outpause();
    }
    public void BTN_Exit()  // 팝업 메뉴 <게임종료>버튼
    {
        Application.Quit();
        Debug.Log("게임을 종료합니다.");
    }

    public void Set_pause() // 일시정지
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격

    }
    public void Set_Outpause() // 일시정지 해제
    {   
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격
    }


}