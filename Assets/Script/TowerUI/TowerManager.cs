using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerManager : MonoBehaviour {

	private StatusManager statusManager;
	public Tower[] tower_list;
	private CannonPlatform useCannon;

	public Text ErrMessage;

	void Awake(){
		GameObject Status = GameObject.Find("GameManager");
		statusManager = Status.GetComponent<StatusManager> ();
	}

	public void set_useCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
	}
	
	public void build_tower(int selectTower)
	{
		Tower useTower = tower_list [selectTower -1];

		if (!useCannon.IsEmpty) {
			ErrMessage.GetComponent<ErrMessage> ().show_message ("Here Already Build Tower!!");
			gameObject.SetActive (false);
			return;
		}

		int cost = useTower.Cost;
		Debug.Log (cost);
		Debug.Log (statusManager);
		if (!statusManager.updateMoney (-cost)) {
			ErrMessage.GetComponent<ErrMessage> ().show_message ("Need More Money!!");
			gameObject.SetActive (false);
			return;
		}
				
		useCannon.BuildCannon (useTower);
		Vector3 TowerPosition = useCannon.Position;
		Quaternion TowerRotation = Quaternion.identity;
		Instantiate (useTower, TowerPosition, TowerRotation);

		// 關閉UI介面
		gameObject.SetActive (false);
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}
}
