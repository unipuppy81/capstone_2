using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("BombT");
    }


    IEnumerator BombT()
    {
        Debug.Log("ÆøÅº");
        
        // anim.stand

        yield return new WaitForSeconds(3.0f);


        Destroy(gameObject);

        // instantiate , anim.waterline


    }
}
