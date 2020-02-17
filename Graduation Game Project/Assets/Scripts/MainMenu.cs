using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

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
		SceneManager.LoadScene(1);
	}

		public void PlayCollegeOfBusiness()
	{
		SceneManager.LoadScene(2);
	}

}
