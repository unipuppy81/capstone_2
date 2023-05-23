using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject g1;

    public GameObject Bomb;

    public Vector3[] pos;

    public float Rtimer;
    public int bombCount = 15;
    int maxPos = 112;
    int ran;

    private void Awake()
    {
        Rtimer = 4.0f;
        // 폭탄 터지기까지 3초 + 애니메이션 1초
    }
    void Start()
    {
        InvokeRepeating("Create", 2, Rtimer);
    }

    void Update()
    {
        //Rtimer = 3.0f + 1.0f;
    }

    void Create()
    {
        for(int i = 0; i < bombCount; i++) { 

            ran = Random.Range(0, maxPos);
            Vector2 setPos = pos[ran];
            g1.GetComponent<MoveG1>().position = setPos;
            g1.GetComponent<MoveG1>().SetBomb();

        }
    }
}
