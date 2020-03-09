using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Scripting;

[Preserve]
public class HighScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highScoreEntryTransformList;

    private void Awake()
    {
        string jsonString = "";
        HighScores highScores = new HighScores();

        entryContainer = transform.Find("highScoreEntryContainer");
        entryTemplate = entryContainer.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //DeleteAllHighScores();

        //AddHighScoreEntry(40000000, "C"); // 3
        //AddHighScoreEntry(20000000, "A"); // 1

        // Grab the data from PlayerPrefs if the key exists
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            jsonString = PlayerPrefs.GetString("highScoreTable");
            highScores = JsonUtility.FromJson<HighScores>(jsonString);

            // Calls the member function to sort the list by the score
            highScores.sort();
        
            // Create the transforms
            highScoreEntryTransformList = new List<Transform>();
            // Limits the list to 10 or however many items there are
            for (int i = 0; i < highScores.highScoreEntryList.Count && i < 10; i++)
            {
                CreateHighScoreEntryTransform(highScores.highScoreEntryList[i], entryContainer, highScoreEntryTransformList);
            }
        }
    }

    // Returns true if okay to add to high scores, false otherwise
    public bool okayToAddToHighScores(int score)
    {
        // Grab the data from PlayerPrefs if the key exists
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            string jsonString = PlayerPrefs.GetString("highScoreTable");
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);

            // Calls the member function to sort the list by the score
            highScores.sort();

            int count = highScores.highScoreEntryList.Count;

            // If the count is equal to 10 then check the lowest score
            if (count == 10)
            {
                if (score > highScores.highScoreEntryList[count].score)
                {
                    return true;
                }
            }
            // If the count is less than 10 then we have space to add no matter what
            else if (count < 10)
            {
                return true;
            }
        }
        else // the PlayerPrefs hasn't been created yet so add the high score
        {
            return true;
        }
        return false;
    }

    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);
        
        int rank = transformList.Count + 1;
        string rankString;
        int score = highScoreEntry.score;
        string name = highScoreEntry.name;

        switch (rank) 
        {
            default:
                rankString = rank + "TH";
                break;
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        // Sets background for odd rows
        entryTransform.Find("rowBackground").gameObject.SetActive(rank % 2 == 1);

        // Changes the color of the first ranked name
        /*
        if (rank == 1)
        {
            entryTransform.Find("posText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.yellow;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.yellow;
        }
        */
        transformList.Add(entryTransform);
    }

    // Deletes all the entries with the key highScoreTable
    private void DeleteAllHighScores()
    {
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            PlayerPrefs.DeleteKey("highScoreTable");
            PlayerPrefs.Save();
        }
    }

    // Adds an entry to the highScoreTable
    public void AddHighScoreEntry(int score, string name)
    {
        string jsonString = "";
        // Create HighScores object
        HighScores highScores = new HighScores();
        // Create HighScoreEntry
        HighScoreEntry highScoreEntry = new HighScoreEntry { score = score, name = name };

        // Load saved HighScores if they exist
        if (PlayerPrefs.HasKey("highScoreTable"))
        {
            jsonString = PlayerPrefs.GetString("highScoreTable");
            highScores = JsonUtility.FromJson<HighScores>(jsonString);
        }

        // Add new entry to HighScores
        highScores.highScoreEntryList.Add(highScoreEntry);
        
        // Save updated HighScores
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("highScoreTable", json);
        PlayerPrefs.Save();
    }

    private class HighScores 
    {
        public List<HighScoreEntry> highScoreEntryList = new List<HighScoreEntry>();

        public void sort()
        {
            // Sort entry list by Score
            for (int i = 0; i < this.highScoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < this.highScoreEntryList.Count; j++)
                {
                    if (this.highScoreEntryList[j].score > this.highScoreEntryList[i].score)
                    {
                        HighScoreEntry tmp = this.highScoreEntryList[i];
                        this.highScoreEntryList[i] = this.highScoreEntryList[j];
                        this.highScoreEntryList[j] = tmp;
                    }
                }
            }
        }
    }

    [System.Serializable]
    private class HighScoreEntry 
    {
        public int score = 0;
        public string name = "";
    }
}
