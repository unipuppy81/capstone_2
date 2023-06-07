using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ButtonManager;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Stage
{
    public Sprite[] grounds;
    public GameObject[] mobs;
}

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    DatabaseManager databaseManager;

    public float gameSpeed = 1;
    public bool isPlay = false;
    public GameObject playbtn;
    public GameObject mainbtn;
    public GameObject explainPanel;
    public GameObject GameoverPanel;
    public GameObject PausePanel;
    public GameObject Bgm;
    public GameObject Overbgm;
    public GameObject WinBgm;


    public float runScore1 = 0;
    public float runScore2 = 0;

    public TextMeshProUGUI runscore1Text;
    public TextMeshProUGUI runscore2Text;
    public TextMeshProUGUI RunBestScoreText;

    public string bestscore;

    public int curStage;
    public int[] stageScore;
    public Stage[] stages;

    public ParticleSystem starParticle;
    public bool bestEffect;

    private void Start()
    {
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();

        InvokeRepeating("speedUp", 0, 0.1f);
        BestUpdate();
        Bgm.SetActive(false);
        Overbgm.SetActive(false);
        WinBgm.SetActive(false);
        GameoverPanel.SetActive(false);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
        bestEffect = false;
    }

    void speedUp()
    {
        if (isPlay) { 
        gameSpeed = gameSpeed + 0.01f;
        runScore1++;
        }
    }
    void Update()
    {
        if (isPlay)
        {
            StartCoroutine(AddScore());
            runscore1Text.text = runScore1.ToString();
            runscore2Text.text = runScore1.ToString();

            databaseManager.score2_1 = runScore1;
            databaseManager.score2_2 = float.Parse(databaseManager.tmp2);
            bestscore = databaseManager.score2_2.ToString();
        }
        RunBestScoreText.text = databaseManager.tmp2.ToString();
    }

    void BestUpdate()
    {
        UnityEngine.Debug.Log("PPP111");
        databaseManager.OnClickSaveButton2();
    }

    IEnumerator AddScore()
    {
        while(isPlay)
        {
            try {
                if (stageScore[curStage] <= runScore1)
                    curStage++;
            }
            catch { }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Playbtn()
    {
        playbtn.SetActive(false);
        explainPanel.SetActive(false);
        GameoverPanel.SetActive(false );
        Bgm.SetActive(true);
        curStage = 0;
        isPlay = true;
        onPlay.Invoke(isPlay);
        gameSpeed = 1;
    }

    void bestScoreEffect()  // 최고기록 시 이펙트
    {
        if (runScore1 > databaseManager.score2_2 && bestEffect == false)
        {
            AudioManager.soundPlay2();
            ParticleSystem particleSystem = Instantiate(starParticle);
            Destroy(particleSystem, 3.0f);
            bestEffect = true;
        }
    }

    public void GameOver()
    {
        databaseManager.readScore("Game2");
        GameoverPanel.SetActive(true);
        Bgm.SetActive(false);
        Overbgm.SetActive(true);
        isPlay = false;
        onPlay.Invoke(isPlay);
        StopCoroutine(AddScore());
        StopCoroutine(GameoverRoutine());
        bestScoreEffect();
        /*
        if(runScore1 != bestscore)
        {
            Bgm.SetActive(false);
            Overbgm.SetActive(true);
        }
        else if(runScore1 == bestscore)
        {
            Bgm.SetActive(false) ;
            WinBgm.SetActive(true) ;
        }
        */
    }

    public void Pause_btn()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator GameoverRoutine()
    {
        
        yield return new WaitForSeconds(1f);
       
    }
}
