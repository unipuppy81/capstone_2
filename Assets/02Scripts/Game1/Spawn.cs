using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject g1;

    public GameObject Bomb;

    public Vector3[] pos;
    int maxPos = 127;
    int ran;

    //¹öÀü
    void Start()
    {
        InvokeRepeating("Create", 2, 3.0f);
       
    }

    void Update()
    {
        
    }

    void Create()
    {
        for(int i = 0; i < 10; i++) { 

            ran = Random.Range(0, maxPos);
            Vector2 setPos = pos[ran];
            g1.GetComponent<MoveG1>().position = setPos;
            g1.GetComponent<MoveG1>().SetBomb();

        }
    }
}
