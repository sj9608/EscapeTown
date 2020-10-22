using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    GameObject enemies;
    public Dictionary<string, Zombie> enemiesDic;
    public PlayerAttack playerAttack;
    // 게임오버 판단
    public bool IsGameOver;
    // 무기 데미지 나중 무기클래스에서 얻어옴
    int weaponDamage;

    // 퀵슬롯에서 2번(탄창 누를 시 충전 될 총알 수
    int addAmmo = 60;
    public int curSceneNum = 2;
    void Start()
    {
        playerAttack = FindObjectOfType<PlayerAttack>();
        // 인스펙터에서 Enemies에 아무것도 넣지 않으면
        // 해당 스테이지는 낮 Scene
        enemies = GameObject.Find("Enemies");
        enemiesDic = new Dictionary<string, Zombie>();
        if (enemies != null)
        {
            enemiesDic = enemies.GetComponentsInChildren<Zombie>().ToDictionary(key => key.name);
        }
        Debug.Log("enemiesDic.Count : " + enemiesDic.Count);
        IsGameOver = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UsePotion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseMagazine();
        }
    }

    public void ZombieDead(string zName)
    {
        enemiesDic.Remove(zName);
    }

    public void PlayerDead()
    {
        Debug.Log("플레이어가 죽음을 게임매니저가 인식");
    }

    public void Attack(Collider hit, float damage)
    {
        if (hit != null)
        {
            switch (hit.tag)
            {
                case "Enemy":
                    Zombie enemy = enemiesDic[hit.name];
                    enemy.HP = enemy.HP - damage;
                    if (enemy.HP <= 0)
                    {
                        enemy.Die();
                        enemiesDic.Remove(hit.name);
                    }
                    break;
                case "Player":
                    //player.HP = player.HP - damage;
                    //if (player.HP <= 0)
                    //{
                    //    player.Die();
                    //    player.animator.SetTrigger("Die");
                    //    GameOver();
                    //}
                    break;
            }
        }
    }
    public void GetItem(Collider getItem)
    {
        if (getItem != null)
        {
            Debug.Log(getItem.name + "을 습득했다.");
            getItem.gameObject.SetActive(false);
        }
    }
    public void UsePotion()
    {
        // 포션 사용 메서드
        // QuickSlot 에서 포션 개수가 남아 있는지 확인
        if(QuickSlot.Instance.numOfPotion > 0)
        {
            // 남아 있으면 포션 개수 -1, HP + 30
            QuickSlot.Instance.UsePotion();
            // player.HP += 30;
        }
    }

    public void UseMagazine()
    {
        // 탄창 사용 메서드
        // QuickSlot 에서 탄창 개수가 남아 있는지 확인
        if(QuickSlot.Instance.numOfMagazine > 0)
        {
            // 남아 있으면 탄창 개수 -1
            QuickSlot.Instance.UseMagazine();
            // 총알 + 60
            // Gun 단에서 총알 충전 처리
            playerAttack.gun.AddAmmo(addAmmo);
        }
    }
    public void GameOver()
    {
        Debug.Log("게임 오버");
        SceneManager.LoadScene(0);
    }
    
    public void StageClear()
    {
        if (enemiesDic == null || enemiesDic.Count == 0)
        {
            Debug.Log("스테이지 클리어");
            SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("클리어 조건을 만족하지 못하였습니다.");
        }
    }
}

