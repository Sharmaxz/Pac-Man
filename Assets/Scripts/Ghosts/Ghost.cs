using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    public string mode = "scatter";
    public GameObject current;
    public string direction = "up";
    
    [Range(0, 100)]
    public int speed = 2;
    
    public new Rigidbody2D rigidbody;
    public Animator animator;

    [Header("Targets")]
    public GameObject pacman;
    public GameObject mrsPacman;
    public GameObject homeTarget;
    public GameObject scatterTarget;

    [Header("Debug")]
    public GhostRoute ghostRoute;
    private Vector3? _previousTarget;
    private string _previousDirection;
    
    void Update()
    {
        switch (mode)
        {
            case "scatter":
                Scatter();
                break;
            case "chase":
                Chase();
                break;
        }
    }

    private void Chase()
    {
        
        
        var path = AStar.Search(current, AvoidDirection(direction), pacman.GetComponent<Player>().current);
        ghostRoute.SetPath(path);
        if (path != null)
        {
            Move(path);
        }
    }

    private void Scatter()
    {
        var path = AStar.Search(current, AvoidDirection(direction), scatterTarget);
        ghostRoute.SetPath(path);
        if (path != null)
        {
            Move(path);
        }
    }
    
    protected abstract void Frightened();

    protected void MoveCallback(List<GameObject> path)
    {
        ghostRoute.SetPath(path);
        if (path != null)
        {
            Move(path);
        }
    }

    protected void Move(List<GameObject> path)
    {
        // if (path.Count == 1) return;
        
        var position = transform.position;
        var targetPosition = path[path.Count-2].transform.position;
        if (_previousTarget != null)
        {
            targetPosition = (Vector3) _previousTarget;
        }
        else
        {
            _previousTarget = targetPosition;
            
            if (Math.Abs(Math.Abs(position.x) - Math.Abs(targetPosition.x)) > Math.Abs( Math.Abs(position.y) - Math.Abs(targetPosition.y)))
            {
                if (position.x > targetPosition.x)
                {
                    ChangeDirection("left");
                }
                else if (position.x < targetPosition.x)
                {
                    ChangeDirection("right");
                }
            }
            else
            {
                if (position.y > targetPosition.y)
                {
                    ChangeDirection("down");
                }
                else if (position.y < targetPosition.y)
                {
                    ChangeDirection("up");
                }
            }
        }
        
        switch (direction)
        {
            case "up" when position.y < targetPosition.y:
                rigidbody.velocity = new Vector2(0, speed);
                ChangeDirection("up");
                break;
            case "up":
                rigidbody.velocity = Vector2.zero;
                transform.position = targetPosition;
                break;
        }
        
        switch (direction)
        {
            case "right" when position.x < targetPosition.x:
                rigidbody.velocity = new Vector2(speed, 0);
                ChangeDirection("right");
                break;
            case "right" :
                rigidbody.velocity = Vector2.zero;
                transform.position = targetPosition;
                break;
        }
        
        switch (direction)
        {
            case "down" when position.y > targetPosition.y:
                rigidbody.velocity = new Vector2(0, -speed);
                ChangeDirection("down");
                break;
            case "down":
                rigidbody.velocity = Vector2.zero;
                transform.position = targetPosition;
                break;
        }
        
        switch (direction)
        {
            case "left" when position.x > targetPosition.x:
                rigidbody.velocity = new Vector2(-speed, 0);
                ChangeDirection("left");
                break;
            case "left":
                rigidbody.velocity = Vector2.zero;
                transform.position = targetPosition;
                break;
        }

        if (transform.position == targetPosition)
        {
            _previousTarget = null;
        }
    }

    private void ChangeDirection(string actualDirection)
    {
        ResetAnimator();
        switch (actualDirection)
        {
            case "up":
                animator.SetBool("up", true);
                break;               
            case "right":
                animator.SetBool("right", true);
                break;                
            case "down":
                animator.SetBool("down", true);
                break;                
            case "left":
                animator.SetBool("left", true);
                break;
        }
        _previousDirection = direction;
        direction = actualDirection;
    }

    protected GameObject AvoidDirection(string direction)
    {
        var cell = current.GetComponent<Cell>();
        var result = direction switch
        {
            "up" => cell.down,
            "right" => cell.left,
            "down" => cell.up,
            "left" => cell.right,
            _ => null
        };

        return result;
    }
    

    private void ResetAnimator()
    {
        animator.SetBool("up", false);
        animator.SetBool("right", false);
        animator.SetBool("down", false);
        animator.SetBool("left", false);
    } 
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Cell")
        {
            current = collision.gameObject;
        } 
    }
}
