using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject current;
    public string direction = "";
    [Range(0, 100)]
    public int speed = 50;

    public Rigidbody2D rigidbody;
    public Animator animator;
    
    public List<bool> path;
    public List<Collider2D> colliders;

    private void Update()
    {
        if (path[0] && Input.GetKey(KeyCode.W))
        {
            Move(0, speed);
        }
        else if (path[1] && Input.GetKey(KeyCode.D))
        {
            Move(speed, 0);
        }
        else if (path[2] && Input.GetKey(KeyCode.S))
        {
            Move(0, -speed);
        }
        else if (path[3] &&Input.GetKey(KeyCode.A))
        {
            Move(-speed, 0);
        }
    }

    private void Move(int x, int y)
    {
        CheckDirection(x, y);
        rigidbody.velocity = new Vector2(Time.deltaTime * 100 * x, Time.deltaTime * 100 * y);
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
        if (colliders.Find(item => item.name == "Pacman" ).IsTouching(collision)  && 
            collision.name == "Cell")
        {
            current = collision.gameObject;
            // transform.position = current.transform.position;
            path = current.GetComponent<Cell>().CheckPath();
        } 
    }
}
