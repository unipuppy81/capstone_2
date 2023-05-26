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
    public bool isSign;
    public bool isSign2;

    public string errortextmesh; // ȸ�����Կ�
    public string loginerrortext;
    public void Init()
    {
        errortextmesh = string.Empty;
        loginerrortext = "�α��� Ȯ�� ��..";
        isLogin = false;  // fasle ����
        isSign = false;
        isSign2 = false;
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
                isSign = false;
                errortextmesh = "ȸ�����Կ� �����Ͽ����ϴ�. ���̵� �� ��й�ȣ�� Ȯ�����ּ���.";
                Debug.LogError("ȸ������ ����");
                return;
            }
            isSign2 = true;
            FirebaseUser newUser = task.Result.User;
            Debug.LogError("ȸ������ �Ϸ�");
            errortextmesh = "ȸ�����Կ� �����Ͽ����ϴ�. �α���â���� �α����� ���ּ���.";
            Logout();
        });
    }

    public void Login(string email, string password)
    {
        loginerrortext = "�α��� Ȯ�� ��..";
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("�α��� ���");
                return;
            }

            if (task.IsFaulted)
            {
                loginerrortext = "�α��� ����";
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
