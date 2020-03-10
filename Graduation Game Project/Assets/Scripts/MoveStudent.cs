using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
public class MoveStudent : MonoBehaviour
{
	[HideInInspector]
	public GameObject[] Waypoints;
	public float Speed;
	public Vector3 velocity;
	public Vector3 currentPosition;

	[HideInInspector]
	public int StartElement = 0;
	private float lastWaypointSwitchTime;
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

		/*
		Debug.Log("GO.x:" + gameObject.transform.position.x + " GO.y:" + gameObject.transform.position.x + " GO.z:" + gameObject.transform.position.z);
		Debug.Log("EP.x:" + endPosition.x + " EP.y:" + endPosition.y + " EP.z:" + endPosition.z);
		Debug.Log("GO == EP:" + gameObject.transform.position.Equals(endPosition));
		*/

		if (gameObject.transform.position == endPosition)
		{
			if (StartElement < Waypoints.Length - 2)
			{
				Debug.Log("Equal Positions: Start Element Before: " + StartElement);
		    	StartElement = StartElement + 1;
		    	Debug.Log("Equals Position: Start Element After: " + StartElement);
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
