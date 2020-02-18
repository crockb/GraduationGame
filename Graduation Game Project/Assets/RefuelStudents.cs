using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefuelStudents : MonoBehaviour
{

    public List<GameObject> StudentsInRange;

    private float lastRefuelTime;
    private StarbucksData StarbucksData;



    // Use this for initialization
    void Start()
    {
        StudentsInRange = new List<GameObject>();
        lastRefuelTime = Time.time;
        StarbucksData = gameObject.GetComponentInChildren<StarbucksData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
        // 1
        float minimalStudentDistance = float.MaxValue;
        foreach (GameObject Student in StudentsInRange)
        {
            float distanceToGoal = Student.GetComponent<MoveStudent>().DistanceToGoal();
            if (distanceToGoal < minimalStudentDistance)
            {
                target = Student;
                minimalStudentDistance = distanceToGoal;
            }
        }
        // 2
        if (target != null )
        {
        	Transform healthBarTransform = target.transform.Find("HealthBar");
        	HealthBar healthBar =
	            healthBarTransform.gameObject.GetComponent<HealthBar>();

	        if(healthBar.currentHealth < 100)
	        {
	            if (Time.time - lastRefuelTime > StarbucksData.CurrentLevel.fireRate)
	            {

	                Refuel(target.GetComponent<Collider2D>());
	                lastRefuelTime = Time.time;
	            }
	        }

	        else
	        	StudentsInRange.Remove(target);
        }
    }

    /*private void OnStudentDestroy(GameObject Student)
    {
        StudentsInRange.Remove(Student);
    }*/

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Student"))
        {
            StudentsInRange.Add(other.gameObject);
            StudentDestructionDelegate del =
                other.gameObject.GetComponent<StudentDestructionDelegate>();
            //del.StudentDelegate += OnStudentDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Student"))
        {
            StudentsInRange.Remove(other.gameObject);
            StudentDestructionDelegate del =
                other.gameObject.GetComponent<StudentDestructionDelegate>();
            //del.StudentDelegate -= OnStudentDestroy;
        }
    }

    private void Refuel(Collider2D target)
    {
        GameObject bulletPrefab = StarbucksData.CurrentLevel.bullet;
        // 1 
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        // 2 
        GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // 3 
        Animator animator =
            StarbucksData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireRefuel");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.PlayOneRefuel(audioSource.clip);
    }



}
