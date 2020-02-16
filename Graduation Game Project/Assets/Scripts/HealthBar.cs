using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth = 100;
    private float originalScale;
    private float nextActionTime = 0.0f;
    public float healthDeclineSeconds;


    // Use this for initialization
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActionTime ) 
        { 
            nextActionTime = Time.time + healthDeclineSeconds;
            currentHealth = currentHealth - 1;
        } 

        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale;
    }
}
