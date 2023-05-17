using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Firebase.Database;
using Firebase.Unity;
using static System.Net.Mime.MediaTypeNames; // using Firebase.Unity.Editor; 에러난 Editor 삭제

public class DatabaseManager : MonoBehaviour
{
    public string nameField;
    public string scoreField;

    public string userid = "222@222.22";
    public string name = "김재우";
    public string score= "12";

    // json 파일로 만들기 위해 class 정의
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
    // databasereference 변수 선언
    private DatabaseReference databaseReference;
    int count = 1;

    private void Start()
    {
        userid = "naver";

        // 데이터 쓰려면 databasereference의 인스턴트가 필요
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    
    public void writeNewUser(string userId, string name, string email)
    {
        //name = nameField.text.Trim();
        //score = scoreField.text.Trim();

        var userScore = new Data(name, score);
        string jsonData = JsonUtility.ToJson(userScore);

        databaseReference.Child(userid).Child("num" + count.ToString()).SetRawJsonValueAsync(jsonData);
        //databaseReference.Child(email).SetRawJsonValueAsync(jsonData);
    }
    public void OnClickSaveButton()
    {
        writeNewUser("personal information", "googleman", "google@google.com");
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
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference("naver");

        //reference의 자식(userId)를 task로 받음
        //databaseReference.Child(userid).GetValueAsync().ContinueWith(task =>
         databaseReference.GetValueAsync().ContinueWith(task =>
         {
            string t;
            t = task.ToString();
            Debug.Log(task);

            

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
