using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private Rigidbody2D poopRigidbody;

    public float turnSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        poopRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime);

        if (!GameManager3.instance.stopTrigger) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (GameManager3.instance.score1 >= 30)
        {
            poopRigidbody.AddForce(Vector2.down * 10f);
        }

        if(GameManager3.instance.score1 >= 60)
        {
            poopRigidbody.AddForce(Vector2.down * 15f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GameManager3.instance.Score();
            //animator.SetTrigger("poop");
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager3.instance.GameOver();
            //animator.SetTrigger("poop");
            AudioManager.soundPlay1();
            Destroy(this.gameObject);
        }
    }

}
