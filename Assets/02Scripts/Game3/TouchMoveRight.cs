using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveRight : MonoBehaviour
{
    private static TouchMoveRight _instance; // �ٸ� �ڵ忡�� �ҷ����� ������ ���� 
    public static TouchMoveRight instance    // �̱��� ����
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
