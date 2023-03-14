using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Bomb;

    public Vector3[] pos;
    int maxPos = 15;
    int ran;


    void Start()
    {
        InvokeRepeating("Create", 2, 1f);
    }

    void Update()
    {
        
    }

    void Create()
    {
        ran = Random.Range(0, maxPos);
        Instantiate(Bomb, pos[ran], Quaternion.identity);
    }
}
