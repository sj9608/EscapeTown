using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Option panel UI 관리
// 옵션 창 On / Off
// BGM / GameSound 볼륨 조절
// 뒤로가기 버튼
// GameManager에 만들어 둔 UnityAction에 메서드 등록
public class UIOption : MonoBehaviour
{
    GameManager GMI;
    public GameObject option;
    public AudioSource bgmsource;
    public AudioSource gameSound_Source;
    public Button btn_Back;
    UIPause uipause;
    public bool isShow = false;

    private void Awake() {
        GMI = GameManager.Instance;
    }
    // UI Action에 등록 / 해제
    private void OnEnable() {
        GMI.UIOptionToggleAction += ShowOptionUI;
    }
    private void OnDisable() {
        GMI.UIOptionToggleAction -= ShowOptionUI;
    }
    void Start()
    {
        uipause =  GetComponent<UIPause>();
    }
    void Update()
    {
        // Popup_Option();
    }

    // public void Popup_Option()
    // {
    //     if (GameManager.Instance.isGameOver || GameManager.Instance.isLoading)
    //     {
    //         return;
    //     }
    //     if (Input.GetKeyDown(KeyCode.Escape) && uipause.ispopup == true)
    //     {
    //         option.SetActive(false);
    //         GameManager.Instance.isPopupOn = true;
    //         uipause.menuSet.SetActive(true);
    //         isShow = false;
    //     }
    // }
    // 옵션 창 On/Off
    public void ShowOptionUI(bool isOption){
        option.SetActive(!isOption);
    }
    // BGM 볼륨 조절
    public void SetBGMVolume(float volume)
    {
        bgmsource.volume = volume;
    }
    // GameSound 볼륨 조절
    public void SetGameVolume(float volume)
    {
        gameSound_Source.volume = volume;
    }
    // 뒤로가기 버튼
    // 일시정지 > 옵션 에서 뒤로가기 눌렀을 때
    // 뒤로가기와 ESC는 동일 기능
    public void BTN_Back()
    {
        // option.SetActive(false);
        // Debug.Log("working back button!");
        // GameManager.Instance.isPopupOn = true;
        // uipause.menuSet.SetActive(true);
        // Debug.Log("working pannel");
        // isShow = false;
        GMI.Popup();
    }
}
