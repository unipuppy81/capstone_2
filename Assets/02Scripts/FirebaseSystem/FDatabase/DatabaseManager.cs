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
    public string name; // �����̸�
    public string score; // 


    [Header("Login")]
    public TMP_InputField IDField;
    public TMP_Text text123;
    public string userid;

    private DatabaseReference databaseReference;
    ScoreG1 scoreg1;
    SceneManagerG1 smg;
    int count = 1;


    [Header("Ranking")]
    public TMP_Text[] Rank_name1 = new TMP_Text[10];
    public TMP_Text[] Rank_score1 = new TMP_Text[10];

    public TMP_Text[] Rank_name2 = new TMP_Text[10];
    public TMP_Text[] Rank_score2 = new TMP_Text[10];

    public TMP_Text[] Rank_name3 = new TMP_Text[10];
    public TMP_Text[] Rank_score3 = new TMP_Text[10];

    ScoreArray[] sarray, sarray2;   // srray2 = ���� ������ ���Ҷ�

    private float[] scoreRankf;
    private string[] scoreRank;
    private string[] textRank;
    private long strLen;

    private bool textLoadBool = false;

    private bool G1 = false;
    private bool G2 = false;
    private bool G3 = false;


    [Header("Game 1")]
    public float score1_1; // ���� �ְ��� vs ������
    public float score1_2; // �̱���� ���⿡ ��Ƽ� ���
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
        // ������ ������ databasereference�� �ν���Ʈ�� �ʿ�
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        IdUpdate();

     
        
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
            int endIndex = userid.Length - 4;
            userid = userid.Substring(0, endIndex);
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
    public void writeNewUser2(string userid, string score)
    {
        // Ŭ���� UserScore����� �޾ƿ� name, userid, score ����
        //var userScore = new Data(name, userid ,score);
        Data userScore = new Data(userid, score);

        //���Խ�Ų Ŭ���� ���� user�� json ���Ϸ� ��ȯ
        string jsonData = JsonUtility.ToJson(userScore);

        // DatabseReference ������ userId�� �ڽ����� json ���� ���ε�
        databaseReference.Child(userid).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(name).Child("num" + count.ToString()).SetRawJsonValueAsync(jsonData);

        //databaseReference.Child(userid).SetRawJsonValueAsync(jsonData);
    }
    public void Onclick()
    {
        name = "Game1";
        writeNewUser(userid, score);
    }
    public void FirstSaveButton1()
    {
        name = "Game1";
    }

    public void OnClickSaveButton1()
    {
        name = "Game1";
        readScore2(name);
    }



    public void OnClickSaveButton2()
    {
        name = "Game2";
        UnityEngine.Debug.Log("PPP");
        readScore2(name);
        
    }

    public void OnClickSaveButton3()
    {
        name = "Game3";
        UnityEngine.Debug.Log("OnclickBtn3");
        readScore2(name);
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


    // ������ ���
    public void readScore(string name) // ���� ������  �ְ����̶� ��
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(name);

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
                sarray2 = new ScoreArray[strLen];
                //foreach ������ ���� �����͸� IDictionary�� ��ȯ�� �� �̸��� �°� ���� �ʱ�ȭ
                foreach (DataSnapshot data in snapshot.Children)
                {
                    UnityEngine.Debug.Log(strLen);
                    IDictionary personInfo = (IDictionary)data.Value;
                    textRank[co] = personInfo["id"].ToString();
                    scoreRank[co] = personInfo["score"].ToString();


                    sarray2[co] = new ScoreArray(textRank[co], float.Parse(scoreRank[co]));
                    Debug.Log("ReadScore :: " + "name : " + personInfo["id"] + ", score: " + personInfo["score"]);
                    co++;
                }
                if(name == "Game1") {
                    findScoreName();
                }
                else if(name == "Game2")
                {
                    findScoreName2();
                }
                else if (name == "Game3")
                {
                    findScoreName3();
                }

                
            }
        });
    }
    private void findScoreName()
    {
        UnityEngine.Debug.Log("POW");

        if (score1_2 >= score1_1)
        {
            tmp1 = score1_2.ToString("N2");
        }
        else if (score1_1 > score1_2)
        {
            tmp1 = score1_1.ToString("N2");
        }


        writeNewUser2(userid, tmp1);
    }

    private void findScoreName2()
    {
        UnityEngine.Debug.Log("POW");

        if (score2_2 >= score2_1)
        {
            UnityEngine.Debug.Log("score2_2 : " + score2_2);
            tmp2 = score2_2.ToString();
        }
        else if (score2_1 > score2_2)
        {
            UnityEngine.Debug.Log("score2_1 : " + score2_1);
            tmp2 = score2_1.ToString();
        }

        UnityEngine.Debug.Log(tmp2);

        writeNewUser2(userid, tmp2);
    }
    private void findScoreName3()
    {
        UnityEngine.Debug.Log("POW");

        if (score3_2 >= score3_1)
        {
            tmp3 = score3_2.ToString();
        }
        else if (score3_1 > score3_2)
        {
            tmp3 = score3_1.ToString();
        }


        writeNewUser2(userid, tmp3);
    }
    private void findBestScore()
    {
        UnityEngine.Debug.Log("POW1");
        for (int i = 0; i < sarray.Length; i++)
        {
            string said = sarray[i].id;

            if (said == userid)
            {
                float tmp = sarray[i].score;
                UnityEngine.Debug.Log("�ְ� tmp1 : " + tmp);
                tmp1 = tmp.ToString("N2");
                tmp2 = tmp.ToString();
                tmp3 = tmp.ToString();
                break;
            }
        }
        UnityEngine.Debug.Log("tmp1 : " + tmp1);
        //writeNewUser2(userid, tmp1);
    }

    private void findBestScore2()
    {
        UnityEngine.Debug.Log("POW2");
        for (int i = 0; i < sarray.Length; i++)
        {
            string said = sarray[i].id;

            if (said == userid)
            {
                float tmp = sarray[i].score;
                int tmp22 = Mathf.FloorToInt(tmp);
                UnityEngine.Debug.Log("�ְ� tmp2 : " + tmp22);
                tmp2 = tmp22.ToString();
                break;
            }
        }
        UnityEngine.Debug.Log("tmp2 : " + tmp2);
        //writeNewUser2(userid, tmp1);
    }
    private void findBestScore3()
    {
        UnityEngine.Debug.Log("POW3");
        for (int i = 0; i < sarray.Length; i++)
        {
            string said = sarray[i].id;

            if (said == userid)
            {
                float tmp = sarray[i].score;
                UnityEngine.Debug.Log("�ְ� tmp3 : " + tmp);

                tmp3 = tmp.ToString();
                break;
            }
        }
        UnityEngine.Debug.Log("tmp3 : " + tmp3);
        //writeNewUser2(userid, tmp1);
    }
    private void readScore2(string name) // �ְ��� �޾ƿ��� �뵵
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(name);

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
                    UnityEngine.Debug.Log(strLen);
                    IDictionary personInfo = (IDictionary)data.Value;
                    textRank[co] = personInfo["id"].ToString();
                    scoreRank[co] = personInfo["score"].ToString();


                    sarray[co] = new ScoreArray(textRank[co], float.Parse(scoreRank[co]));
                    Debug.Log("ReadScore :: " + "name : " + personInfo["id"] + ", score: " + personInfo["score"]);
                    co++;
                }
                if(name == "Game1") { 
                findBestScore();
                }
                else if (name == "Game2")
                {
                    findBestScore2();

                }
                else if(name == "Game2")
                {
                    findBestScore3();
                }

            }
        });

       
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

    //��ŷ
    private void readUser(string name)
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(name);

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
           
            Rank_name1[i].text = sarray[i].id;
            Rank_score1[i].text = sarray[i].score.ToString();
            }
        }
        else if(G2 == true && G1 == false && G3 == false)
        {
            for (int i = 0; i < Rank_name2.Length; i++)
            {
                if (strLen <= i) return;
               
                Rank_name2[i].text = sarray[i].id;
                Rank_score2[i].text = sarray[i].score.ToString();
            }
        }
        else if (G3 == true && G1 == false && G2 == false)
        {
            for (int i = 0; i < Rank_name3.Length; i++)
            {
                if (strLen <= i) return;
                
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
    */
}
