using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkAnim : MonoBehaviour
{
    public Image image;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        image = image.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color = image.color;

        if (time < 0.75f) 
        {
            //tempColor = new Color(1, 1, 1, 1 - time);
            color.a = time;
            image.color = color;
        }
        else
        {
           //tempColor = new Color(1, 1, 1, time);
            color.a = time;
            image.color = color;
            if(time > 1.5f)
            {
                time = 0f;
            }
        }

        time += Time.deltaTime;
    }
}
