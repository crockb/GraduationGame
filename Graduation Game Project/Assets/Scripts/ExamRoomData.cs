using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ExamRoomLevel
{
  	public int cost;
  	public GameObject visualization;
  	public GameObject bullet;
  	public float fireRate;

}


public class ExamRoomData : MonoBehaviour
{
	public List<ExamRoomLevel> levels;
	private ExamRoomLevel currentLevel;

	void OnEnable()
	{
	  CurrentLevel = levels[0];
	}

	//1
	public ExamRoomLevel CurrentLevel
	{
	  //2
	  get 
	  {
	    return currentLevel;
	  }
	  //3
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

	public void IncreaseLevel()
	{
	  int currentLevelIndex = levels.IndexOf(currentLevel);
	  if (currentLevelIndex < levels.Count - 1)
	  {
	    CurrentLevel = levels[currentLevelIndex + 1];
	  }
	}
}
