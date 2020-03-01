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
	public GameObject levelCompleteLabel;
	public bool gameOver = false;
	private int wave;
	private Scene currentScene;

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

	//For Business Level
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
	    if (wave < 10 && gameOver == false)
	    {
	    	nextWaveLabel.GetComponent<Animator>().SetTrigger("nextWave");
	    	waveCountLabel.text = (wave + 1) + "/10";
		}

		else if (gameOver != false)
		{
			NextLevel();
		}

		else
		{
			// do nothing - game over script will execute
		}
	  }

	}

	//for Education and Wildlife levels
	// set the wave value in the menu
	public int WaveInfo
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

		else
		{
			NextLevel();
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
	    	if (GameStats.dropouts < 10)
	    	{
	    		dropOutsLabel.text = value + "/10";
	    	}
	    	// 10 dropouts - game over
	    	else
	    	{
	    		dropOutsLabel.text = "10/10";
	    		GameOver();
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
        currentScene = SceneManager.GetActiveScene();
        gameOverLabel.GetComponent<Animator>().ResetTrigger("gameOverTrigger");
        gameWonLabel.GetComponent<Animator>().ResetTrigger("gameWonTrigger");
        levelCompleteLabel.GetComponent<Animator>().ResetTrigger("levelCompleteTrigger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // switch levels
    public void TransitionLevels()
    {
    	// dropouts exceeds limit - game is over
    	if (GameStats.dropouts >= 10)
    	{
    		GameOver();
    	}

    	// all levels complete - game is won
    	else if (currentScene.name == "CollegeOfEducation" && Wave == 10 && GameStats.dropouts < 10)
    	{
    		GameWon();
    	}

    	// level complete - switch levels
    	else
    	{
    		NextLevel();
    	}
    }


    // execute game steps
    void GameOver()
    {
    	if (gameOver == false)
    	{
    		gameOverLabel.GetComponent<Animator>().SetTrigger("gameOverTrigger");
    		gameOverLabel.GetComponent<Animator>().ResetTrigger("gameOverTrigger");
    		levelCompleteLabel.GetComponent<Animator>().ResetTrigger("levelCompleteTrigger");
    		gameOver = true;
    		StartCoroutine(WaitToLoadNextScene());
    	}    	
    }

    void GameWon()
    {
    	gameWonLabel.GetComponent<Animator>().SetTrigger("gameWonTrigger");
    	levelCompleteLabel.GetComponent<Animator>().ResetTrigger("gameWonTrigger");
    	gameOver = true;
    	StartCoroutine(WaitToLoadNextScene());
    }

    void NextLevel()
    {
    	levelCompleteLabel.GetComponent<Animator>().SetTrigger("levelCompleteTrigger");
    	levelCompleteLabel.GetComponent<Animator>().ResetTrigger("levelCompleteTrigger");
    	StartCoroutine(WaitToLoadNextScene());
    }

    // used to delay the next scene load
    private IEnumerator WaitToLoadNextScene()
    {
    	if (gameOver == true)
    	{
    		yield return new WaitForSeconds(4);
			// Check to see if Score should be added to high scores
			HighScoreTable highScores = new HighScoreTable();
			if (highScores.okayToAddToHighScores(Score))
			{
				HighScoreEntry.score = Score;
				SceneManager.LoadScene("HighScoreEntry");
			}
			else
			{
				SceneManager.LoadScene("HighScores");
			}
    	}

    	else
    	{
    		yield return new WaitForSeconds(4);
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    	}
    }
}