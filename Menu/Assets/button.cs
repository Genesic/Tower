using UnityEngine;
using System.Collections;

public class button : MonoBehaviour {

	private void OnGUI()
	{
		if(GUI.Button(new Rect(15, 15, 200, 100), "Load Level"))
			Application.LoadLevel("GameScene");
	}
}
