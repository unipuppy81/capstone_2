using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public float gameSpeed = 1;

    public void GameOver()
    {
        
    }
    void Start()
    {
        
    }



    void Update()
    {
        
    }
}
