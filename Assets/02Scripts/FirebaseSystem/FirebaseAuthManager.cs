using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;

public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new FirebaseAuthManager();
            }

            return instance;
        }
    }


    public Action<bool> LoginState;

    private FirebaseAuth auth;
    private FirebaseUser user;

    LoginSystem loginSystem;

    public string UserId => user.UserId;

    public bool isLogin;

    public void Init()
    {
        isLogin = false;
        loginSystem = GameObject.Find("Canvas").GetComponent<LoginSystem>();
        auth = FirebaseAuth.DefaultInstance;
        auth.StateChanged += OnChanged;
        Logout();
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if(auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if(!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("�α���");
                LoginState?.Invoke(true);
            }
        }
    }
    public void Create(string id, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(id, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("ȸ������ ���");
                return;
            }

            if (task.IsFaulted)
            {
                // ȸ������ ���� ���� => �̸޹� ������ / ��� ���� / �̹� ���Ե� �̸���
                Debug.LogError("ȸ������ ����");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("ȸ������ �Ϸ�");
            Logout();
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("�α��� ���");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("�α��� ����");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("�α��� �Ϸ�");
            isLogin = true;
            //LoginPanel.SetActive(false);

        });
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
        isLogin = false;
    }
}
