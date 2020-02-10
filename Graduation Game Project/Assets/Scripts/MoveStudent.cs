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

    		    	RotateIntoMoveDirection();
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

	private void RotateIntoMoveDirection()
    {
      //1
      Vector3 newStartPosition = Waypoints[StartElement].transform.position;
      Vector3 newEndPosition = Waypoints[StartElement + 1].transform.position;
      Vector3 newDirection = (newEndPosition - newStartPosition);
      //2
      float x = newDirection.x;
      float y = newDirection.y;
      float rotationAngle = Mathf.Atan2 (y, x)  * Mathf.Rad2Deg;//180 / Mathf.PI;
      //3
      GameObject sprite = gameObject.transform.Find("Sprite").gameObject;
      sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    } 

    public float DistanceToGoal()
    {
        float distance = 0;
        distance += Vector2.Distance(
            gameObject.transform.position,
            Waypoints[StartElement + 1].transform.position);
        for (int i = StartElement + 1; i < Waypoints.Length - 1; i++)
        {
            Vector3 startPosition = Waypoints[i].transform.position;
            Vector3 endPosition = Waypoints[i + 1].transform.position;
            distance += Vector2.Distance(startPosition, endPosition);
        }
        return distance;
    }

}
