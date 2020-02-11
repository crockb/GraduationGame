using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LibraryLevel
{
    public int cost;
    public GameObject visualization;
    public GameObject bullet;
    public float fireRate;
}

public class LibraryData : MonoBehaviour
{

    public List<LibraryLevel> levels;
    private LibraryLevel currentLevel;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        currentLevel = levels[0];
    }

    public LibraryLevel GetNextLevel()
    {
        int currentLevelIndex = levels.IndexOf(currentLevel);
        int maxLevelIndex = levels.Count - 1;
        if (currentLevelIndex < maxLevelIndex)
        {
            return levels[currentLevelIndex + 1];
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
            currentLevel = levels[currentLevelIndex + 1];
        }
    }

}
