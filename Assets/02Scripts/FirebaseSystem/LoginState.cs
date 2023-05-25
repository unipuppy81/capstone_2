using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoginState : MonoBehaviour
{
    static public bool LoginOk;
    static public string LoginId;

    // Start is called before the first frame update
    void Start()
    {
        LoginOk = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(FirebaseAuthManager.Instance.isLogin == true)
        {
            LoginOk = true;
        }
    }
}
