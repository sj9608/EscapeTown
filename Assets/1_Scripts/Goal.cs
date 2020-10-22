using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // SceneLoader.LoadSceneHandle("Loading", 0);
            //Debug.Log("GameManager.Instance.curSceneNum : " + GameManager.Instance.curSceneNum);
            //GameManager.Instance.curSceneNum = GameManager.Instance.curSceneNum + 1;
            //Debug.Log("GameManager.Instance.curSceneNum : " + GameManager.Instance.curSceneNum);
            //if (GameManager.Instance.curSceneNum > 3)
            //{
            //    GameManager.Instance.curSceneNum = 0;
            //}
            //SceneManager.LoadScene(GameInformation.Instance.CurSceneNum);
            SceneController.Instance.NextSecne();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
