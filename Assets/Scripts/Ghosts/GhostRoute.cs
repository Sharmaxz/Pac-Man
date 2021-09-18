using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GhostRoute : MonoBehaviour
{
    public bool blink;
    public Color color;
    public LineRenderer line;

    private bool _turn;
    private float _timeLeft;
    private Color _tempColor;
    private Color _targetColor;
    
    void Update()
    {
        if (blink && _timeLeft <= Time.deltaTime)
        {
            if (_turn)
            {
                _tempColor = _targetColor;
                _targetColor = new Color(_tempColor.r, _tempColor.b, _tempColor.g, _tempColor.a);
                _turn = false;
            }
            else
            {
                _tempColor = color;
                _targetColor = new Color(_tempColor.r, _tempColor.b, _tempColor.g, _tempColor.a + (10f / 255f));
                _turn = true;
            }
            _timeLeft = 0.4f;
        }
        else
        {
            _tempColor = Color.Lerp(color, _targetColor, Time.deltaTime / _timeLeft);
            SetLineColor(_tempColor);
            _timeLeft -= Time.deltaTime;
        }
    }

    private void OnValidate()
    {
        line.startColor= color;
        line.endColor = color;
    }
    
    public void SetPath(List<GameObject> path)
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

            if (temp.Count > 1 && temp[0] == temp[1])
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

    public void SetLineColor(Color c)
    {
        line.startColor = c;
        line.endColor = c;
    }
}
