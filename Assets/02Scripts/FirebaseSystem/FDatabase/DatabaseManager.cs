using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Firebase.Database;
using Firebase.Unity;
using static System.Net.Mime.MediaTypeNames;
using System;

public class DatabaseManager : MonoBehaviour
{

    public class Data
    {
        public string name;
        public string score;

        public Data(string name, string score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public string nameField;
    public string IDField;

    public string scoreField;

    public string name;
    public string userid;

    public string score;

    private DatabaseReference databaseReference;
    int count = 1;

    public TMP_Text[] Rank = new TMP_Text[10];

    private string[] strRank;
    private long strLen;

    private bool textLoadBool = false;

    private void Start()
    {
        userid = "naver";

        // ������ ������ databasereference�� �ν���Ʈ�� �ʿ�
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        // ���� ù��° Text UI�� "Loading" �̸�,
        // ��, ��ũ��Ʈ�� ������Ʈ�ϰ��ִ� ���� ������Ʈ�� Activeself(true) �̸�,
        /*
        if (Rank[0].text == "Loading..")
        {
            DataLoad();
        }
        */
    }

    private void LateUpdate()
    {
        /*
        if (textLoadBool)
        {
            TextLoad();
        }
        if (Time.timeScale != 0.0f) Time.timeScale = 0.0f;
        */

    }

    public void DataLoad()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference("rank");

        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                DataLoad();
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                int count1 = 0;
                strLen = snapshot.ChildrenCount;
                strRank = new string[strLen];

                foreach (DataSnapshot data in snapshot.Children)
                {
                    // ���� �����͵��� �ϳ��� �߶� string �迭�� ����
                    IDictionary rankInfo = (IDictionary)data.Value;

                    strRank[count] = rankInfo["name"].ToString() + " | " + string.Format("{0:N2}", rankInfo["score"]);

                    count1++;
                }
            }
        });
    }

    public void TextLoad()
    {
        textLoadBool = false;
        try
        {
            // �޾ƿ� ������ ���� =?> ���������� �Ʒ���
            Array.Sort(strRank, (x, y) => string.Compare(
                y.Substring(y.Length - 5, 5).ToString() + x.Substring(x.Length - 5, 5).ToString(),
                x.Substring(x.Length - 5, 5).ToString() + y.Substring(y.Length - 5, 5).ToString()));
        }
        catch(NullReferenceException e)
        {
            return;
        }

        for(int i = 0; i < Rank.Length; i++)
        {
            if (strLen <= i) return;
            Rank[i].text = strRank[i];
        }
    }

    public void writeNewUser(string userId, string name, string email)
    {
        var userScore = new Data(name, score);
        string jsonData = JsonUtility.ToJson(userScore);

        databaseReference.Child(userid).Child("num" + count.ToString()).SetRawJsonValueAsync(jsonData);
        //databaseReference.Child(email).SetRawJsonValueAsync(jsonData);
    }
    public void OnClickSaveButton()
    {
        writeNewUser(userid, name, score);
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
        readUser("Perzzz");
    }
    private void readUser(string userid)
    {
        string set = "naver";
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(set);

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
                // Do something with snapshot ...
                // snapShot�� �ڽ� ������ Ȯ��
                Debug.Log(snapshot.ChildrenCount);

                //foreach ������ ���� �����͸� IDictionary�� ��ȯ�� �� �̸��� �°� ���� �ʱ�ȭ
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("name : " + personInfo["name"] + ", score: " + personInfo["score"]);
                }
            }
        });
    }


}
