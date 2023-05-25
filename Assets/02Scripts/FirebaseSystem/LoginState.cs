using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LoginState : MonoBehaviour
{
    static public bool LoginOk;
    static public string LoginId;
    static public bool isMain;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(FirebaseAuthManager.Instance.isLogin == true)
        {
            LoginOk = true;
        }
        else if(FirebaseAuthManager.Instance.isLogin == false)
        {
            LoginOk = false;
        }
    }
    
    public void MainScene()
    {
        isMain = true;
    }

    public void MainSceneNot()
    {
        UnityEngine.Debug.Log("IsMain : False");
        isMain = false;
    }
}
