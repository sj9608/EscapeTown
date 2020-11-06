using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBase<GameManager>
{
    GameInformation GI;
    SceneController SCI;
    ChatManager ChatManager;
    // 게임 save load용 data class
    GameData gameData;
    public SoundController Sc;

    // 적 수를 딕셔너리에 넣어 좀비 수로 골포인트 판별
    GameObject enemies;
    public Dictionary<string, Zombie> enemiesDic;

    // ChatObject 자식 오브젝트의(ObjectData 컴포넌트) 수로 골포인트 판별
    GameObject chatObject;
    public int numOfChatObject;

    // 게임오버 판단
    public bool isGameOver;
    // 포션 사용 중
    public bool isUsePotion;
    // 로딩 중
    public bool isLoading;
    // 팝업 띄우는 중
    public bool isPopupOn;
    // 설정 띄우는 중
    public bool isOptionOn;
    // 대화 중
    public bool isInteractioning;

    // 무기 데미지 나중 무기클래스에서 얻어옴
    int weaponDamage;

    // 퀵슬롯에서 2번(탄창 누를 시 충전 될 총알 수
    int addAmmo = 60;
    public PlayerAttack playerAttack;
    public CharacterLocomotion characterLocomotion;
    // 씬 별 낮 밤 체크 bool 배열
    // 0번 / 1번은 true 고정 ManagerScene / MainScene
    // 마지막 번호는 EndingScene
    bool[] isDays = {true, true, true, false, true, false, true, false, true, false, false, true, false, true, false, true};
    //                0     1     2       3     4     5     6     7       8     9     10    11    12      13    14     15
    //               MS    MAIN  ROOM   S02   S03   S031   S04   S041   S05   S051   S06    S07   S071   S08    S81   GES 
    // 씬로딩에만 쓸 임시 씬번호
    private int tempCurrentSceneNum;

    // 낮 밤 스테이지 따른 배경음악
    public AudioClip audioDay;
    public AudioClip audioNight;
    public AudioSource audioSource;
    float audioTime = 0f;


    // 게임 오버시 UI호출 Action
    public event UnityAction GameOverAction;
    public event UnityAction GetMagazineAction;
    public UnityAction<bool> IsAimAction;
    public event UnityAction<bool> UIPauseAction;
    public event UnityAction<bool> UIOptionToggleAction;
    public void InitScene()
    {
        // 현재 페이지에서만 쓸 인자용 씬번호
        // 저장위치에 따라 쓸모 유무가 생김
        // 저장위치는 스테이지 클리어 직후 / 로딩 중 / 새 스테이지 씬 로딩 후
        tempCurrentSceneNum = SCI.CurSceneNum;
        Debug.Log("tempCurrentSceneNum : " + tempCurrentSceneNum);
        playerAttack = FindObjectOfType<PlayerAttack>();
        characterLocomotion = FindObjectOfType<CharacterLocomotion>();
        if (characterLocomotion != null)
        {
            characterLocomotion.ChangePose(isDays[tempCurrentSceneNum]);
        }
        // 인스펙터에서 Enemies에 아무것도 넣지 않으면
        // 해당 스테이지는 낮 Scene
        enemies = GameObject.Find("Enemies");
        enemiesDic = new Dictionary<string, Zombie>();
        if (enemies != null)
        {
            enemiesDic = enemies.GetComponentsInChildren<Zombie>().ToDictionary(key => key.name);
            Debug.Log("enemiesDic.Count : " + enemiesDic.Count);
        }

        // ChatObject의 자식 수 세기
        chatObject = GameObject.Find("ChatObject");
        if(chatObject != null) numOfChatObject = chatObject.transform.childCount;

        if (tempCurrentSceneNum == isDays.Length -1)
        {
            SCI.EndingScene();
        }

        // 오디오 전환
        if(tempCurrentSceneNum > 1)
        {
            if(isDays[tempCurrentSceneNum] == true) { audioSource.clip = audioDay; }
            else{ audioSource.clip = audioNight; }
            audioSource.Play();
            Cursor.visible = false;
        }
        
    }

    private void Awake()
    {
        GI = GameInformation.Instance;
        SCI = SceneController.Instance;
        ChatManager = ChatManager.Instance;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }
    void Start()
    {
        isInteractioning = false;
        isGameOver = false;
        isUsePotion = false;
        isLoading = false;
        isPopupOn = false;
        InitScene();
    }
    void Update()
    {
        if (isGameOver || isLoading)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Popup();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsePotion();
        }



        // 테스틀용 코드 정식 버전에서 삭제
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetPotion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetMagazine();
        }
    }
    public void ZombieDead(string zName)
    {
        enemiesDic.Remove(zName);
    }

    public void PlayerDead()
    {
        Debug.Log("플레이어가 죽음을 게임매니저가 인식");
        GameOver();
    }

    public void GetItem(Collider getItem)
    {
        if (getItem != null)
        {
            switch (getItem.name)
            {
                case "Potion":
                    GetPotion();
                    break;
                case "Magazine":
                    GetMagazine();
                    break;
            }
            Debug.Log(getItem.name + "을 습득했다.");
            getItem.gameObject.SetActive(false);
        }
    }
    public void GetPotion()
    {
        GI.UpdatePotion(1);
    }
    public void UsePotion()
    {
        // 포션 사용 메서드
        // QuickSlot 에서 포션 개수가 남아 있는지 확인
        if(isUsePotion != true && GI.NumOfPotion > 0)
        {
            GI.UpdatePotion(-1);
            GI.UpdateHp(30);
        }
    }
    public void GetMagazine()
    {
        GI.RemainAmmo += addAmmo;
        GetMagazineAction();
    }
    
    // Main Scene 버튼 새로하기 이어하기
    // 새로하기
    public void NewGame()
    {
        // gameData가 null 새로 생성 해서 새 게임
        // nextScene에 (메인 씬 번호 넘김)
        gameData = null;
        GI.GameInformationInit(gameData);
        SaveGameDataToJson();
        SCI.NextSecne(tempCurrentSceneNum);
    }
    // 이어하기
    public void LoadGame()
    {
        // gameData가 loadData
        // nextScene에 (메인 씬 번호 넘김)
        gameData = LoadGameDataToJson();
        GI.GameInformationInit(gameData);
        SCI.NextSecne(tempCurrentSceneNum);
    }
    // Main Scene 버튼 새로하기 이어하기 끝 

    // 게임 오버 팝업과 연결
    public void GameOver()
    {
        Debug.Log("게임 오버");
        // 게임 오버 bool
        // 각 씬 입력값 막기
        isGameOver = true;
        
        if (GameOverAction != null)
        {
            GameOverAction();
        }
        
    }
    
    public void GameRetry()
    {
        isGameOver = false;
        LoadGame();
    }
    
    public void GoMain()
    {
        // 메인 씬 불러오기
        isGameOver = false;
        SCI.CurSceneNum = 1;
        SCI.NextSecne(tempCurrentSceneNum);
    }
    public void StageClear()
    {
        if (enemiesDic == null || enemiesDic.Count == 0)
        {
            if(numOfChatObject == 0)
            {
               Debug.Log("스테이지 클리어");
                SCI.CurSceneNum++;
                SaveGameDataToJson();
                SCI.NextSecne(tempCurrentSceneNum); 
            }
        }
        else 
        {
            Debug.Log("클리어 조건을 만족하지 못하였습니다.");
        }
    }

    [ContextMenu("To Json Data")]
    public void SaveGameDataToJson()
    {
        // 저장 데이터 생성
        gameData = new GameData(SceneController.Instance.CurSceneNum, GI.HP, GI.RemainAmmo, GI.CurAmmo, GI.NumOfPotion, ChatManager.chatArray, ChatManager.chatNumber);
        string jsonData = JsonUtility.ToJson(gameData, true);
        string path = Path.Combine(Application.dataPath, "SaveData.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public GameData LoadGameDataToJson()
    {
        string path = Path.Combine(Application.dataPath, "SaveData.json");
        string jsonData = File.ReadAllText(path);
        gameData = JsonUtility.FromJson<GameData>(jsonData);
        return gameData;
    }
    public void Popup(){
        if (isOptionOn && isPopupOn)
        {
            // 옵션 창 끄기
            UIOptionToggleAction(isOptionOn);
            isOptionOn = !isOptionOn;
        }
        else
        {
            isPopupOn = !isPopupOn;
        }
        
        if (isPopupOn)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        else
        {
            // 마우스 커서를 화면 중앙에 고정
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("커서 비활성화");
        }
        UIPauseAction(isPopupOn);
        Set_Pause();
    }
    private void Set_Pause(){
        Time.timeScale = isPopupOn ? 0f : 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // fixedDeltaTime = 물리적인 효과, FixedUpdat 가 실행되는  초당 간격
    }
}

 