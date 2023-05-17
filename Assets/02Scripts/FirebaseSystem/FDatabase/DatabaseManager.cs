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

    public string name = "ABA";
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

    private void Start()
    {
        // 데이터 쓰려면 databasereference의 인스턴트가 필요
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSaveButton()
    {
        //name = nameField.text.Trim();
        //score = scoreField.text.Trim();

        var userScore = new Data(name, score);
        string jsonData = JsonUtility.ToJson(userScore);

        databaseReference.Child(name).SetRawJsonValueAsync(jsonData);
        //databaseReference.Child(email).SetRawJsonValueAsync(jsonData);
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
}
