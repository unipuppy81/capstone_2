using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    private static GameManager3 _instance; // 다른 코드에서 불러오기 쉽도록 만든 
    public static GameManager3 instance    // 싱글톤 패턴
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
    DatabaseManager databaseManager;

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

    public float nowscore; // nowscore 끝나고 뜨는 현재 점수
    public float score1;  // score1 == 실시간으로 올라가는 점수
    public string bestscore; // 최고 점수


    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private TextMeshProUGUI nowScoreTxt;
    [SerializeField]
    private TextMeshProUGUI score1Text;
    [SerializeField]
    private TextMeshProUGUI bestScoreText;


    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject PausePanel;
    public GameObject GameoverPanel;

    public GameObject Bgm;
    public GameObject Overbgm;

    public bool stopTrigger = true;

    public bool isStart;

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
        checkBestScore();
        score1 = 0;
        
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();

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

    private void Update()
    {
        if (isStart)
        {
            score1Text.text = score1.ToString();
            nowScoreTxt.text = score1.ToString();


            databaseManager.score3_1 = score1;
            databaseManager.score3_2 = float.Parse(databaseManager.tmp3);
            bestscore = databaseManager.score3_2.ToString();
        }
        bestScoreText.text = databaseManager.tmp3.ToString();
    }

    void checkBestScore()
    {
        UnityEngine.Debug.Log("chekBestScore");
        databaseManager.OnClickSaveButton3();
    }

    public void Score() // 1점 획득
    {
        score1++;
        scoreTxt.text = ""+ score1;
    }

    public void Score2() // 2점 획득
    {
        score1 += 2;
        scoreTxt.text = "" + score1;
    }

    public void Score3() // 3점 획득
    {
        score1 += 3;
        scoreTxt.text = "" + score1;
    }

    public void GameStart()
    {
        Debug.Log("시작?");
        Time.timeScale = 1.0f;
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
        Debug.Log("게임 시작");
    }

    public void GameOver()
    {
        databaseManager.readScore("Game3");
        stopTrigger = false;

        StopCoroutine(e1);
        StopCoroutine(e2);
        StopCoroutine(e3);
        StopCoroutine(c1);
        StopCoroutine(c2);

        /*
        if (score >= PlayerPrefs.GetInt("BestScore", 0))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

        nowScoreTxt.text = "" + score;


        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();
                */
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
        if(score1 >= 10)
        {
            Instantiate(enemy2, pos, Quaternion.identity);
        }
    }

    private void CreateEnemy3()
    {
        a = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(a, 1.1f, 0));
        pos.z = 0.0f;
        if (score1 >= 50)
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
        if(score1 >= 15)
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
