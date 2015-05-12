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
		currentHP = maxHP;

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
			MinusUIHP(0.01f);
			timeCount = 0;
		}
	}

	//輸入扣血由0到1
	public void MinusUIHP(float maxIsOne)
	{
		if (currentHP > 0) 
		{
			currentHP -= (maxIsOne * maxHP);

			if (currentHP <= 0)
				currentHP = 0;

			HPText.text = currentHP + " / " + maxHP;
			HPScroll.rectTransform.anchoredPosition = new Vector2 (HPScroll.rectTransform.anchoredPosition.x - maxIsOne * 180, 0);
		}
	}
		
}
