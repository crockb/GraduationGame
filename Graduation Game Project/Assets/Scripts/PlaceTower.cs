using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTower : MonoBehaviour
{

	public GameObject towerPrefab;
	private GameObject tower;
	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private bool CanPlaceTower()
		{
		  return tower == null;
		}

    //1
	void OnMouseUp()
	{
	  //2
	  if (CanPlaceTower())
	  {
	    //3
	    tower = (GameObject) Instantiate(towerPrefab, transform.position, Quaternion.identity);
	    //4
	    //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
	    //audioSource.PlayOneShot(audioSource.clip);

	    // TODO: Deduct gold
	  }
	}
}
