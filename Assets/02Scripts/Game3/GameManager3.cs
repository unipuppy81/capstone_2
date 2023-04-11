using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager3 : MonoBehaviour
{
    private static GameManager3 _instance; // �ٸ� �ڵ忡�� �ҷ����� ������ ���� 
    public static GameManager3 instance    // �̱��� ����
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
    private GameObject poop;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject coin2;

    public int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject PausePanel;

    public bool stopTrigger = true;

    private float a;
    private float b;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score() // 1�� ȹ��
    {
        score++;
        scoreTxt.text = "Score : " + score;
    }

    public void Score2() // 2�� ȹ��
    {
        score += 2;
        scoreTxt.text = "Score : " + score;
    }

    public void Score3() // 3�� ȹ��
    {
        score += 3;
        scoreTxt.text = "Score : " + score;
    }

    public void GameStart()
    {
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        StartCoroutine(CreatecoinRoutine());
        StartCoroutine(Createcoin2Routine());
        panel.SetActive(false);
    }

    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(CreatepoopRoutine());
        StopCoroutine(CreatecoinRoutine());
        StopCoroutine(Createcoin2Routine());

        if(score >= PlayerPrefs.GetInt("BestScore", 0))
        PlayerPrefs.SetInt("BestScore", score);

        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        panel.SetActive(true);
    }

    IEnumerator CreatepoopRoutine()
    {
        while (true)
        {
            CreatePoop();
            yield return new WaitForSeconds(0.9f);
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

    private void CreatePoop()
    {
        a = Random.Range(0.0f, 1.0f);
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(a, 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(poop, pos, Quaternion.identity);
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
        if(score >= 10)
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
