using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GhostRoute : MonoBehaviour
{
    public Color initialColor;
    public Color color;
    public LineRenderer line;
    
    public float timeLeft;
    Color targetColor;
    private bool turn = false;
    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {
            if (turn)
            {
                color = targetColor;
                targetColor = new Color(initialColor.r, initialColor.b, initialColor.g, initialColor.a);
                turn = false;
            }
            else
            {
                color = initialColor;
                targetColor = new Color(initialColor.r, initialColor.b, initialColor.g, initialColor.a + (10f / 255f));
                turn = true;
            }
            timeLeft = 0.4f;
        }
        else
        {
            color = Color.Lerp(color, targetColor, Time.deltaTime / timeLeft);
            line.startColor = color;
            line.endColor = color;
            timeLeft -= Time.deltaTime;
        }
        
    }

    void OnValidate()
    {
        line.startColor= color;
        line.endColor = color;
    }


    public void Set(List<GameObject> path)
    {
        if (path != null)
        {
            var temp = new List<Vector3>();
            var walkX = false;

            Transform previous = path[0].transform;
            temp.Add(previous.position);
            foreach (var step in path)
            {
                var position = step.transform.position;
                var previousPosition = previous.position; 

                if (walkX)
                {
                    if (position.x == previous.position.x && position.y != previous.position.y)
                    {
                        walkX = false;
                        temp.Add(previousPosition);
                    }
                }
                else
                {
                    if (position.x != previous.position.x && position.y == previous.position.y)
                    {
                        walkX = true;
                        temp.Add(previousPosition);
                    }
                }

                previous = step.transform;
            }

            if (temp[0] == temp[1])
            {
                temp.RemoveAt(0);
            }
            
            temp.Add(path[path.Count - 1].transform.position);

            var positions = new Vector3[temp.Count];
            var index = 0;
            foreach (var item in temp)
            {
                positions.SetValue(item, index);
                index++;
            }

            line.positionCount = positions.Length;
            line.widthMultiplier = 1f;
            line.SetPositions(positions);
        }
    }
}
