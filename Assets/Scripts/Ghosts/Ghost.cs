using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    public GameObject current;
    public string direction = "";
    [Range(0, 100)]
    public int speed = 50;

    public new Rigidbody2D rigidbody;
    public Animator animator;
    
    [Header("Debug")]
    public GhostRoute ghostRoute;

    void Update()
    {
        Hunt();
    }

    protected abstract void Hunt();
    
    protected abstract void Scatter();
    
    
    private void CheckDirection(float x, float y)
    {
        if (y == 0 && x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            direction = "up";
        }
        else if (x == 0 && y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            direction = "right";
        }
        else if (x == 0 && y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            direction = "down";
        }
        else if (y == 0 && x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            direction = "left";
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Cell")
        {
            current = collision.gameObject;
        } 
    }
}
