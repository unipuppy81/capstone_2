using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveRight : MonoBehaviour
{
    private static TouchMoveRight _instance; // 다른 코드에서 불러오기 쉽도록 만든 
    public static TouchMoveRight instance    // 싱글톤 패턴
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TouchMoveRight>();
            }
            return _instance;
        }
    }


    public bool isClickRight;
    // Start is called before the first frame update
    void Start()
    {
        isClickRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        isClickRight = true;

    }

    void OnMouseUp()
    {
        isClickRight = false;
    }
}
