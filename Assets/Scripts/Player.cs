using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject current;
    public string direction;
    [Range(0, 50)]
    public int speed = 20;

    public Rigidbody2D rigidbody;
    public Animator animator;
    
    public List<bool> path;
    public List<Collider2D> colliders;

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (path[0] && Input.GetKey(KeyCode.W))
        {
            direction = "up";
            Move(0, speed);
        }
        else if (path[1] && Input.GetKey(KeyCode.D))
        {
            direction = "right";
            Move(speed, 0);
        }
        else if (path[2] && Input.GetKey(KeyCode.S))
        {
            direction = "down";
            Move(0, -speed);
        }
        else if (path[3] && Input.GetKey(KeyCode.A))
        {
            direction = "left";
            Move(-speed, 0);
        }
    }

    private void Move(int x, int y)
    {
        CheckDirection(x, y);
        rigidbody.velocity = new Vector2(x, y);
        animator.SetBool("move", true);
    }

    private void CheckDirection(float x, float y)
    {
        if (y == 0 && x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (x == 0 && y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (x == 0 && y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (y == 0 && x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } 
    }

    private void Stop()
    {
        animator.SetBool("move", false);
        rigidbody.velocity = Vector2.zero;
        transform.position = current.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls")
        {
            Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (colliders.Find(item => item.name == "Pacman").IsTouching(collision) && 
            collision.name == "Cell")
        {
            current = collision.gameObject;
            path = current.GetComponent<Cell>().AvailablePath();
        } 
    }
    
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (colliders.Find(item => item.name == "Pacman").IsTouching(collision) && 
    //         collision.name == "Cell")
    //     {
    //         
    //     } 
    // }
}
