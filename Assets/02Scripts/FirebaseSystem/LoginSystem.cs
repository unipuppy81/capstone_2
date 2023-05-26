using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System.Reflection;
using UnityEngine.Rendering;

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
    public GameObject OkayPanel;
    public GameObject ErrorPanel;
    public TextMeshProUGUI ErrorPanelText;
    public GameObject LogErrorPanel;
    public TextMeshProUGUI LogErrorText;

    public string errorText;  // 회원가입
    public string loginerrorText;

    void Awake()
    {
        FirebaseAuthManager.Instance.Init();
    }

    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
       
        //FirebaseAuthManager.Instance.Logout();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            FirebaseAuthManager.Instance.Logout();
        }

        errorText = FirebaseAuthManager.Instance.errortextmesh.ToString();
        ErrorPanelText.text = errorText;

        loginerrorText = FirebaseAuthManager.Instance.loginerrortext.ToString();
        LogErrorText.text = loginerrorText;

        if (FirebaseAuthManager.Instance.isLogin == true)
        {
            LogErrorPanel.SetActive(false);
        }
        

        
        /*
        if(FirebaseAuthManager.Instance.isLogin == true){
            PanelSet();
            FirebaseAuthManager.Instance.isLogin = false;
        }
        */
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

        Error();


        //UnityEngine.Debug.Log("ABC");
        //FirebaseAuthManager.Instance.Logout();
    }

    public void PanelSet()
    {
        id.text = string.Empty;
        password.text = string.Empty;

        SignupPanel.SetActive(false);
        LoginPanel.SetActive(true);
        TPanel.SetActive(false);
    }

    public void Login()
    {
        FirebaseAuthManager.Instance.Login(id.text, password.text);
        if(FirebaseAuthManager.Instance.isLogin == false)
        {
            LoginPanel.SetActive(true);
        }

        LogErrorPanel.SetActive(true);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.Logout();
    }

    public void OpenSignup()
    {
        id_signUp.text = string.Empty;
        pw_signUp.text = string.Empty;


        LoginPanel.SetActive(false);
        SignupPanel.SetActive(true);
    }

    public void BackBtn()
    {
        id.text = string.Empty;
        password.text = string.Empty;

        LoginPanel.SetActive(true);
        SignupPanel.SetActive(false);

    }
    public void OkcloseBtn()
    {
        OkayPanel.SetActive(false);
        SignupPanel.SetActive(false);
    }
    public void ErrorCloseBtn()
    {
        ErrorPanel.SetActive(false);
    }
    public void LogErrorCloseBtn()
    {
        LogErrorPanel.SetActive(false);
    }
    public void Error()
    {


        ErrorPanel.SetActive(true);
 
    }
}
