using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{
    // 게임 플레이 화면 UI
    // 퀵슬롯, HP바, 퀘스트 목록, 시스템 로그, 총알 수, 대화 텍스트

    GameInformation GI;

    // 퀘스트 목록 관련 변수
    
    // 시스템 로그 관련 변수

    //public Text remainBullet;            // 총 탄 수
    public Text curBullet;               // 잔탄 수

    public GameObject crossHair;

    public Image IMG_potion;
    public Image IMG_magazine;
    public Text numOfPotion_text;
    public Text numOfMagazine_text;

    //public ChatController chatController;      // 대화 텍스트
    public delegate void battle_Event();

    [SerializeField] private Slider hpbar; // HP바

    private void Awake()
    {
        GI = GameInformation.Instance;
    }
    private void OnEnable()
    {
        GI.UpdateCurAmmoAction += Show_Bullet_Count;
        GI.UpdateRemainAmmoAction += GetMagazine;
        GameManager.Instance.GetMagazineAction += GetMagazine;
        GI.UpdateGetPotionAction += GetPotion;
        GI.UpdateUsePotionAction += UsePotion;
        // GI.UpdateHpAction += HandleHP;       // UnityAction에 Lerp적용 되야함
        GameManager.Instance.IsAimAction += Show_CrossHair;
    }
    private void OnDisable()
    {
        GI.UpdateCurAmmoAction -= Show_Bullet_Count;
        GI.UpdateRemainAmmoAction -= GetMagazine;
        GameManager.Instance.GetMagazineAction -= GetMagazine;
        GI.UpdateGetPotionAction -= GetPotion;
        GI.UpdateUsePotionAction -= UsePotion;
        // GI.UpdateHpAction -= HandleHP;       // UnityAction에 Lerp적용 되야함
        GameManager.Instance.IsAimAction -= Show_CrossHair;
    }
    private void Start() 
    {
        // numOfPotion 값 불러오기
        
        // numOfBullet 값 불러오기

        // curHP 값 불러오기
        hpbar.value = GI.HP;
        
        // 총 탄 수 / 잔 탄 수 불러오기
        curBullet.text = GI.CurAmmo.ToString(); // 잔탄 수;
        //remainBullet.text = GameInformation.Instance.RemainAmmo.ToString(); // 한 탄창에 남은 총알 수
        //show_CrossHair = crossHair.GetComponent<GameObject>();
        
        numOfPotion_text.text = GI.NumOfPotion.ToString();
        numOfMagazine_text.text = GI.RemainAmmo.ToString();

        crossHair.SetActive(false);                        
    }

    private void Update() 
    {
        // numOfPotion 값 불러오기
        // 

        // numOfBullet 값 불러오기
        // UI 반영

        // curHP 값 불러오기
        HandleHP(); // GameInformation의 UnityAction 델리게이트로 이동  // UnityAction에 Lerp적용시키기

        // 총 탄 수 / 잔 탄 수 불러오기
        // Show_Bullet_Count(); // GameInformation의 UnityAction 델리게이트로 이동
        // Show_CrossHair(); // GameManager의 UnityAction 델리게이트로 이동
    }


    public void Show_Bullet_Count()  // 장전 후 사용하고 남은 총알 갯수
    {
        // curBullet : 현재 총알수, magCapacity : 최대 총알 수(30발)

        curBullet.text = GI.CurAmmo.ToString() + " /  30"; 
                         //string.Format("{0} / {1}",cur_Bullet, magCapacity);
        //remainBullet.text = GameInformation.Instance.RemainAmmo.ToString();
    }

    private void HandleHP() // HP바 
    {
        hpbar.value = Mathf.Lerp(hpbar.value, GI.HP / GI.MAX_HP, Time.deltaTime);
    }

    public void Show_CrossHair(bool fire2)
    {
        crossHair.SetActive(fire2);
    }
    public void UsePotion()
    {
        numOfPotion_text.text = GI.NumOfPotion.ToString();
        Potion_CoolTime();
    }
    public void GetPotion()
    {
        numOfPotion_text.text = GI.NumOfPotion.ToString();
    }
    void GetMagazine()
    {
        numOfMagazine_text.text = GI.RemainAmmo.ToString();
    }

    public void Potion_CoolTime() // 아이템 사용시 쿨타임 표시 UI(쿨 타임 동안 퀵슬롯 이미지 로드)
    {
        GameManager.Instance.isUsePotion = true;
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
            GameManager.Instance.isUsePotion = false;
        }
    }
}
 