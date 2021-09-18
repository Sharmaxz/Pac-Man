using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private List<List<GameObject>> maze = new List<List<GameObject>>();
    public int height;
    public int width;

    public Tilemap tilemap;
    
    private void Start()
    {
        Create();
    }

    public void Create()
    {
        for (var x = 0; x < width; x++)
        {
            var col = new List<GameObject>();
            for (var y = 0; y < height; y++)
            {
                var position = transform.position;
                
                var cell = Resources.Load("Cell") as GameObject;
                cell = Instantiate(cell, this.transform, true);
                cell.transform.position = new Vector3(position.x + x * 1.6f, position.y - y * 1.6f, 0);

                if (col.Count > 0 && col[y - 1])
                {
                    cell.GetComponent<Cell>().up = col[y - 1];
                    col[y - 1].GetComponent<Cell>().down = cell;
                }
                
                if (maze.Count > 0 && maze[x -1][y])
                {
                    cell.GetComponent<Cell>().left = maze[x -1][y];
                    maze[x -1][y].GetComponent<Cell>().right = cell;
                }
                
                col.Add(cell.gameObject);
            }
            maze.Add(col);
        }
    }

    
}
