using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
	GameObject HPUI;
	Image HPScroll;

	Text HPText;
	float maxHP = 1000;

	// Use this for initialization
	void Start () {

		HPUI = Instantiate(Resources.Load ("HUD/HPBar")) as GameObject;
		HPUI.transform.parent = GameObject.Find ("Canvas").transform;
		HPUI.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 370);

		Image[] images = HPUI.GetComponentsInChildren<Image> ();
		foreach (Image image in images)
		{
			if(image.name.Equals("HPScroll"))
				HPScroll = image;
		}

		Text[] texts = HPUI.GetComponentsInChildren<Text> ();
		foreach (Text text in texts)
		{
			if(text.name.Equals("HPText"))
				HPText = text;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	//直接設定血量
	public void SetUIHP(float maxIsOne)
	{
		if (maxIsOne <= 0)
			maxIsOne = 0;

		int nowHP = (int)(maxIsOne * maxHP);
		HPText.text = nowHP + " / " + maxHP;
		HPText.rectTransform.anchoredPosition = new Vector2 ();
		HPScroll.rectTransform.anchoredPosition = new Vector2 ((maxIsOne - 1) * 180, 0);
	}
		
}
