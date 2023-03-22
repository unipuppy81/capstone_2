using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    
    public void AvoidAvoidScene()
    {
        SceneManager.LoadScene("GameScene1");
    }
    public void JumpJumpScene()
    {
        SceneManager.LoadScene("GameScene2");
    }
    public void PickmePickmeScene()
    {
        SceneManager.LoadScene("GameScene3");
    }
    // Start is called before the first frame update
}
