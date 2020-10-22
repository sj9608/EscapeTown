using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    RaycastHit hit;
    float maxDistance = 15f;

    void Update()
    {
        // 이동
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z + 0.1f);
        }
        
        // f키를 눌렀을 때 주민이 레이캐스트 거리 안에 있으면 주민의 대화 내용을 출력 하고 대화정보를 비활성화 시킴 
        if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            ObjectData obj = hit.transform.GetComponent<ObjectData>();
            if(obj != null && obj.enabled == true)
            {
                StartCoroutine(ChatManager.Instance.PrintNormalChat(obj.id, obj.isNpc));
                obj.enabled = false;
            }
        }
    }
}
