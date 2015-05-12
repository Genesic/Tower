using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUD : MonoBehaviour {
	GameObject healthUI;
	Image healthScroll;

	Text healthText;
	float maxHP = 100;
	float currentHP;


	float timeCount = 0;
	// Use this for initialization
	void Start () {
		currentHP = maxHP;

		healthUI = Instantiate(Resources.Load ("HUD/healthBar")) as GameObject;
		healthUI.transform.parent = GameObject.Find ("Canvas").transform;
		healthUI.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, 370);

		Image[] images = healthUI.GetComponentsInChildren<Image> ();
		foreach (Image image in images)
		{
			if(image.name.Equals("healthScroll"))
				healthScroll = image;
		}

		Text[] texts = healthUI.GetComponentsInChildren<Text> ();
		foreach (Text text in texts)
		{
			if(text.name.Equals("healthText"))
				healthText = text;
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
		currentHP -= (maxIsOne * maxHP);
		healthText.text = currentHP +" / "+ maxHP;
		healthScroll.rectTransform.anchoredPosition = new Vector2(healthScroll.rectTransform.anchoredPosition.x - maxIsOne * 190, 0);
	}
		
}
