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

    public string userid = "222@222.22";
    public string name = "�����";
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
    int count = 1;

    private void Start()
    {
        userid = "naver";

        // ������ ������ databasereference�� �ν���Ʈ�� �ʿ�
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
        databaseReference = FirebaseDatabase.DefaultInstance.GetReference("naver");

        //reference�� �ڽ�(userId)�� task�� ����
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
