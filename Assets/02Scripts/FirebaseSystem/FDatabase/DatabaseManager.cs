using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using static System.Net.Mime.MediaTypeNames;

public class DatabaseManager : MonoBehaviour
{
    public TMP_InputField emailField;
    public TMP_InputField levelField;
    public TMP_InputField goldField;
    public TMP_Text text;

    public string email;
    public string level;
    public string gold;

    public class Data
    {
        public string level;
        public string gold;

        public Data(string level, string gold)
        {
            this.level = level;
            this.gold = gold;
        }
    }

    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void OnClickSaveButton()
    {
        email = emailField.text.Trim();
        level = levelField.text.Trim();
        gold = goldField.text.Trim();

        var data = new Data(level, gold);
        string jsonData = JsonUtility.ToJson(data);

        databaseReference.Child(email).SetRawJsonValueAsync(jsonData);

        text.text = "저장";
    }

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
}
