using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Firebase.Database;
using Firebase.Unity;
using static System.Net.Mime.MediaTypeNames;
using System;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class DatabaseManager : MonoBehaviour
{

    public class Data
    {
        // name = 게임이름
        public string id;
        public string score;

        public Data(string id, string score)
        {
            this.id = id;
            this.score = score;
        }
    }


    public struct ScoreArray
    {
        public string id;
        public float score;

        public ScoreArray(string id, float score)
        {
            this.id = id;
            this.score = score;
        }
    }

    [Header("User")]
    public string name; // 게임이름
    public string score; // 


    [Header("Login")]
    public TMP_InputField IDField;
    public TMP_Text text123;
    public string userid;

    private DatabaseReference databaseReference;

    int count = 1;


    [Header("Ranking")]
    public TMP_Text[] Rank_name1 = new TMP_Text[10];
    public TMP_Text[] Rank_score1 = new TMP_Text[10];

    public TMP_Text[] Rank_name2 = new TMP_Text[10];
    public TMP_Text[] Rank_score2 = new TMP_Text[10];

    public TMP_Text[] Rank_name3 = new TMP_Text[10];
    public TMP_Text[] Rank_score3 = new TMP_Text[10];

    ScoreArray[] sarray;

    private float[] scoreRankf;
    private string[] scoreRank;
    private string[] textRank;
    private long strLen;

    private bool textLoadBool = false;

    private bool G1 = false;
    private bool G2 = false;
    private bool G3 = false;


    [Header("Game 1")]
    public float score1_1; // 원래
    public float score1_2; // firebase 최고기록
    public string tmp1;


    [Header("Game 2")]
    public float score2_1;
    public float score2_2;
    public string tmp2;

    [Header("Game 3")]
    public float score3_1;
    public float score3_2;
    public string tmp3;

    private void Start()
    {

        if (LoginState.LoginOk == true) { 
            userid = LoginState.LoginId;
        }
        // 데이터 쓰려면 databasereference의 인스턴트가 필요
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        IdUpdate();

        // 현재 첫번째 Text UI가 "Loading" 이면,
        // 즉, 스크립트를 컴포넌트하고있는 게임 오브젝트가 Activeself(true) 이면,
        /*
        if (LoginState.isMain) { 
        if (Rank_score1[0].text == "None")
        {
            readUser("Game1");
        }
        if (Rank_score2[0].text == "None")
        {
            readUser("Game2");
        }
        if (Rank_score3[0].text == "None")
        {
            readUser("Game3");
        }
        }
        */
    }

    private void LateUpdate()
    {

        if (textLoadBool) {
            TextLoad();
        }
        
        // if (Time.timeScale != 0.0f) Time.timeScale = 0.0f;


    }

    public void IdUpdate()
    {
        if (FirebaseAuthManager.Instance.isLogin == false && LoginState.isMain == false)
        {
            userid = IDField.text;
        }
        else if (FirebaseAuthManager.Instance.isLogin == true)
        {
            LoginState.LoginId = userid;
        }
        
        if (LoginState.LoginOk)
        {
            userid = LoginState.LoginId;
        }
    }


    // writeNewUser = 랭킹 등록
    // 데이터 입력
    public void writeNewUser(string userid, string score)
    {
        // 클래스 UserScore만들고 받아온 name, userid, score 대입
        //var userScore = new Data(name, userid ,score);
        Data userScore = new Data(userid, score);

        //대입시킨 클래스 변수 user를 json 파일로 변환
        string jsonData = JsonUtility.ToJson(userScore);

        // DatabseReference 변수에 userId의 자식으로 json 파일 업로드
        databaseReference.Child(userid).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(name).Child("num" + count.ToString()).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(userid).SetRawJsonValueAsync(jsonData);
    }
    public void OnClickSaveButton1()
    {
        readScore("Game1");
        name = "Game1";
        writeNewUser(userid, score);

    }

    public void OnClickSaveButton2()
    {
        readScore("Game2");
        name = "Game2";
        writeNewUser(userid, score);

    }

    public void OnClickSaveButton3()
    {
        readScore("Game3");
        name = "Game3";
        writeNewUser(userid, score);

    }
    /*
    public void OnClickLoadButton()
    {
        email = emailField.text.Trim();

        databaseReference.Child(email).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                text.text = "로드 취소";
            }
            else if (task.IsFaulted)
            {
                text.text = "로드 실패";
            }
            else
            {
                var dataSnapshot = task.Result;

                string dataString = "";
                foreach (var data in dataSnapshot.Children)
                {
                    dataString += data.Key + " " + data.Value + "\n";
                }

                text.text = dataString;
            }
        });
    }
    */


    // 데이터 출력
    private void readScore(string name)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(name);

        //reference의 자식(userId)를 task로 받음
        //databaseReference.Child(userid).GetValueAsync().ContinueWith(task =>
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //DataSnapShot 변수를 선언하여 task의 결과 값을 받음
                DataSnapshot snapshot = task.Result;

                int co = 0;
                strLen = snapshot.ChildrenCount;
                scoreRank = new string[strLen];
                scoreRankf = new float[strLen];
                textRank = new string[strLen];
                sarray = new ScoreArray[strLen];
                //foreach 문으로 각각 데이터를 IDictionary로 변환해 각 이름에 맞게 변수 초기화
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    textRank[co] = personInfo["id"].ToString();
                    scoreRank[co] = personInfo["score"].ToString();

                    sarray[co] = new ScoreArray(textRank[co], float.Parse(scoreRank[co]));
                    co++;
                }
                findScoreName();
            }
        });
    }

    private void findScoreName()
    {
       for(int i = 0; i<sarray.Length; i++)
        {
            if (sarray[i].id == userid)
            {
                score1_2 = sarray[i].score;

                if (score1_2 >= score1_1)
                {
                    score = score1_2.ToString();
                    break;
                }
                else
                {
                    score = score1_1.ToString();
                    break;
                }
                
            }
            else
            {
                score = score1_1.ToString();
            }
        }
    }
    public void LoadButton1()
    {
        readUser("Game1");
        G1 = true;
        G2 = false;
        G3 = false;
    }

    public void LoadButton2()
    {
        readUser("Game2");
        G1 = false;
        G2 = true;
        G3 = false;
    }

    public void LoadButton3()
    {
        readUser("Game3");
        G1 = false;
        G2 = false;
        G3 = true;
    }

    private void readUser(string name)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(name);

        //reference의 자식(userId)를 task로 받음
        //databaseReference.Child(userid).GetValueAsync().ContinueWith(task =>
         databaseReference.GetValueAsync().ContinueWith(task =>
         {
            if (task.IsFaulted)
            {
                // Handle the error
                Debug.Log("error");
            }
            else if (task.IsCompleted)
            {
                //DataSnapShot 변수를 선언하여 task의 결과 값을 받음
                DataSnapshot snapshot = task.Result;

                 int co = 0;
                 strLen = snapshot.ChildrenCount;
                 scoreRank = new string[strLen];
                 scoreRankf = new float[strLen];
                 textRank = new string[strLen];
                 sarray = new ScoreArray[strLen];
                 //foreach 문으로 각각 데이터를 IDictionary로 변환해 각 이름에 맞게 변수 초기화
                 foreach (DataSnapshot data in snapshot.Children)
                 {
                    IDictionary personInfo = (IDictionary)data.Value;
                     textRank[co] = personInfo["id"].ToString();
                     scoreRank[co] = personInfo["score"].ToString();
                     
                     sarray[co] = new ScoreArray(textRank[co], float.Parse(scoreRank[co]));

                     Debug.Log("name : " + personInfo["id"] + ", score: " + personInfo["score"]);
                     co++;
                 }
                 SetScoreArray();
                 textLoadBool = true;

            }
        });
    }

    public void TextLoad()
    {
        textLoadBool = false;
        /*
        for(int i = 0; i< Rank.Length; i++)
        {
            if(strLen <= i) return;
            Rank[i].text = scoreRankf[i].ToString();
        }
        */

        if (G1 == true && G2 == false && G3 == false) { 
            for (int i = 0; i < Rank_name1.Length; i++)
            {
            if (strLen <= i) return;
            UnityEngine.Debug.Log("TRY");
            Rank_name1[i].text = sarray[i].id;
            Rank_score1[i].text = sarray[i].score.ToString();
            }
        }
        else if(G2 == true && G1 == false && G3 == false)
        {
            for (int i = 0; i < Rank_name2.Length; i++)
            {
                if (strLen <= i) return;
                UnityEngine.Debug.Log("TRY");
                Rank_name2[i].text = sarray[i].id;
                Rank_score2[i].text = sarray[i].score.ToString();
            }
        }
        else if (G3 == true && G1 == false && G2 == false)
        {
            for (int i = 0; i < Rank_name3.Length; i++)
            {
                if (strLen <= i) return;
                UnityEngine.Debug.Log("TRY");
                Rank_name3[i].text = sarray[i].id;
                Rank_score3[i].text = sarray[i].score.ToString();
            }
        }
    }

    public void SetScoreArray()
    {
        /*
        for (int i = 0; i < strLen; i++)
        {
            scoreRankf[i] = sarray[i].score;
        }
        Array.Sort(scoreRankf);
        Array.Reverse(scoreRankf);
        */

        Array.Sort(sarray, (x, y) => x.score.CompareTo(y.score));
        Array.Reverse(sarray);
     
    }

    /*
    public void DataLoad()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(score);

        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                UnityEngine.Debug.Log("ABC");
                DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int count1 = 0;
                strLen = snapshot.ChildrenCount;
                UnityEngine.Debug.Log(strLen);
                scoreRank = new string[strLen];


                foreach (DataSnapshot data in snapshot.Children)
                {
                    // 받은 데이터들을 하나씩 잘라 string 배열에 저장
                    IDictionary rankInfo = (IDictionary)data.Value;
                    UnityEngine.Debug.Log("ooo2");
                    Rank_name1[0].text = "isreal";
                    //scoreRank[count1] = rankInfo["name"].ToString() + " | " + string.Format("{0:N2}", rankInfo["score"]);
                    UnityEngine.Debug.Log("ooo3");
                    count1++;
                }
            }
        });
    }
    */
}
