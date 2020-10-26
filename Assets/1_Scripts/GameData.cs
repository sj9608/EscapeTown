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

    public GameData(int saveSceneName, float saveHP, int saveRemainAmmo, int saveCurAmmo, int saveNumOfPotion, int saveNumOfMagazine, int[] saveChatArray, int saveChatNumber)
    {
        this.saveSceneName = saveSceneName;
        this.saveHP = saveHP;
        this.saveRemainAmmo = saveRemainAmmo;
        this.saveCurAmmo = saveCurAmmo;
        this.saveNumOfPotion = saveNumOfPotion;
        this.saveNumOfMagazine = saveNumOfMagazine;
        this.saveChatArray = saveChatArray;
        this.saveChatNumber = saveChatNumber;
    }
}
