using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int saveSceneNum;
    public float saveHP;
    public int saveRemainAmmo;
    public int saveCurAmmo;
    public int saveNumOfPotion;
    public int[] saveChatArray;
    public int saveChatNumber;

    public GameData(int saveSceneNum, float saveHP, int saveRemainAmmo, int saveCurAmmo, int saveNumOfPotion, int[] saveChatArray, int saveChatNumber)
    {
        this.saveSceneNum = saveSceneNum;
        this.saveHP = saveHP;
        this.saveRemainAmmo = saveRemainAmmo;
        this.saveCurAmmo = saveCurAmmo;
        this.saveNumOfPotion = saveNumOfPotion;
        this.saveChatArray = saveChatArray;
        this.saveChatNumber = saveChatNumber;
    }
}
