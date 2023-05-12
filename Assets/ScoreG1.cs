using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreG1 : MonoBehaviour
{
    MoveG1 moveG1;
    public TextMeshProUGUI GameScore;
    public TextMeshProUGUI GameScore2;
    public TextMeshProUGUI BestScoreText;

    public float score1;
    public float bestScore;
    void Awake()
    {
        moveG1 = GameObject.Find("Player").GetComponent<MoveG1>();
        GameScore = GetComponent<TextMeshProUGUI>();
        GameScore2 = GetComponent<TextMeshProUGUI>();
        BestScoreText = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        bestScore = PlayerPrefs.GetFloat("BestScore", 0);
    }
    void Update()
    {
        score1 = moveG1.gameTime;
        GameScore.text = score1.ToString();
        //GameScore2.text = score1.ToString();
        //BestScoreText.text = bestScore.ToString();
        BestScored();
    }

    void BestScored()
    {
        if (moveG1.isDead)
        {
            if(bestScore < score1)
            {
                bestScore = score1;
            }
            PlayerPrefs.SetFloat("BestScore", bestScore);
            PlayerPrefs.Save();
        }
    }
}
