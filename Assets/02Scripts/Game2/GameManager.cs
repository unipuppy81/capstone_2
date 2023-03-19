using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ButtonManager;
using UnityEngine.UI;
using TMPro;

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


    public int runScore1 = 0;
    public int runScore2 = 0;

    public TextMeshProUGUI runscore1Text;
    public TextMeshProUGUI runscore2Text;
    public TextMeshProUGUI RunBestScoreText;

    private void Start()
    {
        RunBestScoreText.text = PlayerPrefs.GetInt("RunBestScore",0).ToString();
        GameoverPanel.SetActive(false);
    }

    IEnumerator AddScore()
    {
        while(isPlay)
        {
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
        playbtn.SetActive(false);
        explainPanel.SetActive(false);
        GameoverPanel.SetActive(false );
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
        //playbtn.SetActive(true);
        //explainPanel.SetActive(true);
        isPlay = false;
        onPlay.Invoke(isPlay);
        StopCoroutine(AddScore());
        //�ְ�����
        if (PlayerPrefs.GetInt("RunBestScore", 0) < runScore1)
        {
            PlayerPrefs.SetInt("RunBestScore", runScore1);
            RunBestScoreText.text = runScore1.ToString();
        }
    }
}
