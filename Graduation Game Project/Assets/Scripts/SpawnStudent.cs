using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class WaveInfo
{
  public GameObject studentPrefab;
  public float spawnInterval = 2;
  public int maxStudents = 20;
}

public class SpawnStudent : MonoBehaviour
{
    public GameObject[] Waypoints;

    public WaveInfo[] waves;
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
        int currentWave = gameManager.WaveInfo;
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

            Instantiate(waves[currentWave].studentPrefab).GetComponent<MoveStudent>().Waypoints = Waypoints;
            studentsSpawned++;

            // 2/16/20 - Bryant added to mitigate blip at initial spawn
            (waves[currentWave].studentPrefab).transform.position = new Vector3(-1000,0,0);
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

/* WORKING SCRIPT BEFORE ADDING WAVES - HOLLY - 2/23/20
public class SpawnStudent : MonoBehaviour
{
    public GameObject[] Waypoints;
	public GameObject ObjectToSpawn;
    public int NumberObjectsToSpawn;
    public float SpawnFrequencySeconds;
    
    private int objectsSpawned = 0; // One is already created
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnStudent();
        // This is the time that the next object will be spawned
        nextSpawnTime = Time.time + SpawnFrequencySeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Check to make sure enough time has passed and that we are under the object spawn limit
        if (Time.time > nextSpawnTime && objectsSpawned < NumberObjectsToSpawn)
        {
            spawnStudent();
            nextSpawnTime += SpawnFrequencySeconds;
        }
    }

    private void spawnStudent()
    {
        Instantiate(ObjectToSpawn).GetComponent<MoveStudent>().Waypoints = Waypoints;
        
        objectsSpawned++;
        
        // 2/16/20 - Bryant added to mitigate blip at initial spawn
        ObjectToSpawn.transform.position = new Vector3(-1000,0,0);
    }
}
*/