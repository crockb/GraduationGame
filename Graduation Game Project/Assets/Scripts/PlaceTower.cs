using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

[Preserve]
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


    //placing towers when mouse is clicked
	void OnMouseUp()
	{
  		// determine which tower to be placed
		activeTowerLabel = (GameObject.Find("ActiveTower").GetComponent<Text>().text);
			
		//placing starbucks tower
		if (CanPlaceStarbucks())
  		{
    		
    		starbucks = (GameObject) 
      			Instantiate(starbucksPrefab, transform.position, Quaternion.identity);
    		gameManager.Money -= starbucks.GetComponent<StarbucksData>().CurrentLevel.cost;
    		GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

  		//upgrading starbucks tower
		else if (CanUpgradeStarbucks())
		{
		  starbucks.GetComponent<StarbucksData>().IncreaseLevel();
		  gameManager.Money -= starbucks.GetComponent<StarbucksData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}
	
		//placing library tower
		else if (CanPlaceLibrary())
  		{
    		
    		library = (GameObject) 
      			Instantiate(libraryPrefab, transform.position, Quaternion.identity);
    		gameManager.Money -= library.GetComponent<LibraryData>().CurrentLevel.cost;
  			GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

  		//upgrading library tower
		else if (CanUpgradeLibrary())
		{
		  library.GetComponent<LibraryData>().IncreaseLevel();
		  gameManager.Money -= library.GetComponent<LibraryData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}

		//placing exam room tower
		else if (CanPlaceExamRoom())
  		{
    		
    		examroom = (GameObject) 
      			Instantiate(examroomPrefab, transform.position, Quaternion.identity);
    		gameManager.Money -= examroom.GetComponent<ExamRoomData>().CurrentLevel.cost;
  			GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
  		}

  		//upgrading exam room tower
		else if (CanUpgradeExamRoom())
		{
		  examroom.GetComponent<ExamRoomData>().IncreaseLevel();
		  gameManager.Money -= examroom.GetComponent<ExamRoomData>().CurrentLevel.cost;
		  GameObject.Find("ActiveTower").GetComponent<Text>().text = "None";
		}
	}

	// functions to place Starbucks

	private bool CanPlaceStarbucks()
	{
  		cost = starbucksPrefab.GetComponent<StarbucksData>().levels[0].cost;
		return starbucks == null && library == null && examroom == null && activeTowerLabel == "PlaceStarbucks" && gameManager.Money >= cost;
	}

	private bool CanUpgradeStarbucks()
	{
	  if (starbucks != null)
	  {
	    StarbucksData starbucksData = starbucks.GetComponent<StarbucksData>();
	    StarbucksLevel nextStarbucksLevel = starbucksData.GetNextLevel();
	    if (nextStarbucksLevel != null)
	    {
			return gameManager.Money >= nextStarbucksLevel.cost;
	    }
	  }
	  return false;
	}


	// functions to place Library

	private bool CanPlaceLibrary()
	{
  		cost = libraryPrefab.GetComponent<LibraryData>().levels[0].cost;
		return library == null && starbucks == null && examroom == null && activeTowerLabel == "PlaceLibrary" && gameManager.Money >= cost;
	}


	private bool CanUpgradeLibrary()
	{
	  if (library != null)
	  {
	    LibraryData libraryData = library.GetComponent<LibraryData>();
	    LibraryLevel nextLibraryLevel = libraryData.GetNextLevel();
	    if (nextLibraryLevel != null)
	    {
			return gameManager.Money >= nextLibraryLevel.cost;
	    }
	  }
	  return false;
	}

	// functions to place ExamRoom

	private bool CanPlaceExamRoom()
	{
  		cost = examroomPrefab.GetComponent<ExamRoomData>().levels[0].cost;
		return examroom == null && starbucks == null && library == null && activeTowerLabel == "PlaceExamRoom" && gameManager.Money >= cost;
	}


	private bool CanUpgradeExamRoom()
	{
	  if (examroom != null)
	  {
	    ExamRoomData examRoomData = examroom.GetComponent<ExamRoomData>();
	    ExamRoomLevel nextExamRoomLevel = examRoomData.GetNextLevel();
	    if (nextExamRoomLevel != null)
	    {
			return gameManager.Money >= nextExamRoomLevel.cost;
	    }
	  }
	  return false;
	}


}
