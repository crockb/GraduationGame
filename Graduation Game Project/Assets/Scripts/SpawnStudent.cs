using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStudent : MonoBehaviour
{
    public GameObject[] Waypoints;
	public GameObject ObjectToSpawn;
    public int NumberObjectsToSpawn;
    public float SpawnFrequencySeconds;
    public Transform ParentCanvas;
    
    private int objectsSpawned = 1; // One is already created
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(ObjectToSpawn).GetComponent<MoveStudent>().Waypoints = Waypoints;
        // This is the time that the next object will be spawned
        nextSpawnTime = Time.time + SpawnFrequencySeconds;
    }

    // Update is called once per frame
    void Update()
    {
        // Check to make sure enough time has passed and that we are under the object spawn limit
        if (Time.time > nextSpawnTime && objectsSpawned < NumberObjectsToSpawn)
        {
            var clone = Instantiate(ObjectToSpawn);
            clone.transform.SetParent(ParentCanvas); // Doing this because of layering issues
            clone.GetComponent<MoveStudent>().Waypoints = Waypoints;
            objectsSpawned++;
            nextSpawnTime += SpawnFrequencySeconds;
        }
    }
}
