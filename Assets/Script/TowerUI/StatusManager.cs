using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusManager : MonoBehaviour {

	[SerializeField]
	private int kill;
	[SerializeField]
	private int score;

	public int money;

	public Text killText;
	public Text scoreText;
	public Text moneyText;

	void Awake(){
		updateMoney (0);
	}

	public int getMoney { get {return money;} }

	public void updateKill(int patch){
		kill += patch;
		killText.text = "擊殺數量 : " + kill;
	}

	public void updateScore(int patch){
		score += patch;
		scoreText.text = "分數 : " + score;
	}

	public bool updateMoney(int patch){
		if (money + patch < 0)
			return false;

		money += patch;
		moneyText.text = "金錢 : " + money;
		return true;
	}

	// Update is called once per frame
	void Update () {
	}
}
