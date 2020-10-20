using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get{ return instance; }
    }

    public string sceneName;
    
    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}

