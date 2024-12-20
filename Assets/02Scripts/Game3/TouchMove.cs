using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private static TouchMove _instance; // 다른 코드에서 불러오기 쉽도록 만든 
    public static TouchMove instance    // 싱글톤 패턴
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TouchMove>();
            }
            return _instance;
        }
    }

    public bool isClick1 = false;
    public bool isClick2 = true;
    public bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isClick2 == true)
        {
            isClick1 = true;
            isClick2 = false;
            isClicked = true;
        }
        else if (Input.GetMouseButtonDown(0) && isClick1 == true)
        {
            isClick1 = false;
            isClick2 = true;
            isClicked = true;
        }
    }
}
