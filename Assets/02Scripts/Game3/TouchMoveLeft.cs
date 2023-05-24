using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveLeft : MonoBehaviour
{
    private static TouchMoveLeft _instance; // �ٸ� �ڵ忡�� �ҷ����� ������ ���� 
    public static TouchMoveLeft instance    // �̱��� ����
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
        if (Input.GetMouseButton(0))
        {
            isClickLeft = true;
            Debug.Log("���� Ŭ��");
        }
    }
}
