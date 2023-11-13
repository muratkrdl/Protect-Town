using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour
{
    List<Transform> path  = new List<Transform>();

    public List<Transform> GetPath
    {
        get
        {
            return path;
        }
    }

    void Awake() 
    {
        FindPath();    
    }

    void FindPath()
    {
        path.Clear();

        foreach(Transform child in transform)
        {
            path.Add(child);
        }
    }

}
