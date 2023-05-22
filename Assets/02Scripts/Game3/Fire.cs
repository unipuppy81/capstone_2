using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Destroy(this.gameObject, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager3.instance.stopTrigger) Destroy(gameObject);
        animator.SetTrigger("Idle1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager3.instance.GameOver();
            Destroy(this.gameObject);
            //animator.SetTrigger("poop");
        }
    }
}
