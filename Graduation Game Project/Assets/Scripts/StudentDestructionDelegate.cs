using UnityEngine;
using System.Collections;

public class StudentDestructionDelegate : MonoBehaviour
{

    public delegate void StudentDelegate(GameObject student);
    public StudentDelegate studentDelegate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        if (studentDelegate != null)
        {
            studentDelegate(gameObject);
        }
    }

}
