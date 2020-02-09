using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Path : MonoBehaviour
{
    public int CurrentElement = 0;
    public Transform[] ThisPath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (ThisPath == null || ThisPath.Length < 1)
        {
            yield break;
        }

        while(true)
        {
            // Return the current point in the path and waits for next call of enumerator
            // This prevents an infinite loop
            yield return ThisPath[CurrentElement];
            // If there is only one point then skip this iteration
            if (ThisPath.Length == 1)
            {
                continue;
            }
            CurrentElement++;
        }
    }
}