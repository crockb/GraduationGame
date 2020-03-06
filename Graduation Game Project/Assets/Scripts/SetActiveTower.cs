using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SetActiveTower : MonoBehaviour
{
	public Text activeTowerLabel;
	private string newText;

    // Start is called before the first frame update
    void Start()
    {
        activeTowerLabel.GetComponent<Text>().text = "None";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //setting the tower as active
    public void setActiveTower()
    {
    	newText = (this.name);
    	activeTowerLabel.GetComponent<Text>().text = newText;
    }

}
