using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool multiplayer;
    public GameObject pacman; 
    public GameObject msPacman; 

    void Start()
    {
        Create();
    }

    void Create()
    {
        CreatePacman();
        if (multiplayer)
        {
            CreateMsPacman();
        }
    }

    private void CreatePacman()
    {
        var go = Instantiate(pacman);
        go.name = "Pacman";
        go.transform.position = transform.position;
    }

    private void CreateMsPacman()
    {
        var go = Instantiate(msPacman);
        go.name = "Ms Pacman";
        go.transform.position = transform.position;
    }
}
