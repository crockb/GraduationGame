using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StarbucksLevel
{
  	public int cost;
  	public GameObject visualization;
  	public GameObject bullet;
  	public float fireRate;

}

public class StarbucksData : MonoBehaviour
{

	public List<StarbucksLevel> levels;
	private StarbucksLevel currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //set current level
	void OnEnable()
	{
	  CurrentLevel = levels[0];
	}

	public StarbucksLevel CurrentLevel
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

	//determine next level
	public StarbucksLevel GetNextLevel()
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

	//increase level
	public void IncreaseLevel()
	{
	  int currentLevelIndex = levels.IndexOf(currentLevel);
	  if (currentLevelIndex < levels.Count - 1)
	  {
	    CurrentLevel = levels[currentLevelIndex + 1];
	  }
	}

}
