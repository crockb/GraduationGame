using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
[System.Serializable]
public class Wave
{
  public GameObject studentPrefab;
  public float spawnInterval = 2;
  public int maxStudents = 20;
  public GameObject[] waypoints;
}

[Preserve]
public class BusinessSpawnStudents : MonoBehaviour
{

	public Wave[] waves;
	public int timeBetweenWaves = 5;

	private InGameMenuManagerBehavior gameManager;

	private float lastSpawnTime;
	private int studentsSpawned = 0;

    // Start is called before the first frame update
    void Start()
    {

    	lastSpawnTime = Time.time;
    	gameManager = GameObject.Find("InGameMenuManager").GetComponent<InGameMenuManagerBehavior>();

    }

    // Update is called once per frame
    void Update()
    {
		// Check wave count
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length)
		{
		  // with each wave confirm the ability to spawn a student
		  float timeInterval = Time.time - lastSpawnTime;
		  float spawnInterval = waves[currentWave].spawnInterval;
		  if (((studentsSpawned == 0 && timeInterval > timeBetweenWaves) ||
		       timeInterval > spawnInterval) && 
		      studentsSpawned < waves[currentWave].maxStudents)
		  {
		    // spawn a new student in the wave and set the waypoints
		    lastSpawnTime = Time.time;
		    GameObject newStudent = (GameObject)
		        Instantiate(waves[currentWave].studentPrefab);

		    newStudent.transform.position = new Vector3(-1000,0,0);

    		newStudent.GetComponent<MoveStudent>().Waypoints = waves[currentWave].waypoints;
		    studentsSpawned++;
		  }

		  // students gone from the wave - switch to the next wave
		  if (studentsSpawned == waves[currentWave].maxStudents &&
		      GameObject.FindGameObjectWithTag("Student") == null)
		  {
		    gameManager.Wave++;
		    studentsSpawned = 0;
		    lastSpawnTime = Time.time;
		  }
		}

		// end of the set of waves - switch levels
		else
		{
		  gameManager.TransitionLevels();
		}
    }
}