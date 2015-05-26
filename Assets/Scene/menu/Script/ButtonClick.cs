using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour 
{
	public void LevelClick(int num)
	{
		Application.LoadLevel ("game");
	}

	public void StartClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuOut", 0f);
		
		Animator anim2;
		GameObject menu2 = GameObject.Find ("LevelSelect");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuInto", 0.5f);
	}

	public void SettingClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuOut", 0f);

		Animator anim2;
		GameObject menu2 = GameObject.Find ("Setting");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuInto", 0.5f);
	}

	public void MusicToggle()
	{
		AudioSource audioSource;
		GameObject obj = GameObject.Find ("EventSystem");
		audioSource = obj.GetComponent<AudioSource> ();
		audioSource.mute = !audioSource.mute;
	}

	public void CreditClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuOut", 0f);
		
		Animator anim2;
		GameObject menu2 = GameObject.Find ("Shop");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuInto", 0.5f);
		
	}

	public void ExitClick(string levelName)
	{
		Application.Quit();
	}


	public void LevelBackClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuInto", 0.5f);
		
		Animator anim2;
		GameObject menu2 = GameObject.Find ("LevelSelect");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuOut", 0f);
	}

	public void SettingBackClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuInto", 0.5f);

		Animator anim2;
		GameObject menu2 = GameObject.Find ("Setting");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuOut", 0f);
	}

	public void ShopBackClick()
	{
		Animator anim;
		GameObject menu = GameObject.Find ("MenuBg");
		anim = menu.GetComponent<Animator> ();
		anim.CrossFade ("MainMenuInto", 0.5f);
		
		Animator anim2;
		GameObject menu2 = GameObject.Find ("Shop");
		anim2 = menu2.GetComponent<Animator> ();
		anim2.CrossFade ("SettingMenuOut", 0f);
	}




}
