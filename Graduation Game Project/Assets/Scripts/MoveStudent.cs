using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStudent : MonoBehaviour
{
	public int StartElement;
	public GameObject[] Waypoints;
	public float Speed;
	private float lastWaypointSwitchTime;
	
    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPosition = Waypoints[StartElement].transform.position;
		Vector3 endPosition = Waypoints[StartElement + 1].transform.position;

		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / Speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);

		if (gameObject.transform.position.Equals(endPosition)) 
		{
			if (StartElement < Waypoints.Length - 2)
			{
		    	StartElement++;
		    	lastWaypointSwitchTime = Time.time;
		    	// TODO: Rotate into move direction
		  	}
		  	else
		  	{
		    	Destroy(gameObject);

		    	AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		    	AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
		    	// TODO: deduct health
		  	}
		}
    }

	public GameObject[] GetPath()
	{
		return Waypoints;
	}
}
