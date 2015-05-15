using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TowerManager : MonoBehaviour {

	private UIManager uiManager;
	private StatusManager statusManager;
	public Tower[] tower_list;
	private CannonPlatform useCannon;

	public Text ErrMessage;

	void Awake(){
		GameObject Status = GameObject.Find("GameManager");
		uiManager = Status.GetComponent<UIManager> ();
		//StatusManager = uiManager.get
	}

	public void set_useCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
	}
	
	public void build_tower(int selectTower)
	{
		Tower useTower = tower_list [selectTower -1];

		if (!useCannon.checkBuildCannon (useTower))
			return;

		int cost = useTower.Cost;		
		if ( uiManager.statusManager.getMoney < cost) {
			uiManager.errMsg.show_message("Need More Money!!");
			return;
		}

		uiManager.statusManager.updateMoney (-useTower.Cost);
		
		Vector3 TowerPosition = useCannon.Position;
		Quaternion TowerRotation = Quaternion.identity;
		Tower buildTower = Instantiate (useTower, TowerPosition, TowerRotation) as Tower;
		
		useCannon.BuildCannon (buildTower);
		buildTower.setUseCannon(useCannon);
		buildTower.level_up();


		// 關閉UI介面
		gameObject.SetActive (false);
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}
}
