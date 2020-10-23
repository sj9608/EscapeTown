using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameInformation information = GameInformation.Instance;
        
        saveSceneName = information.CurSceneNum;
        saveHP = information.HP;
        saveRemainAmmo = information.RemainAmmo;
        saveCurAmmo = information.CurAmmo;
        saveNumOfPotion = information.NumOfPotion;
        saveNumOfMagazine = information.NumOfMagazine;

        ChatManager saveChat = ChatManager.Instance;
        
        saveChatArray = saveChat.chatArray;
        saveChatNumber = saveChat.chatNumber;
    }
}
