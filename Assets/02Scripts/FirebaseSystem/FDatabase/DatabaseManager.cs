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

        // 데이터 쓰려면 databasereference의 인스턴트가 필요
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Update()
    {
        // 현재 첫번째 Text UI가 "Loading" 이면,
        // 즉, 스크립트를 컴포넌트하고있는 게임 오브젝트가 Activeself(true) 이면,
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
                    // 받은 데이터들을 하나씩 잘라 string 배열에 저장
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
            // 받아온 데이터 정렬 =?> 위에서부터 아래로
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

    public void LoadButton()
    {
        readUser("Perzzz");
    }
    private void readUser(string userid)
    {
        string set = "naver";
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference(set);

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
                // Do something with snapshot ...
                // snapShot의 자식 개수를 확인
                Debug.Log(snapshot.ChildrenCount);

                //foreach 문으로 각각 데이터를 IDictionary로 변환해 각 이름에 맞게 변수 초기화
                foreach (DataSnapshot data in snapshot.Children)
                {
                    IDictionary personInfo = (IDictionary)data.Value;
                    Debug.Log("name : " + personInfo["name"] + ", score: " + personInfo["score"]);
                }
            }
        });
    }


}
