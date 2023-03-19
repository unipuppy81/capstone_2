using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private Animator animator;

    private SpriteRenderer renderer;

    private float speed = 3;

    private float horizontal;

    public bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (GameManager3.instance.stopTrigger)
        {
            animator.SetTrigger("start");
            PlayerMove();
        }

        if (!GameManager3.instance.stopTrigger)
        {
            animator.SetTrigger("dead");
        }

        ScreenChk();
    }

    private void PlayerMove()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (horizontal < 0)
        {
            renderer.flipX = true;
        }
        else
        {
            renderer.flipX = false;
        }

        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);
    }

    private void ScreenChk()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.x < 0.05f) worldpos.x = 0.05f;
        if (worldpos.x > 0.95f) worldpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }
}
