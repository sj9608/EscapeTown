using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] Player player;
    GameObject enemies;
    Dictionary<string, Enemy> enemiesDic;

    // 게임오버 판단
    public bool IsGameOver;
    // 무기 데미지 나중 무기클래스에서 얻어옴
    int weaponDamage;
    // Start is called before the first frame update
    void Start()
    {
        // 인스펙터에서 Enemies에 아무것도 넣지 않으면
        // 해당 스테이지는 
        enemies = GameObject.Find("Enemies");
        if (enemies != null)
        {
            enemiesDic = enemies.GetComponentsInChildren<Enemy>().ToDictionary(key => key.name);
        }
        IsGameOver = false;
    }
    public void Attack(Collider hit, float damage)
    {
        if (hit != null)
        {
            switch (hit.tag)
            {
                case "Enemy":
                    Enemy enemy = enemiesDic[hit.name];
                    enemy.HP = enemy.HP - damage;
                    if (enemy.HP <= 0)
                    {
                        enemy.Die();
                        enemiesDic.Remove(hit.name);
                    }
                    break;
                case "Player":
                    player.HP = player.HP - damage;
                    if (player.HP <= 0)
                    {
                        player.Die();
                        player.animator.SetTrigger("Die");
                        GameOver();
                    }
                    break;
            }
        }
    }
    public void GetItem(Collider getItem)
    {
        if (getItem != null)
        {
            Debug.Log(player.name + "이(가) " + getItem.name + "을 습득했다.");
            getItem.gameObject.SetActive(false);
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
