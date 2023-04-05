using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] SpriteRenderer scroll;
    [SerializeField] float ScrollSpeed;
    Vector2 newOffset;

    void Start()
    {
        scroll = gameObject.GetComponent<SpriteRenderer>();
        Vector2 newOffset = scroll.material.mainTextureOffset;
    }

    void Update()
    {
        Vector2 newOffset = scroll.material.mainTextureOffset;
        newOffset.Set(newOffset.x + (ScrollSpeed * Time.deltaTime), 0 );
        scroll.material.mainTextureOffset = newOffset;
    }
}
