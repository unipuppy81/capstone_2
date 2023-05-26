using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Rigidbody2D poopRigidbody;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        poopRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager3.instance.stopTrigger) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (GameManager3.instance.score1 >= 60)
        {
            poopRigidbody.AddForce(Vector2.down * 15f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager3.instance.Score();
            Destroy(this.gameObject);
            //animator.SetTrigger("poop");
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager3.instance.GameOver();
            AudioManager.soundPlay1();
            Destroy(this.gameObject);
            //animator.SetTrigger("poop");
        }
    }
}
