using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public int start = 0;
    public Transform[] path;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onDrawGizmos() 
    {
        // Checks to see if a path exists
        if (path == null || path.Length < 2)
        {
            return;
        }
        // Loop through all the points in the path
        for (var i = 1; i < path.Length; i++)
        {
            // This actually draws the line between points
            Gizmos.DrawLine(path[i - 1].position, path[i].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (path == null || path.Length < 1)
        {
            yield break;
        }

        while(true)
        {
            // Return the current point in the path and waits for next call of enumerator
            // This prevents an infinite loop
            yield return path[start];
            // If there is only one point then skip this iteration
            if (path.Length == 1)
            {
                continue;
            }
            start++;
        }
    }
}
