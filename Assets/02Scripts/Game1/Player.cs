using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriter.flipX = true;
            this.transform.Translate(-0.5f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriter.flipX = false;
            this.transform.Translate(0.5f, 0.0f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.5f, 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, -0.5f, 0.0f);
        }
    }
}
