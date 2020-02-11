using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStudent : MonoBehaviour
{
	public GameObject[] Waypoints;
	public GameObject testStudentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(testStudentPrefab).GetComponent<MoveStudent>().Waypoints = Waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
