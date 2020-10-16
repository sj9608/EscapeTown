using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
        private set{}
    }

    public string sceneName;
    
    public void Awake()
    {
        instance = this;
    }
}
