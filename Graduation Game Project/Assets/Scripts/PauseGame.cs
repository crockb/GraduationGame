using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
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