using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Bomb;

    public Vector3[] pos;

    int maxPos = 15;
    int ran;
    void Awake()
    {
    }

    void Start()
    {
        for(int i = 0; i < 1; i++)
        {
            Instantiate(Bomb, pos[i], Quaternion.identity);
        }
    }

    void Update()
    {

    }

    void Create()
    {
        Instantiate(Bomb, pos[ran], Quaternion.identity);
    }
}
