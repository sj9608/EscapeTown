using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// Stage_06 맵에 가둬둔 좀비를 풀어놓는 함정 트리거 스크립트
// 1. 플레이어와 바닥에 심어둔 트리거가 부딪히는 것으로 발동
// 2. 트리거 발동 시 벽이 제거 됨
public class Triger : MonoBehaviour
{
    public GameObject wall;
    Vector3 target = new Vector3(0, -10, 0);
    private Collider trap;
    
    void Start()
    {
        trap = GetComponent<Collider>();
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {   
            Debug.Log("Working");
            wall.transform.position += target;
        }
        yield return null;
    }
}
