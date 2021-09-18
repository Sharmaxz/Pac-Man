using System;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool target = false;
    public GameObject parent;
    public int distance = 0;
    public GameObject up;
    public GameObject right;
    public GameObject down;
    public GameObject left;

    private void Update()
    {
        distance = 0;
        parent = null;
    }

    public List<bool> AvailablePath()
    {
        var path = new List<bool> {true, true, true, true};

        if (up == null)
        {
            path[0] = false;
        }
        if (right == null)
        {
            path[1] = false;
        }
        if (down == null)
        {
            path[2] = false;
        }
        if (left == null)
        {
            path[3] = false;
        }

        return path;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            target = true;
        } 
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Pacman")
        {
            target = false;
        } 
    }
    
}
