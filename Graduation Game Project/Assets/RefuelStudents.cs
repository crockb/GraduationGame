using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefuelStudents : MonoBehaviour
{

    public List<GameObject> StudentsInRange;

    private float lastRefuelTime;
    private StarbucksData StarbucksData;
    private LibraryData LibraryData;
    private ExamRoomData ExamRoomData;
    private string towerData;
    private int index;
    private GameObject target;


    private float lifetime = 2.0f;


    // Use this for initialization
    void Start()
    {
        StudentsInRange = new List<GameObject>();
        lastRefuelTime = Time.time;

        //StarbucksData = gameObject.GetComponentInChildren<StarbucksData>();
        findTowerData();

        //Debug.Log();

        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject target = null;
        target = null;

        // Starbucks Tower:
        // Will refuel student closest to destination until full and then will remove target from list when at full health
        if (StarbucksData != null)
        {
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
                //Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =target.GetComponent<HealthBar>();

                if(healthBar.currentHealth < 100)
                {
                    if (Time.time - lastRefuelTime > StarbucksData.CurrentLevel.fireRate)
                    {

                        Refuel(target.GetComponent<Collider2D>());
                        lastRefuelTime = Time.time;
                    }
                }

                //Remove student from list when health is full
                else
                    StudentsInRange.Remove(target);

            }
        }

        // Library Tower:
        // Will refuel student with the lowest health first and will remove from list when they reach full health
        else if (LibraryData != null)
        {
            // 1
            float minimalStudentHealth = float.MaxValue;
            foreach (GameObject Student in StudentsInRange)
            {
                float lowestHealth = Student.GetComponent<HealthBar>().currentHealth;
                if (lowestHealth < minimalStudentHealth)
                {
                    target = Student;
                    minimalStudentHealth = lowestHealth;
                }
            }
            // 2

            if (target != null )
            {
                //Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar = target.GetComponent<HealthBar>();

                if(healthBar.currentHealth < 100)
                {
                    if (Time.time - lastRefuelTime > LibraryData.CurrentLevel.fireRate)
                    {

                        Refuel(target.GetComponent<Collider2D>());
                        lastRefuelTime = Time.time;
                    }
                }

                //Remove student from list when health is full
                else 
                {
                    StudentsInRange.Remove(target);
                }
            }
        }

        // Exam Room Tower:
        // Will refuel student ...
        else if (ExamRoomData != null)
        {
            // 1
            float minimalStudentHealth = float.MaxValue;
            foreach (GameObject Student in StudentsInRange)
            {
                float lowestHealth = Student.GetComponent<HealthBar>().currentHealth;
                if (lowestHealth < minimalStudentHealth)
                {
                    target = Student;
                    minimalStudentHealth = lowestHealth;
                }
            }
            // 2

            if (target != null )
            {
                //Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar = target.GetComponent<HealthBar>();

                if(healthBar.currentHealth < 100)
                {
                    if (Time.time - lastRefuelTime > ExamRoomData.CurrentLevel.fireRate)
                    {

                        Refuel(target.GetComponent<Collider2D>());
                        lastRefuelTime = Time.time;
                    }
                }

                //Remove student from list when health is full
                else 
                {
                    StudentsInRange.Remove(target);
                }
	        }

	        
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
        if (StarbucksData != null)
        {

/*  ORIGINAL WORKING CODE - BEFORE 2/28/20 UPDATES TO LEAD STUDENTS

            GameObject bulletPrefab = StarbucksData.CurrentLevel.bullet;

            // set the positions of the tower and student 
            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;
            startPosition.z = bulletPrefab.transform.position.z;
            targetPosition.z = bulletPrefab.transform.position.z;

            // instatiate a bullet and set the targets original
            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = targetPosition;
*/

//  NEW CODE - TARGET WHERE THE STUDENTS ARE GOING
            GameObject bulletPrefab = StarbucksData.CurrentLevel.bullet;

            // instatiate a bullet and set the targets original
            Vector3 startPosition = gameObject.transform.position;

            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = AcquireTargetPosition(target.gameObject, newBullet);
            //Debug.Log("Actual Target - x:" + bulletComp.targetPosition.x + " y:" + bulletComp.targetPosition.y);
            
            // fire the bullet
            Animator animator =
                StarbucksData.CurrentLevel.visualization.GetComponent<Animator>();
            animator.SetTrigger("fireRefuel");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneRefuel(audioSource.clip);
        }

        if (LibraryData != null)
        {

/*  ORIGINAL WORKING CODE - BEFORE 2/28/20 UPDATES TO LEAD STUDENTS

            GameObject bulletPrefab = LibraryData.CurrentLevel.bullet;

            // set the positions of the tower and student 
            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;
            startPosition.z = bulletPrefab.transform.position.z;
            targetPosition.z = bulletPrefab.transform.position.z;

            // instatiate a bullet and set the targets
            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = targetPosition;

            // fire the bullet
            Animator animator =
                LibraryData.CurrentLevel.visualization.GetComponent<Animator>();
            animator.SetTrigger("fireRefuel");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneRefuel(audioSource.clip);
*/
//  NEW CODE - TARGET WHERE THE STUDENTS ARE GOING
            GameObject bulletPrefab = LibraryData.CurrentLevel.bullet;

            // instatiate a bullet and set the targets original
            Vector3 startPosition = gameObject.transform.position;

            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = AcquireTargetPosition(target.gameObject, newBullet);
            //Debug.Log("Actual Target - x:" + bulletComp.targetPosition.x + " y:" + bulletComp.targetPosition.y);
            
            // fire the bullet
            Animator animator =
                LibraryData.CurrentLevel.visualization.GetComponent<Animator>();
            animator.SetTrigger("fireRefuel");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneRefuel(audioSource.clip);

        }

        if (ExamRoomData != null)
        {

/*  ORIGINAL WORKING CODE - BEFORE 2/28/20 UPDATES TO LEAD STUDENTS
            GameObject bulletPrefab = ExamRoomData.CurrentLevel.bullet;

            // set the positions of the tower and student  
            Vector3 startPosition = gameObject.transform.position;
            Vector3 targetPosition = target.transform.position;
            startPosition.z = bulletPrefab.transform.position.z;
            targetPosition.z = bulletPrefab.transform.position.z;

            // instatiate a bullet and set the targets original
            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = targetPosition;

             // fire the bullet
            Animator animator =
                ExamRoomData.CurrentLevel.visualization.GetComponent<Animator>();
            animator.SetTrigger("fireRefuel");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneRefuel(audioSource.clip);
*/
//  NEW CODE - TARGET WHERE THE STUDENTS ARE GOING
            GameObject bulletPrefab = ExamRoomData.CurrentLevel.bullet;

            // instatiate a bullet and set the targets original
            Vector3 startPosition = gameObject.transform.position;

            GameObject newBullet = (GameObject) Instantiate(bulletPrefab);
            newBullet.transform.position = startPosition;
            BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
            bulletComp.target = target.gameObject;
            bulletComp.startPosition = startPosition;
            bulletComp.targetPosition = AcquireTargetPosition(target.gameObject, newBullet);
            //Debug.Log("Actual Target - x:" + bulletComp.targetPosition.x + " y:" + bulletComp.targetPosition.y);
            
            // 3 
            Animator animator =
                ExamRoomData.CurrentLevel.visualization.GetComponent<Animator>();
            animator.SetTrigger("fireRefuel");
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneRefuel(audioSource.clip);

        }
    }

    public void findTowerData()
    {
        if (gameObject.GetComponentInChildren<StarbucksData>()){
            StarbucksData = gameObject.GetComponentInChildren<StarbucksData>();
        }
        else if (gameObject.GetComponentInChildren<LibraryData>()){
            LibraryData = gameObject.GetComponentInChildren<LibraryData>();
        }
        else if (gameObject.GetComponentInChildren<ExamRoomData>()){
            ExamRoomData = gameObject.GetComponentInChildren<ExamRoomData>();
        }
    }

    // identify where the student and bullet will meet based on current path
    // algorithm reference:  https://gamedevelopment.tutsplus.com/tutorials/unity-solution-for-hitting-moving-targets--cms-29633

    Vector3 AcquireTargetPosition(GameObject targetStudent, GameObject theBullet)
    {

        Vector3 targetPosition = gameObject.transform.position;  // default
        Vector2 targetVelocity=targetStudent.GetComponent<MoveStudent>().velocity;
        float missileSpeed = theBullet.GetComponent<BulletBehavior>().speed;
        GameObject turret = this.gameObject;

        float a=(targetVelocity.x*targetVelocity.x)+(targetVelocity.y*targetVelocity.y)-(missileSpeed*missileSpeed);
        float b=2*(targetVelocity.x*(targetStudent.gameObject.transform.position.x-turret.transform.position.x) 
        +targetVelocity.y*(targetStudent.gameObject.transform.position.y-turret.transform.position.y));
        float c= ((targetStudent.gameObject.transform.position.x-turret.transform.position.x)*(targetStudent.gameObject.transform.position.x-turret.transform.position.x))+
        ((targetStudent.gameObject.transform.position.y-turret.transform.position.y)*(targetStudent.gameObject.transform.position.y-turret.transform.position.y));
 
        float disc= b*b -(4*a*c);
        if(disc<0){
            Debug.LogError("No possible hit!");
            return targetPosition;
        }else{
        float t1=(-1*b+Mathf.Sqrt(disc))/(2*a);
        float t2=(-1*b-Mathf.Sqrt(disc))/(2*a);
        float t= Mathf.Max(t1,t2);// let us take the larger time value 
        targetPosition.x =(targetVelocity.x*t)+targetStudent.gameObject.transform.position.x;
        targetPosition.y =targetStudent.gameObject.transform.position.y+(targetVelocity.y*t);
        
        return targetPosition;
        Debug.Log("Target = x:" + targetPosition.x + " y:" + targetPosition.y);
        }
    }

}
