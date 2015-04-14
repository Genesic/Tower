using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour 
{
	public void StartClick(string levelName)
	{
		Application.LoadLevel (levelName);
	}

	public void SettingClick(string levelName)
	{
		Application.LoadLevel (levelName);
	}

	public void CreditClick(string levelName)
	{
		Application.LoadLevel (levelName);
	}

	public void ExitClick(string levelName)
	{
		Application.Quit ();
	}

}
