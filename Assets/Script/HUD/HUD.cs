using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
	GameObject HPUI;
	Image HPScroll;

	Text HPText;
	float maxHP = 100;
	float currentHP;


	float timeCount = 0;
	// Use this for initialization
	void Start () {
		currentHP = 1;

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
	void Update () {
		timeCount += Time.deltaTime;
		if (timeCount >= 0.1f) 
		{
			currentHP -= 0.01f;
			SetUIHP(currentHP);
			timeCount = 0;
		}
	}

	//輸入扣血由0到1
	public void SetUIHP(float maxIsOne)
	{
		if (maxIsOne <= 0)
			maxIsOne = 0;

		int nowHP = (int)(maxIsOne * maxHP);
		HPText.text = nowHP + " / " + maxHP;
		HPScroll.rectTransform.anchoredPosition = new Vector2 ((maxIsOne - 1) * 180, 0);
	}
		
}
