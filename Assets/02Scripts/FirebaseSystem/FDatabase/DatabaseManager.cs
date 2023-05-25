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
        // name = �����̸�
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
    public string name;
    public string score;


    [Header("Login")]
    public TMP_InputField IDField;
    public TMP_Text text123;
    public string userid;

    private DatabaseReference databaseReference;

    int count = 1;


    [Header("Ranking")]
    public TMP_Text[] Rank_name1 = new TMP_Text[10];
    public TMP_Text[] Rank_score1 = new TMP_Text[10];
    ScoreArray[] sarray;

    private float[] scoreRankf;
    private string[] scoreRank;
    private string[] textRank;
    private long strLen;

    private bool textLoadBool = false;



    [Header("Game 1")]
    public string a;

    [Header("Game 2")]
    public string b;

    [Header("Game 3")]
    public string c;

    private void Start()
    {

        if (LoginState.LoginOk == true) { 
            userid = LoginState.LoginId;
        }

        name = "Game1";
        // ������ ������ databasereference�� �ν���Ʈ�� �ʿ�
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        //IdUpdate();
        
        // ���� ù��° Text UI�� "Loading" �̸�,
        // ��, ��ũ��Ʈ�� ������Ʈ�ϰ��ִ� ���� ������Ʈ�� Activeself(true) �̸�,
        /*
        if (Rank_name[0].text == "None")
        {
            DataLoad();
        }
        */

    }

    private void LateUpdate()
    {

        if (textLoadBool) {
            TextLoad2();
            //TextLoad();
        }
        
        // if (Time.timeScale != 0.0f) Time.timeScale = 0.0f;


    }

    public void IdUpdate()
    {
        if (FirebaseAuthManager.Instance.isLogin == false)
        {
            userid = IDField.text;
        }
        else if (FirebaseAuthManager.Instance.isLogin == true)
        {
            LoginState.LoginId = userid;
        }
    }


    // writeNewUser = ��ŷ ���
    // ������ �Է�
    public void writeNewUser(string userid, string score)
    {
        // Ŭ���� UserScore����� �޾ƿ� name, userid, score ����
        //var userScore = new Data(name, userid ,score);
        Data userScore = new Data(userid, score);

        //���Խ�Ų Ŭ���� ���� user�� json ���Ϸ� ��ȯ
        string jsonData = JsonUtility.ToJson(userScore);

        // DatabseReference ������ userId�� �ڽ����� json ���� ���ε�
        databaseReference.Child(name).Child(userid).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(name).Child("num" + count.ToString()).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(userid).SetRawJsonValueAsync(jsonData);
    }
    public void OnClickSaveButton()
    {
        writeNewUser(userid, score);
        count++;
    }
    /*
    public void OnClickLoadButton()
    {
        email = emailField.text.Trim();

        databaseReference.Child(email).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                text.text = "�ε� ���";
            }
            else if (task.IsFaulted)
            {
                text.text = "�ε� ����";
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
    public void LoadButton()
    {
        readUser(name);
    }

    // ������ ���
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
                    // ���� �����͵��� �ϳ��� �߶� string �迭�� ����
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

   
    private void readUser(string userid)
    {




        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(userid);

        //reference�� �ڽ�(userId)�� task�� ����
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
                //DataSnapShot ������ �����Ͽ� task�� ��� ���� ����
                DataSnapshot snapshot = task.Result;

                 int co = 0;
                 strLen = snapshot.ChildrenCount;
                 scoreRank = new string[strLen];
                 scoreRankf = new float[strLen];
                 textRank = new string[strLen];
                 sarray = new ScoreArray[strLen];
                 //foreach ������ ���� �����͸� IDictionary�� ��ȯ�� �� �̸��� �°� ���� �ʱ�ȭ
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
        try
        {
            // �޾ƿ� ������ ���� =?> ���������� �Ʒ���
            Array.Sort(textRank, (x, y) => string.Compare(
                y.Substring(y.Length - 5, 5).ToString() + x.Substring(x.Length - 5, 5).ToString(),
                x.Substring(x.Length - 5, 5).ToString() + y.Substring(y.Length - 5, 5).ToString()));
        }
        catch (NullReferenceException e)
        {
            return;
        }

        for (int i = 0; i < Rank_name1.Length; i++)
        {
            if (strLen <= i) return;
            Rank_score1[i].text = textRank[i];
        }
    }

    public void TextLoad2()
    {
        textLoadBool = false;
        /*
        for(int i = 0; i< Rank.Length; i++)
        {
            if(strLen <= i) return;
            Rank[i].text = scoreRankf[i].ToString();
        }
        */

        for(int i = 0; i< Rank_name1.Length; i++)
        {
            if (strLen <= i) return;
            Rank_score1[i].text = sarray[i].score.ToString();
            Rank_name1[i].text = sarray[i].id;
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
}
