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
    DatabaseManager databaseManagerG1;

    public GameObject Joy;

    //public GameObject MainBtn;
    //public GameObject StartBtn;
    public GameObject PauseBtn;
    public GameObject ResumeBtn;

    public GameObject GameOverPanel;
    public GameObject PausePanel;
    public GameObject ExplainPanel;

    public GameObject BGM;
    public GameObject BGM_die;

    public TMP_Text bestScoreText;
    public TMP_Text nowScoreText;

    public string bestScore;
    public string nowScore;

    public bool isStart;

    public ParticleSystem starParticle;
    public bool bestEffect;

    private void Awake()
    {
        moveG1 = GameObject.Find("Player").GetComponent<MoveG1>();
        scoreG1 = GameObject.Find("Score").GetComponent<ScoreG1>();
        databaseManagerG1 = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
    }
    private void Start()
    {
        BGM_die.SetActive(false);
        isStart = false;
        bestEffect = false;
    }

    private void Update()
    {
        isStarting();
        isDie();
    }

    public void SetJoy()
    {
        Joy.SetActive(true);
    }
    public void isDie()
    {
        if(moveG1.isDead)
        {
            ///databaseManagerG1.OnClickSaveButton1();
            //nowScore = databaseManagerG1.score1_1.ToString();
            // bestScore = databaseManagerG1.score.ToString();
            BGM.SetActive(false);
            BGM_die.SetActive(true);
            GameOverPanel.SetActive(true);
            Joy.SetActive(false);
            bestScoreEffect();

            isStart = false;
        }
    }

    void bestScoreEffect()  // 최고기록 시 이펙트
    {
        if (scoreG1.score1 < databaseManagerG1.score1_2 && bestEffect == false)
        {
            AudioManager.soundPlay2();
            ParticleSystem particleSystem = Instantiate(starParticle);
            bestEffect = true;
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
