using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnStudents : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject studentPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(studentPrefab).GetComponent<MoveStudents>().waypoints = waypoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
