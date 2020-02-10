using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManagerBehavior : MonoBehaviour
{
	public Text graduationCoinsLabel;
	private int graduationCoins;

	public int GraduationCoins {
	  	get
	  	{ 
	    	return graduationCoins;
	  	}
	  	set
	  	{
	    	graduationCoins = value;
	    	graduationCoinsLabel.GetComponent<Text>().text = "$" + graduationCoins;
	  	}
	}



    // Start is called before the first frame update
    void Start()
    {
        GraduationCoins = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}