using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Wave
{
  public GameObject studentPrefab;
  public float spawnInterval = 2;
  public int maxStudents = 20;
  public GameObject[] waypoints;
}

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
		// 1
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length)
		{
		  // 2
		  float timeInterval = Time.time - lastSpawnTime;
		  float spawnInterval = waves[currentWave].spawnInterval;
		  if (((studentsSpawned == 0 && timeInterval > timeBetweenWaves) ||
		       timeInterval > spawnInterval) && 
		      studentsSpawned < waves[currentWave].maxStudents)
		  {
		    // 3  
		    lastSpawnTime = Time.time;
		    GameObject newStudent = (GameObject)
		        Instantiate(waves[currentWave].studentPrefab);

    		newStudent.GetComponent<MoveStudent>().Waypoints = waves[currentWave].waypoints;
		    studentsSpawned++;
		  }
		  // 4 
		  if (studentsSpawned == waves[currentWave].maxStudents &&
		      GameObject.FindGameObjectWithTag("Student") == null)
		  {
		    gameManager.Wave++;
		    //gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
		    studentsSpawned = 0;
		    lastSpawnTime = Time.time;

		  }
		  // 5 
		}
		else
		{
		  //gameManager.gameOver = true;
		  //GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
		  //gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
		}

    }
}