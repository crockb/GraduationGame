﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

[Preserve]
[System.Serializable]
public class ExamRoomLevel
{
  	public int cost;
  	public GameObject visualization;
  	public GameObject bullet;
  	public float fireRate;

}

[Preserve]
public class ExamRoomData : MonoBehaviour
{
	public List<ExamRoomLevel> levels;
	private ExamRoomLevel currentLevel;

	//Set current level
	void OnEnable()
	{
	  CurrentLevel = levels[0];
	}

	//1
	public ExamRoomLevel CurrentLevel
	{
	  //send current level
	  get 
	  {
	    return currentLevel;
	  }
	  //determine current level and set if active
	  set
	  {
	    currentLevel = value;
	    int currentLevelIndex = levels.IndexOf(currentLevel);

	    GameObject levelVisualization = levels[currentLevelIndex].visualization;
	    for (int i = 0; i < levels.Count; i++)
	    {
	      if (levelVisualization != null) 
	      {
	        if (i == currentLevelIndex) 
	        {
	          levels[i].visualization.SetActive(true);
	        }
	        else
	        {
	          levels[i].visualization.SetActive(false);
	        }
	      }
	    }
	  }
	}


	//Determine the next level
	public ExamRoomLevel GetNextLevel()
	{
	  int currentLevelIndex = levels.IndexOf (currentLevel);
	  int maxLevelIndex = levels.Count - 1;
	  if (currentLevelIndex < maxLevelIndex)
	  {
	    return levels[currentLevelIndex+1];
	  } 
	  else
	  {
	    return null;
	  }
	}

	//Increase level
	public void IncreaseLevel()
	{
	  int currentLevelIndex = levels.IndexOf(currentLevel);
	  if (currentLevelIndex < levels.Count - 1)
	  {
	    CurrentLevel = levels[currentLevelIndex + 1];
	  }
	}
}
