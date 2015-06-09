using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerStatusManager : MonoBehaviour {
	private CannonPlatform useCannon;

	public UIManager uiManager;
	public Text ErrMessage;
	public Text lvText;
	public Text towertypeText;
	public Text atkText;
	public Text spdText;
	public Text lvUpText;
	public Text priceText;

	public Button lvUpButtom;

	public void set_useCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
		lvText.text = " Lv : " + useCannon.getLevel;
		towertypeText.text = useCannon.getName;
		atkText.text = " ATK : " + useCannon.getAtk;
		spdText.text = " SPD : " + useCannon.getSpd;
		priceText.text = " Sell : " + useCannon.getPrice + "G";
		if (useCannon.getLevel == useCannon.getMaxLevel) {
			lvUpText.text = " max level";
			lvUpButtom.interactable = false;
		} else {
			lvUpText.text = " LevelUp : " + useCannon.getCost + "G";
			lvUpButtom.interactable = true;
		}

	}

	public void sell_tower(){
		if (useCannon.IsEmpty)
			return;

		int price = useCannon.getPrice;
		uiManager.statusManager.updateMoney(price);
		useCannon.SellCannon();
		close_panel ();

	}

	public void level_up_tower(){
		if (useCannon.IsEmpty)
			return;

		int cost = useCannon.getCost ;
		if (useCannon.getLevel < useCannon.getMaxLevel) {
			if (uiManager.statusManager.getMoney < cost) {
				uiManager.errMsg.show_message ("Need More Money!!");
			} else {
				uiManager.statusManager.updateMoney (-cost);
				useCannon.LevelUpCannon ();
			}
			close_panel ();
		}
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}

}
