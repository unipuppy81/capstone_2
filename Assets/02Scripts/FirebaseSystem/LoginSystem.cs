using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.Reflection;

public class LoginSystem : MonoBehaviour
{
    //public TMP_InputField name;
    public TMP_InputField id;
    public TMP_InputField password;

    public TMP_InputField id_signUp;
    public TMP_InputField pw_signUp;

    public TMP_Text outputText;

    public GameObject LoginPanel;
    public GameObject TPanel;
    public GameObject SignupPanel;


    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
        FirebaseAuthManager.Instance.Logout();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FirebaseAuthManager.Instance.Logout();
        }
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
        else if (!sign)
        {
            LoginPanel.SetActive(true);
            TPanel.SetActive(false);
        }
    }

    public void Create()
    {
        string i = id_signUp.text;
        string p = pw_signUp.text;

        FirebaseAuthManager.Instance.Create(i, p);
        SignupPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void Login()
    {
        FirebaseAuthManager.Instance.Login(id.text, password.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.Logout();
    }

    public void OpenSignup()
    {
        LoginPanel.SetActive(false);
        SignupPanel.SetActive(true);
    }

    public void BackBtn()
    {
        LoginPanel.SetActive(true);
        SignupPanel.SetActive(false);
    }
}
