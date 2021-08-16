using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacDot : MonoBehaviour
{
    public int point = 10;
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;

    private void Start()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            spriteRenderer.enabled = false;
        } 
    }
}
