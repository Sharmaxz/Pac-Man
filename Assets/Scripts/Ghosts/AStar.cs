using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AStar : MonoBehaviour
{
    public static List<GameObject> Search(GameObject current, GameObject avoidDirection=null, GameObject target=null)
    {
        var currentCell = current.GetComponent<Cell>();
        currentCell.distance = 0;
        
        var opened = new List<GameObject>();
        var closed = new List<GameObject>();
        
        opened.Add(current);
        if (avoidDirection != null)
        {
            closed.Add(avoidDirection); //It's to where the ghost is looking 
        } 
        
        if (current == target)
        {
            target = avoidDirection;
        }

        while (opened.Count > 0)
        {
            var node = opened[0];
            var nodeCell = node.GetComponent<Cell>();
            var index = 0;

            foreach (var go in opened)
            {
                if (go.GetComponent<Cell>().distance < nodeCell.distance)
                {
                    node = go;
                    index = opened.FindIndex(x => x == go);
                }
            }

            opened.RemoveAt(index);
            closed.Add(node);

            //Check if node is target
            if (node == target)
            {
                return GeneratePath(node);
            }

            var children = CheckAvailablePath(node, nodeCell);
            foreach (var child in children)
            {
                if (closed.Contains(child))
                {
                    continue;
                }

                var distance = nodeCell.distance + 1;
                var childCell = child.GetComponent<Cell>();
                childCell.distance = distance;
                childCell.parent = node;

                
                if (opened.Contains(child))
                {
                    var openDistance = opened.Find(open => open == child).GetComponent<Cell>().distance;
                    if (childCell.distance > openDistance)
                    {
                        continue;
                    }
                }

                opened.Add(child);
            }
            
            if (avoidDirection == target)
            {
                closed.RemoveAt(0);
            }
        
            // Limiting iterations
            if (opened.Count > 500)
            {
                break;
            }
        }
        return null;
    }

    private static List<GameObject> CheckAvailablePath(GameObject node, Cell nodeCell)
    {
        var children = new List<GameObject>();
        var availablePath = nodeCell.AvailablePath();
            
        if (node.TryGetComponent(out TurnRule rule))
        {
            availablePath = rule.Check(availablePath);
        }
        
        if (availablePath[0])
        {
            children.Add(nodeCell.up);
        }

        if (availablePath[1])
        {
            children.Add(nodeCell.right);
        }
        
        if (availablePath[2])
        {               
            children.Add(nodeCell.down);
        }
        
        if (availablePath[3])
        {
            children.Add(nodeCell.left);
        }

        return children;
    }

    private static List<GameObject> GeneratePath (GameObject node)
    {
        var path = new List<GameObject>();
        var parent = node;
        while (parent != null && path.Count < 300)
        {
            path.Add(parent);
            parent = parent.GetComponent<Cell>().parent;
        }
        return path;
    }
    
}
