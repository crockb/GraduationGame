﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

[Preserve]
public class InGameMenuManagerBehavior : MonoBehaviour
{
	public Text moneyLabel;
	public Text waveCountLabel;
	public Text dropOutsLabel;
	public Text scoreLabel;
	public GameObject gameOverLabel;
	public GameObject gameWonLabel;
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
	    //increase wave count and trigger "next wave" prompt
	    if (wave < 10 && gameOver == false)
	    {
	    	nextWaveLabel.GetComponent<Animator>().SetTrigger("nextWave");
	    	waveCountLabel.text = (wave + 1) + "/10";
		}

		//start next level if game is not over
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


    //Trigger Game over prompts and load next scene
    void GameOver()
    {
    	if (gameOver == false)
    	{
    		gameOverLabel.GetComponent<Animator>().SetTrigger("gameOverTrigger");
    		gameOver = true;
    		StartCoroutine(WaitToLoadNextScene());
    	}    	
    }

    //Trigger Game won prompts and load next scene
    void GameWon()
    {
    	gameWonLabel.GetComponent<Animator>().SetTrigger("gameWonTrigger");
    	gameOver = true;
    	StartCoroutine(WaitToLoadNextScene());
    }

    //Trigger Level complete prompts and load next scene
    void NextLevel()
    {
    	levelCompleteLabel.GetComponent<Animator>().SetTrigger("levelCompleteTrigger");
    	Money = 100;
    	StartCoroutine(WaitToLoadNextScene());
    }

    //calculate the score
	private int CalculateScore() {
		int score = 0;
		// Score (aka Gamestats.score) is being incremented in MoveStudent.cs
		score = Score + Money - (DropOuts * 50); 
		if (DropOuts == 0) {
			score += 1000; // bonus for perfect round
		}
		if (score < 0) {
			score = 0;
		}
		return score;
	}

    // used to delay the next scene load
    private IEnumerator WaitToLoadNextScene()
    {
    	if (gameOver == true)
    	{
    		yield return new WaitForSeconds(4);
			// Check to see if Score should be added to high scores
			HighScoreTable highScores = new HighScoreTable();
			HighScoreEntry.score = CalculateScore();
			if (highScores.okayToAddToHighScores(HighScoreEntry.score))
			{
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