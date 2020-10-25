using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    int saveSceneName;
    float saveHP;
    int saveRemainAmmo;
    int saveCurAmmo;
    int saveNumOfPotion;
    int saveNumOfMagazine;

    int[] saveChatArray;
    int saveChatNumber;

    public GameData()
    {
        saveSceneName = GameInformation.Instance.CurSceneNum;
        saveHP = GameInformation.Instance.HP;
        saveRemainAmmo = GameInformation.Instance.RemainAmmo;
        saveCurAmmo = GameInformation.Instance.CurAmmo;
        saveNumOfPotion = GameInformation.Instance.NumOfPotion;

        
        saveChatArray = ChatManager.Instance.chatArray;
        saveChatNumber = ChatManager.Instance.chatNumber;
    }
}
