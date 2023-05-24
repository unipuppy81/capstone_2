using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Spawn : MonoBehaviour
{
    MoveG1 g1;

    public GameObject Bomb;

    public Vector3[] pos;

    public float Rtimer;
    public int bombCount;
    int maxPos = 112;
    int ran;

    private void Awake()
    {
        bombCount = 10;
        Rtimer = 4.0f;
        g1 = GameObject.Find("Player").GetComponent<MoveG1>();

        // 폭탄 터지기까지 3초 + 애니메이션 1초
    }
    void Start()
    {
        InvokeRepeating("Create", 2, Rtimer);
        InvokeRepeating("SetUp", 2, 1.0f);
    }

    private void Update()
    {
        SetBombCount();
    }

    void SetBombCount()
    {
        if (g1.gameTime >= 0 && g1.gameTime < 20.0f)
        {
            bombCount = 10;
        }
        else if(g1.gameTime >= 20.0f && g1.gameTime < 40.0f)
        {
            bombCount = 13;
        }
        else if(g1.gameTime >= 40.0f)
        {
            bombCount = 16;
        }
    }
    void SetUp()
    {
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
