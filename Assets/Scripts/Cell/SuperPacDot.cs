using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPacDot : MonoBehaviour
{
    public int point = 50;
    public Animator animator;

    private void Start()
    {
        animator.enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            animator.enabled = false;
        } 
    }
}
