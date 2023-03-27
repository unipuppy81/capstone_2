using System.Collections;
using System.Collections.Generic;
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
    private GameObject poop;
    [SerializeField]
    private GameObject coin;

    private int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

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

    public void Score() // 1Á¡ È¹µæ
    {
        score++;
        scoreTxt.text = "Score : " + score;
    }

    public void Score2() // 2Á¡ È¹µæ
    {
        score += 2;
        scoreTxt.text = "Score : " + score;
    }

    public void Score3() // 3Á¡ È¹µæ
    {
        score += 3;
        scoreTxt.text = "Score : " + score;
    }

    public void GameStart()
    {
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        StartCoroutine(CreatecoinRoutine());
        panel.SetActive(false);
    }

    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(CreatepoopRoutine());
        StopCoroutine(CreatecoinRoutine());

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
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator CreatecoinRoutine()
    {
        while (true)
        {
            CreateCoin();
            yield return new WaitForSeconds(1.5f);
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
}
