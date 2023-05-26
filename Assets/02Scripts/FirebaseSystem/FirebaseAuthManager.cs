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
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }

            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
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
                Debug.LogError("회원가입 취소");
                return;
            }

            if (task.IsFaulted)
            {
                // 회원가입 실패 이유 => 이메밀 비정상 / 비번 간단 / 이미 가입된 이메일
                Debug.LogError("회원가입 실패");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("회원가입 완료");
            Logout();
        });
    }

    public void Login(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("로그인 취소");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("로그인 실패");
                return;
            }

            FirebaseUser newUser = task.Result.User;
            Debug.LogError("로그인 완료");
            isLogin = true;
            //LoginPanel.SetActive(false);

        });
    }

    public void Logout()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
        isLogin = false;
    }
}
