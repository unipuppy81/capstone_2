using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMoveLeft : MonoBehaviour
{
    public bool isClickLeft;

    public GameObject player;
    private float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        isClickLeft = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetMouseButtonDown(0))
        {
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(-1.5f, 0, 0), 0.05f);
        }
        if(Input.GetMouseButtonUp(0))
        {
            player.transform.position = new Vector3(0, 0, 0);
        }*/
    }

    void OnMouseDown()
    {
        isClickLeft = true;
        
    }

    void OnMouseUp()
    {
        isClickLeft = false;
    }
}
