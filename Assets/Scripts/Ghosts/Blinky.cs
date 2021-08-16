using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : Ghost
{
    private void Awake()
    {
        direction = "right";
    }

    
    protected override void Scatter() {}
    
    protected override void Hunt()
    {
        var result = Search(current);
        ghostRoute.Set(result);
        
        // Debug.Log($"Blinky - {endTime - startTime}s");
    }
    
    public static List<GameObject> Search(GameObject current)
    {
        current.GetComponent<Cell>().distance = 0;
        var opened = new List<GameObject>();
        var closed = new List<GameObject>();
        
        opened.Add(current);
        closed.Add(current.GetComponent<Cell>().right);
        
        while (opened.Count > 0)
        {
            var node = opened[0];
            var index = 0;

            foreach (var go in opened)
            {
                if (go.GetComponent<Cell>().distance < node.GetComponent<Cell>().distance)
                {
                    node = go;
                    index = opened.FindIndex(x => x == go);
                }
            }

            opened.RemoveAt(index);
            closed.Add(node);
            
            if (node.GetComponent<Cell>().target)
            {
                var path = new List<GameObject>();
                var test = node;
                while (test != null && path.Count < 300)
                {
                    path.Add(test);
                    test = test.GetComponent<Cell>().parent;
                }
                return path;
            }

            var children = new List<GameObject>();

            if (node.GetComponent<Cell>().CheckPath()[0])
            {
                children.Add(node.GetComponent<Cell>().up);
            }

            if (node.GetComponent<Cell>().CheckPath()[1])
            {
                children.Add(node.GetComponent<Cell>().right);
            }

            if (node.GetComponent<Cell>().CheckPath()[2])
            {               
                children.Add(node.GetComponent<Cell>().down);
            }

            if (node.GetComponent<Cell>().CheckPath()[3])
            {
                children.Add(node.GetComponent<Cell>().left);
            }

            foreach (var child in children)
            {
                if (closed.Contains(child))
                {
                    continue;
                }

                var distance = node.GetComponent<Cell>().distance + 1;
                child.GetComponent<Cell>().distance = distance;
                child.GetComponent<Cell>().parent = node;
                
                if (opened.Contains(child) &&  child.GetComponent<Cell>().distance > opened.Find(open_node => open_node == child).GetComponent<Cell>().distance)
                {
                    continue;
                }

                opened.Add(child);
            }
            
            if (opened.Count > 1000)
            {
                break;
            }
        }
        
        return null;
    }
    
}
