using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	//determine and load specific scenes
	
	public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Debug.Log("QUIT");
		Application.Quit();
	}

	public void PlayWildlifeSciences()
	{
		SceneManager.LoadScene("WildlifeSciences");
	}

	public void PlayCollegeOfBusiness()
	{
		SceneManager.LoadScene("CollegeOfBusiness");
	}

	public void PlayCollegeOfEducation()
	{
		SceneManager.LoadScene("CollegeOfEducation");
	}

	public void OpenHighScores()
	{
		SceneManager.LoadScene("HighScores");
	}

	public void OpenStartMenu()
	{
		SceneManager.LoadScene("StartMenu");
	}

	public void OpenHowToPlay()
	{
		SceneManager.LoadScene("HowToPlay");
	}

}