using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour
{

    public float maxHealth = 100;
    public float currentHealth = 100;
    private float originalScale;
    private GameObject theHealthBar;
    private float nextActionTime = 0.0f;
    public float healthDeclineSeconds;
    private InGameMenuManagerBehavior gameManager;


    // Use this for initialization
    void Start()
    {
        theHealthBar = gameObject.transform.Find("HealthBar").gameObject;
        originalScale = theHealthBar.transform.localScale.x;
        gameManager = GameObject.Find("InGameMenuManager").GetComponent<InGameMenuManagerBehavior>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time > nextActionTime && currentHealth > 0) 
        { 
            nextActionTime = Time.time + healthDeclineSeconds;
            currentHealth = currentHealth - 1;
        } 

        if (currentHealth == 0)
        {
            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //audioSource.PlayOneShot(audioSource.clip);
            Destroy(gameObject);
            gameManager.DropOuts = GameStats.dropouts + 1;

        }

        Vector3 tmpScale = theHealthBar.transform.localScale;
        tmpScale.x = currentHealth / maxHealth * originalScale;
        theHealthBar.transform.localScale = tmpScale;
    }
}