using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuManagerBehavior : MonoBehaviour
{
	public Text moneyLabel;
	public Text waveCountLabel;
	public Text dropOutsLabel;
	public Text scoreLabel;
	public GameObject nextWaveLabel;
	public bool gameOver = false;
	private int wave;

	// set the money value in the menu
	public int Money {
	  	get
	  	{ 
	    	return GameStats.money;
	  	}
	  	set
	  	{
	    	GameStats.money = value;
	    	moneyLabel.GetComponent<Text>().text = "$" + value;
	  	}
	}

	// set the wave value in the menu
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

	// set the score value in the menu
	public int Score
	{
	  	get
	  	{ 
	    	return GameStats.score;
	  	}
	  	set
	  	{
	    	GameStats.score = value;
	    	scoreLabel.text = value + "";
	  	}
	}

	// set the dropouts value in the menu
	public int DropOuts 
	{
	  	get
	  	{ 
	    	return GameStats.dropouts;
	  	}
	  	set
	  	{
	    	GameStats.dropouts = value;
	    	dropOutsLabel.text = value + "/10";
	  	}
	}


    // Start is called before the first frame update
    void Start()
    {
		Money = GameStats.money;
		DropOuts = GameStats.dropouts;
		Score = GameStats.score;
        Wave = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

// WORKING CODE BEFORE PUBLIC VARIABLE UPDATES
/*
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

*/