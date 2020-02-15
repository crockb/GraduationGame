using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTower : MonoBehaviour
{

	public GameObject starbucksPrefab, libraryPrefab, examroomPrefab;
	private GameObject starbucks, library, examroom;
	private string activeTowerLabel;
	private int cost;

	private InGameMenuManagerBehavior gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("InGameMenuManager").GetComponent<InGameMenuManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	void OnMouseUp()
	{
  		// determine which tower to be placed
		activeTowerLabel = (GameObject.Find("ActiveTower").GetComponent<Text>().text);
			
		if (CanPlaceStarbucks())
  		{
    		//3
    		starbucks = (GameObject) 
      			Instantiate(starbucksPrefab, transform.position, Quaternion.identity);
    		gameManager.GraduationCoins -= starbucks.GetComponent<StarbucksData>().CurrentLevel.cost;
    		GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

		else if (CanUpgradeStarbucks())
		{
		  starbucks.GetComponent<StarbucksData>().IncreaseLevel();
		  gameManager.GraduationCoins -= starbucks.GetComponent<StarbucksData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}
	
		else if (CanPlaceLibrary())
  		{
    		//3
    		library = (GameObject) 
      			Instantiate(libraryPrefab, transform.position, Quaternion.identity);
    		gameManager.GraduationCoins -= library.GetComponent<LibraryData>().CurrentLevel.cost;
  			GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

		else if (CanUpgradeLibrary())
		{
		  library.GetComponent<LibraryData>().IncreaseLevel();
		  gameManager.GraduationCoins -= library.GetComponent<LibraryData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}

		else if (CanPlaceExamRoom())
  		{
    		//3
    		examroom = (GameObject) 
      			Instantiate(examroomPrefab, transform.position, Quaternion.identity);
    		gameManager.GraduationCoins -= examroom.GetComponent<ExamRoomData>().CurrentLevel.cost;
  			GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

		else if (CanUpgradeExamRoom())
		{
		  examroom.GetComponent<ExamRoomData>().IncreaseLevel();
		  gameManager.GraduationCoins -= examroom.GetComponent<ExamRoomData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}
	}

	// functions to place StarBucksTower

	private bool CanPlaceStarbucks()
	{
  		cost = starbucksPrefab.GetComponent<StarbucksData>().levels[0].cost;
		return starbucks == null && library == null && examroom == null && activeTowerLabel == "PlaceStarbucks" && gameManager.GraduationCoins >= cost;
	}

	private bool CanUpgradeStarbucks()
	{
	  if (starbucks != null)
	  {
	    StarbucksData starbucksData = starbucks.GetComponent<StarbucksData>();
	    StarbucksLevel nextStarbucksLevel = starbucksData.GetNextLevel();
	    if (nextStarbucksLevel != null)
	    {
			return gameManager.GraduationCoins >= nextStarbucksLevel.cost;
	    }
	  }
	  return false;
	}


	// functions to place Library

	private bool CanPlaceLibrary()
	{
  		cost = libraryPrefab.GetComponent<LibraryData>().levels[0].cost;
		return library == null && starbucks == null && examroom == null && activeTowerLabel == "PlaceLibrary" && gameManager.GraduationCoins >= cost;
	}


	private bool CanUpgradeLibrary()
	{
	  if (library != null)
	  {
	    LibraryData libraryData = library.GetComponent<LibraryData>();
	    LibraryLevel nextLibraryLevel = libraryData.GetNextLevel();
	    if (nextLibraryLevel != null)
	    {
			return gameManager.GraduationCoins >= nextLibraryLevel.cost;
	    }
	  }
	  return false;
	}

	// functions to place ExamRoom

	private bool CanPlaceExamRoom()
	{
  		cost = examroomPrefab.GetComponent<ExamRoomData>().levels[0].cost;
		return examroom == null && starbucks == null && library == null && activeTowerLabel == "PlaceExamRoom" && gameManager.GraduationCoins >= cost;
	}


	private bool CanUpgradeExamRoom()
	{
	  if (examroom != null)
	  {
	    ExamRoomData examRoomData = examroom.GetComponent<ExamRoomData>();
	    ExamRoomLevel nextExamRoomLevel = examRoomData.GetNextLevel();
	    if (nextExamRoomLevel != null)
	    {
			return gameManager.GraduationCoins >= nextExamRoomLevel.cost;
	    }
	  }
	  return false;
	}


}