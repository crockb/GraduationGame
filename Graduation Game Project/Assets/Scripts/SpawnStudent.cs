using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
