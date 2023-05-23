using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerG1 : MonoBehaviour
{
    MoveG1 moveG1;
    ScoreG1 scoreG1;

    //public GameObject MainBtn;
    //public GameObject StartBtn;
    public GameObject PauseBtn;
    public GameObject ResumeBtn;

    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject ExplainPanel;

    public bool isStart;

    private void Awake()
    {
        moveG1 = GameObject.Find("Player").GetComponent<MoveG1>();
        scoreG1 = GameObject.Find("Score").GetComponent<ScoreG1>();
    }
    private void Start()
    {
        isStart = false;
    }

    private void Update()
    {
        isStarting();
        isDie();
        if(Input.GetKeyDown(KeyCode.E)) { PlayBtnG1(); }
    }
    public void isDie()
    {
        if(moveG1.isDead)
        {
            GameOverPanel.SetActive(true);
            isStart = false;
        }

    }

    public void isStarting()
    {
        if (!isStart)
        { 
            Time.timeScale = 0.0f;
        }
        else if(isStart)
        {
            Time.timeScale = 1.0f;
        }
    }
    public void PlayBtnG1()
    {
        UnityEngine.Debug.Log("AQWE");
        ExplainPanel.SetActive(false);
        isStart = true;
    }

    public void PauseBtnG1()
    {
        PausePanel.SetActive(true);
        isStart = false;
    }

    public void ResumeBtnG1()
    {
        PausePanel.SetActive(false);
        isStart = true;
    }
    public void HomeBtnG1()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ReStartBtnG1()
    {
        scoreG1.a = 0;
        SceneManager.LoadScene("GameScene1");

    }
}
