using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Path MyPath;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> pointInPath;

    // Start is called before the first frame update
    void Start()
    {
        // Make sure a path exists
        if (MyPath == null)
        {
            Debug.LogError("A movement path doesn't exist!", gameObject);
            return;
        }
        // Gets the next path point
        pointInPath = MyPath.GetNextPathPoint(); // GetNextPathToPoint is in the Path.cs file
        pointInPath.MoveNext(); // Advances to next element in collection
        if (pointInPath.Current == null)
        {
            Debug.LogError("A path must have points to follow!", gameObject);
            return;
        }
        transform.position = pointInPath.Current.position;
        transform.LookAt(transform.position);
    }   

    // Update is called once per frame
    void Update()
    {
        var startElement = MyPath.CurrentElement;
        // Check if there is a path with a point in it
        if (pointInPath == null || pointInPath.Current == null)
        {
            return;
        } 
        transform.position = Vector3.MoveTowards(transform.position,
                                                 pointInPath.Current.position,
                                                 Time.deltaTime * Speed);

        var distanceSquared = (transform.position - pointInPath.Current.position).sqrMagnitude;
        // If the object hasn't reached the end point then move to the next
        if (distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            pointInPath.MoveNext();
        }
        if (MyPath.CurrentElement > startElement)
        {
            //transform.LookAt(MyPath.ThisPath[MyPath.CurrentElement++].transform.position);
            transform.LookAt(transform.position);
        }
    }
}
