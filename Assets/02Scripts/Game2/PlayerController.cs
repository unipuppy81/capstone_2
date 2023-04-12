using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isJump = false;
    bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;
    public GameObject Upbtn;
    public GameObject Downbtn;

    Vector2 startPosition; //캐릭터 시작위치
    Animator animator; //애니메이션 변수
    ButtonManager btnMangaer;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(GameManager.instance.isPlay)
        {
            animator.SetBool("RUN", true);
        }
        else
        {
            animator.SetBool("RUN", false);
        }
        UpBtn();
        DownBtn();
    }

    public void DownBtn()
    {
        if(isJump && Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = startPosition;
        }
    }
    public void UpBtn()
    {

        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.isPlay)
        {
            isJump = true;
            animator.SetBool("JUMP", true);
        }
        else if (transform.position.y <= startPosition.y)
        {
            isJump = false;
            isTop = false;
            transform.position = startPosition;
            animator.SetBool("JUMP", false);
        }
        if (isJump)
        {
            if (transform.position.y <= jumpHeight - 0.1f && !isTop)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, jumpHeight), jumpSpeed * Time.deltaTime);
            }
            else
            {
                isTop = true;
            }
            if (transform.position.y > startPosition.y && isTop)
            {
                transform.position = Vector2.MoveTowards(transform.position, startPosition, jumpSpeed * Time.deltaTime);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ob"))
        {
            GameManager.instance.GameOver();
        }
    }
}
