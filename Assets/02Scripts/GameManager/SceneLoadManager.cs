using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void AvoidScene()
    {
        SceneManager.LoadScene("GameScene1");
    }

    public void JumpScene()
    {
        SceneManager.LoadScene("GameScene2");
    }

    public void PickmeScene()
    {
        SceneManager.LoadScene("GameScene3");
    }

    public void MainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void LoadingScene()
    {
        LoadingManager.LoadScene("MainScene");
    }
}
