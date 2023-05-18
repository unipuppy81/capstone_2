using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.Reflection;

public class LoginSystem : MonoBehaviour
{
    public TMP_InputField email;
    public TMP_InputField password;

    public TMP_Text outputText;

    public GameObject LoginPanel;
    public GameObject TPanel;

    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "LogIn : " : "LogOut : ";
        outputText.text += FirebaseAuthManager.Instance.UserId;
        if (sign)
        {
            LoginPanel.SetActive(false);
            TPanel.SetActive(true);
        }
    }

    public void Create()
    {
        string e = email.text;
        string p = password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }
    public void Login()
    {
        FirebaseAuthManager.Instance.Login(email.text, password.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.Logout();
    }
}
