using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreEntry : MonoBehaviour
{
    private InputField nameInputField;
    public Text scoreValue;
    public static int score;
    
    void start()
    {
        nameInputField = GameObject.Find("NameInputField").GetComponent<InputField>();
    }

    void Update()
    {
        scoreValue.text = score.ToString();
    }

    public void submit() 
    {
        nameInputField = GameObject.Find("NameInputField").GetComponent<InputField>();
        // If the input field is empty then don't do anything
        if (nameInputField.text == "")
        {
            return;
        } 
        else
        {
            HighScoreTable highScore = new HighScoreTable();
            highScore.AddHighScoreEntry(score, nameInputField.text);
            SceneManager.LoadScene("HighScores");
        }
    }
}