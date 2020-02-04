using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHighScore : MonoBehaviour
{
	public InputField firstNameField;
	public InputField lastNameField;
	public InputField scoreField;

	public Button submitButton;
	public void CallAddHighScore() 
	{
		StartCoroutine(AddHighScore());
	}

	IEnumerator AddHighScore()
	{
		WWWForm form = new WWWForm();
		form.AddField("first_name", firstNameField.text);
		form.AddField("last_name", lastNameField.text);
		form.AddField("score", scoreField.text);
		WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
		yield return www;
		if (www.text == "0")
		{
			Debug.Log("User created successfully.");
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
		else
		{
			Debug.Log("User creation failed. Error #" + www.text);
		}
	}

	public void VerifyInputs()
	{
		submitButton.interactable = (firstNameField.text.Length >= 8 && lastNameField.text.Length >= 8 && scoreField.text > 0);
	}
}
