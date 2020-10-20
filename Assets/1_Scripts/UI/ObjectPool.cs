using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    
    [SerializeField]
    private GameObject poolingObjectPrefab;

    Queue<Text> poolingObjectQueue = new Queue<Text>();

    private void Awake() {
        Instance = this;
        Initialize(10);
    }

    private void Initialize(int initCount)
    {
        for(int i=0; i<initCount; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewObject());
        }
    }

    private Text CreateNewObject()
    {   // content에 해당 스크립트를 할당시켜 content의 자식으로 Text가 들어오게 함
        var newObj = Instantiate(poolingObjectPrefab).GetComponent<Text>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);

        return newObj;
    }

    public static Text GetObject()
    {   
        // 오브젝트 풀이 가지고 있는 게임 오브젝트를 요청한 자에게 꺼내주는 역할
        // 모든 오브젝트를 꺼내서 빌려줘서 queue에 빌려줄 오브젝트가 없는 상태라면
        // 새로운 오브젝트를 생성해서 빌려준다
        if(Instance.poolingObjectQueue.Count > 0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            
            return obj;
        }
        else
        {
            var newobj = Instance.CreateNewObject();
            newobj.gameObject.SetActive(true);
            newobj.transform.SetParent(null);
            return newobj;
        }
    }

    public static void ReturnObject(Text obj)
    {   // 빌려준 오브젝트를 돌려받는 메서드
        // 돌려받은 오브젝트의 비활성화한 뒤 정리하는 일 처리
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(obj);
    }
}
