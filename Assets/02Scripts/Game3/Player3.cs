using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Player3 : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;

    private Animator animator;

    private SpriteRenderer playerRenderer;

    private float speed = 3;

    private float horizontal;
    private float Rotation;

    public bool isDie = false;

    public Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (GameManager3.instance.stopTrigger)
        {
            animator.SetTrigger("start");
            //PlayerMoveKey();
            PlayerMoveMouse();
        }

        if (!GameManager3.instance.stopTrigger)
        {
            animator.SetTrigger("dead");
        }

        ScreenChk();
    }


    private void PlayerMoveKey()
    {
        animator.SetFloat("speed", Mathf.Abs(horizontal));

        if (horizontal < 0)
        {
            playerRenderer.flipX = true;
        }
        else
        {
            playerRenderer.flipX = false;
        }

        playerRigidbody.velocity = new Vector2(horizontal * speed, playerRigidbody.velocity.y);
    }

    private void PlayerMoveMouse()
    {
        // animator.SetBool("Rotation", TouchMove.instance.isClick1);

        if (TouchMove.instance.isClick1 == true)
        {
            Rotation = -1;
            playerRenderer.flipX = true;
            //playerRigidbody.AddForce(new Vector2(-speed, 0), ForceMode2D.Force);
            playerRigidbody.velocity = new Vector2(-1.2f * speed, playerRigidbody.velocity.y);
        }
        else if (TouchMove.instance.isClick2 == true)
        {
            Rotation = 1;
            playerRenderer.flipX = false;
            //playerRigidbody.AddForce(new Vector2(speed, 0), ForceMode2D.Force);
            playerRigidbody.velocity = new Vector2(1.2f * speed, playerRigidbody.velocity.y);
        }
        animator.SetFloat("speed", Mathf.Abs(Rotation));
    }

    private void ScreenChk()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.x < 0.05f) worldpos.x = 0.05f;
        if (worldpos.x > 0.95f) worldpos.x = 0.95f;
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }
}
