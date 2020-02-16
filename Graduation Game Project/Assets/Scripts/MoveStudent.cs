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
    	RotateIntoMoveDirection();
        
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
		  	}
		  	else
		  	{
		    	Destroy(gameObject);

		    	AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		    	AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
		    	
		  	}
		}
    }

	public GameObject[] GetPath()
	{
		return Waypoints;
	}

	private void RotateIntoMoveDirection()
	{
	  // set the positions of the next way points
	  Vector3 newStartPosition = Waypoints[StartElement].transform.position;
	  Vector3 newEndPosition = Waypoints[StartElement + 1].transform.position;
	  Vector3 newDirection = (newEndPosition - newStartPosition);

	  // set the position to turn the student
	  float x = newDirection.x;
	  float y = newDirection.y;
	  float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
	  
	  // turn the student (add 90 to account for bottom facing start position)
	  GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
	  sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle + 90, Vector3.forward);
	}

}
