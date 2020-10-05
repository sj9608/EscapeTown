using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBase<GameManager>
{
    [SerializeField] Player player;
    [SerializeField] GameObject enemies;
    Dictionary<string, Enemy> enemiesDic;

    // 무기 데미지 나중 무기클래스에서 얻어옴
    int weaponDamage;
    // Start is called before the first frame update
    void Start()
    {
        enemiesDic = enemies.GetComponentsInChildren<Enemy>().ToDictionary(key => key.name);
        //enemiesList = new List<Enemy>(enemies.GetComponentsInChildren<Enemy>());
        //foreach (KeyValuePair<string, Enemy> pair in enemiesDic)
        //{
        //    Debug.Log("pair" + pair);
        //}
    }
    public void Attack(Collider hit, int damage)
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
        if (enemiesDic.Count == 0)
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
