using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	public void pauseGame()
	{
		Time.timeScale = 0;
	}

	public void resumeGame()
	{
		Time.timeScale = 1;
	}
}


/* ORIGINAL WORKING SCRIPT

public class PauseGame : MonoBehaviour
{
	bool isPaused = false;

	public void pauseGame()
	{
		if (isPaused) {
			Time.timeScale = 1;
			isPaused = false;

		} else {

			Time.timeScale = 0;
			isPaused = true;
		}
	}
}
*/