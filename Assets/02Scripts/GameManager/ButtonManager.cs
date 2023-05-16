using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingPanel;

    public void ContinueBtn()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }
    public void SettingBtn()
    {
        SettingPanel.SetActive(true);
    }
    public void closeBtn()
    {
        SettingPanel.SetActive(false);
    }
}
