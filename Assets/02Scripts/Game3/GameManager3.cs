using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    private static GameManager3 _instance; // ´Ù¸¥ ÄÚµå¿¡¼­ ºÒ·¯¿À±â ½±µµ·Ï ¸¸µç 
    public static GameManager3 instance    // ½Ì±ÛÅæ ÆÐÅÏ
    {
        get 
        {
            if(_instance == null) 
            {
                _instance = FindObjectOfType<GameManager3>(); 
            }
            return _instance;
        }
    }

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemy2;
    [SerializeField]
    private GameObject enemy3;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject coin2;

    public int score;
    public int nowscore;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private TextMeshProUGUI nowScoreTxt;
    [SerializeField]
    private TextMeshProUGUI bestScore;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject PausePanel;
    public GameObject GameoverPanel;

    public GameObject Bgm;
    public GameObject Overbgm;

    public bool stopTrigger = true;

    public bool isStart = false;

    private float a;
    private float b;

    IEnumerator e1;
    IEnumerator e2;
    IEnumerator e3;
    IEnumerator c1;
    IEnumerator c2;
    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        e1 = CreateEnemyRoutine();
        e2 = CreateEnemy2Routine();
        e3 = CreateEnemy3Routine();
        c1 = CreatecoinRoutine();
        c2 = Createcoin2Routine();

        GameoverPanel.SetActive(false);
        Bgm.SetActive(false);
        Overbgm.SetActive(false);

        isStart = false;
    }

    public void Score() // 1Á¡ È¹µæ
    {
        score++;
        scoreTxt.text = ""+ score;
    }

    public void Score2() // 2Á¡ È¹µæ
    {
        score += 2;
        scoreTxt.text = "" + score;
    }

    public void Score3() // 3Á¡ È¹µæ
    {
        score += 3;
        scoreTxt.text = "" + score;
    }

    public void GameStart()
    {
        stopTrigger = true;
        StartCoroutine(e1);
        StartCoroutine(e2);
        StartCoroutine(e3);
        StartCoroutine(c1);
        StartCoroutine(c2);
        panel.SetActive(false);
        GameoverPanel.SetActive(false);
        Bgm.SetActive(true);
        isStart = true;
        Debug.Log("°ÔÀÓ ½ÃÀÛ");
    }

    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(e1);
        StopCoroutine(e2);
        StopCoroutine(e3);
        StopCoroutine(c1);
        StopCoroutine(c2);

        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        nowScoreTxt.text = "" + score;

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
        GameoverPanel.SetActive(true);
        Bgm.SetActive(false);
        Overbgm.SetActive(true);
    }

    IEnumerator CreateEnemyRoutine()
    {
        while (true)
        {
            CreateEnemy();
            yield return new WaitForSeconds(0.9f);
        }
    }

    IEnumerator CreateEnemy2Routine()
    {
        while (true)
        {
            CreateEnemy2();
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator CreateEnemy3Routine()
    {
        while (true)
        {
            CreateEnemy3();
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator CreatecoinRoutine()
    {
        while (true)
        {
            CreateCoin();
            yield return new WaitForSeconds(1.7f);
        }
    }

    IEnumerator Createcoin2Routine()
    {
        while (true)
        {
            CreateCoin2();
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void CreateEnemy()
    {
        a = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(a, 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(enemy, pos, Quaternion.identity);
    }

    private void CreateEnemy2()
    {
        a = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(a, 1.1f, 0));
        pos.z = 0.0f;
        if(score >= 10)
        {
            Instantiate(enemy2, pos, Quaternion.identity);
        }
    }

    private void CreateEnemy3()
    {
        a = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(a, 1.1f, 0));
        pos.z = 0.0f;
        if (score >= 50)
        {
            Instantiate(enemy3, pos, Quaternion.identity);
        }
    }

    private void CreateCoin()
    {
        b = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(b, 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(coin, pos, Quaternion.identity);
    }

    private void CreateCoin2()
    {
        b = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(b, 1.1f, 0));
        pos.z = 0.0f;
        if(score >= 15)
        {
            Instantiate(coin2, pos, Quaternion.identity);
        }
    }
    public void Pause_btn()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
}
