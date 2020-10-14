using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameover;
    public Button retry;
    public Button homebutton;


    public void Show_Pannel()
    {
        gameover.SetActive(false);

        if(GameManager.Instance.player.isDead == true)
        {
            gameover.SetActive(true);
            //Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {    
            gameover.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void Click_ReTry()
    {
        gameover.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 부르기
    }
    
    public void Click_MainButton()
    {
        SceneManager.LoadScene("Main",0);
       
        /*
        자동저장 기능 삽입 예정
        */

    }
    
}
