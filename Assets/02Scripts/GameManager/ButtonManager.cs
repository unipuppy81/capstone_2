using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject SettingPanel;
    public GameObject RankPanel;

    public GameObject MenuPanel;

    public GameObject Game1Btn;
    public GameObject Game2Btn;
    public GameObject Game3Btn;

    public GameObject Game1Panel;
    public GameObject Game2Panel;
    public GameObject Game3Panel;

    public GameObject RankExitBtn;
    public GameObject RankBtn;

    public void RankExit()
    {
        RankPanel.SetActive(false);
        MenuPanel.SetActive(false);
    }
    public void RankingBtn()
    {
        RankPanel.SetActive(true);
        Game1Panel.SetActive(true);
        Game2Panel.SetActive(false); 
        Game3Panel.SetActive(false);
    }

    public void Game1B()
    {
        Game1Panel.SetActive(true);
        Game2Panel.SetActive(false);
        Game3Panel.SetActive(false);
    }

    public void Game2B()
    {
        Game1Panel.SetActive(false);
        Game2Panel.SetActive(true);
        Game3Panel.SetActive(false);
    }

    public void Game3B()
    {
        Game1Panel.SetActive(false);
        Game2Panel.SetActive(false);
        Game3Panel.SetActive(true);
    }
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
        MenuPanel.SetActive(false);
    }
    public void menuPanel()
    {
        if(MenuPanel.activeSelf == true)
        {
            MenuPanel.SetActive(false);
        }
        else
        {
            MenuPanel.SetActive(true);
        }
        
    }
}
