using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManagerBehavior : MonoBehaviour
{
	public Text graduationCoinsLabel;
	private int graduationCoins;
	public Text waveCountLabel;
	public GameObject nextWaveLabel;
	public bool gameOver = false;


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

	private int wave;
	public int Wave
	{
	  get
	  {
	    return wave;
	  }
	  set
	  {
	    wave = value;
	    if (!gameOver)
	    {
	    	nextWaveLabel.GetComponent<Animator>().SetTrigger("nextWave");
	    	waveCountLabel.text = (wave + 1) + "/10";
		}
	  }

	}


    // Start is called before the first frame update
    void Start()
    {
        GraduationCoins = 1000;
        Wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}