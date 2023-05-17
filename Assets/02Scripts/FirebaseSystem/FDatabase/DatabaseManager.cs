using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using Firebase.Database;
using Firebase.Unity;
using static System.Net.Mime.MediaTypeNames; // using Firebase.Unity.Editor; ������ Editor ����

public class DatabaseManager : MonoBehaviour
{
    public string nameField;
    public string scoreField;

    public string name = "ABA";
    public string score= "12";

    // json ���Ϸ� ����� ���� class ����
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
    // databasereference ���� ����
    private DatabaseReference databaseReference;

    private void Start()
    {
        // ������ ������ databasereference�� �ν���Ʈ�� �ʿ�
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
}
