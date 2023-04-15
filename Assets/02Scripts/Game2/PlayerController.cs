using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State { idle, run, jump, hit}
    public float startJumpPower;
    public float jumpPower;
    public bool isGround;
    public bool isJump;

    Rigidbody2D rigi;
    Animator anim;
    Vector2 startPosition; //ĳ���� ������ġ

    // Start is called before the first frame update
    void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // ����
    void OnCollisionStay2D(Collision2D collision)
    {
        if (!isGround)
        {
            ChangeAnim(State.run);
            // ����
        }
        //jumpPower = 1;
        isGround = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        ChangeAnim(State.jump);
        //����
        isGround = false;
    }

    void ChangeAnim(State state)
    {
        anim.SetInteger("State", (int)state);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        rigi.simulated = false;
        // ����
        ChangeAnim(State.hit);

        if (collision.CompareTag("Ob"))
        {
            GameManager.instance.GameOver();
        }
    }

    public void UpBtn()
    {
        if (isGround)
        {
            isJump = true;
            rigi.AddForce(Vector2.up * startJumpPower, ForceMode2D.Impulse);
        }
    }

    public void DownBtn()
    {
        if (isJump)
        {
            transform.position = startPosition;
        }
    }
    

}
