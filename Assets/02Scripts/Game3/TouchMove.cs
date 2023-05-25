using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMove : MonoBehaviour
{
    private static TouchMove _instance; // �ٸ� �ڵ忡�� �ҷ����� ������ ���� 
    public static TouchMove instance    // �̱��� ����
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
        }
        else if (Input.GetMouseButtonDown(0) && isClick1 == true)
        {
            isClick1 = false;
            isClick2 = true;
        }
    }
}
