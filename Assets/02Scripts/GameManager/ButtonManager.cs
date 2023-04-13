using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    bool isJump = false;
    bool isTop = false;
    public float jumpHeight = 0;
    public float jumpSpeed = 0;

    Vector2 startPosition; //캐릭터 시작위치
    Animator animator; //애니메이션 변수
    public GameObject PausePanel;
    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }
    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }
    public void DownBtn()
    {
        if (isJump)
        {
            transform.position = startPosition;
        }
    }
    public void UpBtn()
    {

        if (GameManager.instance.isPlay)
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
}
