using System.Collections;
using System.Collections.Generic;
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
    private GameObject poop;

    private int score;

    [SerializeField]
    private Text scoreTxt;

    [SerializeField]
    private Text bestScore;
    [SerializeField]
    private GameObject panel;

    public bool stopTrigger = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score()
    {
        score++;
        scoreTxt.text = "Score : " + score;
    }

    public void GameStart()
    {
        stopTrigger = true;
        StartCoroutine(CreatepoopRoutine());
        panel.SetActive(false);
    }

    public void GameOver()
    {
        stopTrigger = false;

        StopCoroutine(CreatepoopRoutine());

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
            yield return new WaitForSeconds(1);
        }
    }

    private void CreatePoop()
    {
        Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(UnityEngine.Random.Range(0.0f, 1.0f), 1.1f, 0));
        pos.z = 0.0f;
        Instantiate(poop, pos, Quaternion.identity);
    }
}
