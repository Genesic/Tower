using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerStatusManager : MonoBehaviour {
	private StatusManager statusManager;
	private CannonPlatform useCannon;
	
	public Text ErrMessage;
	public Text lvText;
	public Text towertypeText;
	public Text atkText;
	public Text spdText;
	public Text priceText;
	
	void Awake(){
		GameObject Status = GameObject.Find("GameManager");
		statusManager = Status.GetComponent<StatusManager> ();
	}

	public void set_useCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
		Debug.Log (useCannon);
		lvText.text = " Lv : " + useCannon.getLevel;
		towertypeText.text = useCannon.getName;
		atkText.text = " ATK : " + useCannon.getAtk;
		spdText.text = " SPD : " + useCannon.getSpd;
		priceText.text = " Sell : " + useCannon.getPrice + "G";
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}

}
