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

    Vector2 startPosition; //ĳ���� ������ġ
    Animator animator; //�ִϸ��̼� ����
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
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ob"))
        {
            GameManager.instance.GameOver();
        }
    }
}
