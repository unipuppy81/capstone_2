using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateG1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Sprite idleSprite;
    public Sprite[] animationSprites;

    public float animationTime = 0.25f;
    private int animationFrame = 0;

    public bool loop = true;
    public bool idle = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UnityEngine.Debug.Log("START");
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        UnityEngine.Debug.Log("START2222");
        spriteRenderer.enabled = false;
    }

    private void Start()
    {
        InvokeRepeating("NextFrame", animationTime, animationTime);
    }

    private void NextFrame()
    {
        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }

        if (idle){
            spriteRenderer.sprite = idleSprite;
        }else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
