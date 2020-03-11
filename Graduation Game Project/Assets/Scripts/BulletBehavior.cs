using UnityEngine;
using System.Collections;
using UnityEngine.Scripting;

[Preserve]
public class BulletBehavior : MonoBehaviour
{

    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        distance = Vector2.Distance(startPosition, targetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        // 1 
        float timeInterval =  Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);

        // 2 
        if (gameObject.transform.position == targetPosition)
        {
            if (target != null)
            {
                // 3
                //Transform healthBarTransform = target.transform.Find("HealthBar");
                HealthBar healthBar =
                    target.GetComponent<HealthBar>();

                //check that "damage" doesnt exceed full health
                if ((healthBar.currentHealth + Mathf.Max(damage, 0)) > 100)
                {
                    healthBar.currentHealth = 100;
                }
                //add "damage" to student health
                else
                {
                    healthBar.currentHealth += Mathf.Max(damage, 0);
                }
                
            //remove bullets    
            Destroy(gameObject);
            }
        Destroy(gameObject);
        }
    }

}
