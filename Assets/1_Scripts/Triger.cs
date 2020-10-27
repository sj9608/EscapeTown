using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
