using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootStudents : MonoBehaviour
{

    public List<GameObject> StudentsInRange;

    private float lastShotTime;
    private LibraryData LibraryData;

    // Use this for initialization
    void Start()
    {
        StudentsInRange = new List<GameObject>();
        lastShotTime = Time.time;
        LibraryData = gameObject.GetComponentInChildren<LibraryData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
        // 1
        float minimalStudentDistance = float.MaxValue;
        foreach (GameObject student in StudentsInRange)
        {
            float distanceToGoal = student.GetComponent<MoveStudents>().DistanceToGoal();
            if (distanceToGoal < minimalStudentDistance)
            {
                target = student;
                minimalStudentDistance = distanceToGoal;
            }
        }
        // 2
        if (target != null)
        {
            if (Time.time - lastShotTime > LibraryData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
        }
    }

    private void OnStudentDestroy(GameObject enemy)
    {
        StudentsInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Student"))
        {
            StudentsInRange.Add(other.gameObject);
            StudentDestructionDelegate del =
                other.gameObject.GetComponent<StudentDestructionDelegate>();
            del.studentDelegate += OnStudentDestroy;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Student"))
        {
            StudentsInRange.Remove(other.gameObject);
            StudentDestructionDelegate del =
                other.gameObject.GetComponent<StudentDestructionDelegate>();
            del.studentDelegate -= OnStudentDestroy;
        }
    }

    private void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = LibraryData.CurrentLevel.bullet;
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
            LibraryData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

}
