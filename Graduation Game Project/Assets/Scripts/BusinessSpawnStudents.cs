using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

[System.Serializable]
public class Wave
{
  public GameObject studentPrefab;
  public float spawnInterval = 2;
  public int maxStudents = 20;
}
*/

public class BusinessSpawnStudents : MonoBehaviour
{
/*
	public Wave[] waves;
	public int timeBetweenWaves = 5;

	private InGameMenuManagerBehavior gameManager;

	private float lastSpawnTime;
	private int studentsSpawned;
*/

	public GameObject[] waypoints;
	public GameObject testStudentPrefab;
	private int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    	/*

    	lastSpawnTime = Time.time;
    	gameManager = GameObject.Find("GameManager").GetComponent<InGameMenuManagerBehavior>();
		
		*/

    	testStudentPrefab = GameObject.Find("UnderGrad(DEV)");
    	waypoints = new GameObject[12];
    	foreach(Transform child in transform)
    	{
    		waypoints[i] = GameObject.Find(child.name);
    		i++;
    	}

        Instantiate(testStudentPrefab).GetComponent<BusinessMoveStudents>().waypoints = waypoints;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
