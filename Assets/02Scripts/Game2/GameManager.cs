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

    public float gameSpeed = 1;
    public bool isPlay = false;
    public GameObject playbtn;
    public GameObject mainbtn;
    public GameObject explainPanel;
    public GameObject GameoverPanel;
    public GameObject PausePanel;
    public GameObject Bgm;
    public GameObject Overbgm;


    public int runScore1 = 0;
    public int runScore2 = 0;

    public TextMeshProUGUI runscore1Text;
    public TextMeshProUGUI runscore2Text;
    public TextMeshProUGUI RunBestScoreText;

    public int curStage;
    public int[] stageScore;
    public Stage[] stages;
    private void Start()
    {
        Bgm.SetActive(false);
        Overbgm.SetActive(false);
        RunBestScoreText.text = PlayerPrefs.GetInt("RunBestScore",0).ToString();
        GameoverPanel.SetActive(false);
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
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
            
            runScore1++;
            runScore2++;
            runscore1Text.text = runScore1.ToString();
            runscore2Text.text = runScore2.ToString();
            gameSpeed = gameSpeed + 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Playbtn()
    {
        //AudioManager.instance.PlayBgm(true);
        playbtn.SetActive(false);
        explainPanel.SetActive(false);
        GameoverPanel.SetActive(false );
        Bgm.SetActive(true);
        curStage = 0;
        isPlay = true;
        onPlay.Invoke(isPlay);
        gameSpeed = 1;
        runScore1 = 0;
        runScore2 = 0;
        runscore1Text.text = runScore1.ToString();
        runscore2Text.text = runScore2.ToString();
        StartCoroutine(AddScore());
    }

    public void GameOver()
    {
        GameoverPanel.SetActive(true);
        Bgm.SetActive(false);
        Overbgm.SetActive(true);
        isPlay = false;
        onPlay.Invoke(isPlay);
        //AudioManager.instance.PlayBgm(false);
        //AudioManager.instance.PlaySfx(AudioManager.Sfx.GameOver);
        StopCoroutine(AddScore());
        StopCoroutine(GameoverRoutine());
        //최고점수
        if (PlayerPrefs.GetInt("RunBestScore", 0) < runScore1)
        {
            PlayerPrefs.SetInt("RunBestScore", runScore1);
            RunBestScoreText.text = runScore1.ToString();
        }
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
