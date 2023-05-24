using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveLeft : MonoBehaviour
{
    private static TouchMoveLeft _instance; // 다른 코드에서 불러오기 쉽도록 만든 
    public static TouchMoveLeft instance    // 싱글톤 패턴
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TouchMoveLeft>();
            }
            return _instance;
        }
    }

    public bool isClickLeft;
    // Start is called before the first frame update
    void Start()
    {
        isClickLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClickLeft = true;
            Debug.Log("왼쪽 클릭");
        }
    }
}
