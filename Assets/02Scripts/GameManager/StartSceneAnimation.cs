using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneAnimation : MonoBehaviour
{
    public Animator animator;
    public GameObject LoginPanel;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        if(LoginPanel.activeSelf == false)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }
}
