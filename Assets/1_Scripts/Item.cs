using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // 포션 : HP + 30
    // 탄창 : 총알 + 20
    // Use : 아이템 사용 메서드
    // Get : 아이템 획득 
    public float recoveryPoint;
    public GameObject potion;
    public GameObject magazine;

    public void Use(GameObject target)
    {
        // 플레이어.아이템 사용 -> 매니저 아이템 개수 확인, 사용(플레이어 HP+)
        // 아이템을 사용하는 메서드
        if(target.gameObject == potion.gameObject)
        {
            // 포션 사용
            
        }
        else if(target.gameObject == magazine.gameObject)
        {
            // 탄창 사용
        }
    }
}
