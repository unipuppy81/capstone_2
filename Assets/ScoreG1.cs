using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreG1 : MonoBehaviour
{
    MoveG1 moveG1;
    SceneManagerG1 smg;
    DatabaseManager databaseManager;

    public TextMeshProUGUI GameScore;
    public TextMeshProUGUI GameScore2;
    public TextMeshProUGUI BestScoreText;

    public float score1;
    public string bestScore;

    public int a;
    void Awake()
    {
        moveG1 = GameObject.Find("Player").GetComponent<MoveG1>();
        smg = GameObject.Find("SceneManager").GetComponent<SceneManagerG1>();
        databaseManager = GameObject.Find("DatabaseManager").GetComponent<DatabaseManager>();
    }
    private void Start()
    {
        BestScoreUpdate();

    }
    void Update()
    {
        if (smg.isStart) {
            score1 += Time.deltaTime;
            GameScore.text = score1.ToString("N2");
            GameScore2.text = score1.ToString("N2");
        }
        if (smg.isStart == false) {
            //BestScoreText.text = "Best";
            //BestScoreUpdate();
           
        }
    }

    void BestScoreUpdate()
    {
        UnityEngine.Debug.Log("ScoreG1");
        databaseManager.OnClickSaveButton1();
        UnityEngine.Debug.Log("ScoreG2");
        bestScore = databaseManager.score.ToString();
        UnityEngine.Debug.Log(bestScore);
        UnityEngine.Debug.Log("ScoreG3");
        BestScoreText.text = bestScore;
        UnityEngine.Debug.Log("ScoreG4");
    }

   
}
