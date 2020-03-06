using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStudent : MonoBehaviour
{
	public int StartElement;
	public GameObject[] Waypoints;
	public float Speed;
	private float lastWaypointSwitchTime;
	public Vector3 velocity;
	public Vector3 currentPosition;

	private InGameMenuManagerBehavior gameManager;
	
    // Start is called before the first frame update
    void Start()
    {
        lastWaypointSwitchTime = Time.time;
        gameManager = GameObject.Find("InGameMenuManager").GetComponent<InGameMenuManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
    	//determine rotation and movements
    	RotateIntoMoveDirection();
        
        Vector3 startPosition = Waypoints[StartElement].transform.position;
		Vector3 endPosition = Waypoints[StartElement + 1].transform.position;

		float pathLength = Vector3.Distance (startPosition, endPosition);
		float totalTimeForPath = pathLength / Speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		gameObject.transform.position = Vector2.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
		
		currentPosition = gameObject.transform.position;
		velocity = Speed* ((endPosition-currentPosition).normalized);

		if (gameObject.transform.position.Equals(endPosition)) 
		{
			if (StartElement < Waypoints.Length - 2)
			{
		    	StartElement++;
		    	lastWaypointSwitchTime = Time.time;
		  	}
		  	else
		  	{
		  		//add Money for students reaching desination then remove from game
		  		//all students add $5
		  		gameManager.Money += 5;
				gameManager.Score += (int)gameObject.GetComponent<HealthBar>().currentHealth;
		    	Destroy(gameObject);
		  	}
		}
    }

    //find waypoints to determine path
	public GameObject[] GetPath()
	{
		return Waypoints;
	}

	//determines rotation for students to follow the path
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

	//determine the distance left to the goal
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
