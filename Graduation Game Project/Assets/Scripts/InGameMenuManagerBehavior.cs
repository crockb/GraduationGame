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
	public Text gameOverLabel;
	public Text gameWonLabel;
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
	    if (wave <= 10)
	    {
	    	nextWaveLabel.GetComponent<Animator>().SetTrigger("nextWave");
	    	waveCountLabel.text = (wave + 1) + "/10";
		}

		// transition to next level or check for GameOver
		else
		{


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
	    	if (GameStats.dropouts <= 10)
	    	{
	    		dropOutsLabel.text = value + "/10";
	    	}
	    	// 10 dropouts - game over
	    	else
	    	{

	    	}

	  	}
	}


    // Start is called before the first frame update
    void Start()
    {
		Money = GameStats.money;
		DropOuts = GameStats.dropouts;
		Score = GameStats.score;
        Wave = 0;
        gameOverLabel.GetComponent<Animator>().ResetTrigger("gameOverTrigger");
        gameWonLabel.GetComponent<Animator>().ResetTrigger("gameWonTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // note:  these two functions will be updated to support additional GameOver behaviors
    private void GameOver()
    {
    	gameOverLabel.GetComponent<Animator>().SetTrigger("gameOverTrigger");
    }

    private void GameWon()
    {
    	gameWonLabel.GetComponent<Animator>().SetTrigger("gameWonTrigger");
    }


}