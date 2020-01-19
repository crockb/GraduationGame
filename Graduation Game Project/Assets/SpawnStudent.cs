using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStudent : MonoBehaviour
{
	public GameObject[] waypoints;
	public GameObject testStudentPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(testStudentPrefab).GetComponent<MoveStudent>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
