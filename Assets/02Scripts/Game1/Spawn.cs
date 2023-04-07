using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Bomb;

    public Vector3[] pos;
    int maxPos = 63;
    int ran;

    //¹öÀü
    void Start()
    {
        InvokeRepeating("Create", 2, 10f);
    }

    void Update()
    {
        
    }

    void Create()
    {
        for(int i = 0; i < 10; i++) { 
            ran = Random.Range(0, maxPos);
            Instantiate(Bomb, pos[ran], Quaternion.identity);
        }
    }
}
