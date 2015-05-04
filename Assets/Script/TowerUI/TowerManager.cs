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

		if( useCannon.checkBuildCannon(useTower) )
		{
			Vector3 TowerPosition = useCannon.Position;
			Quaternion TowerRotation = Quaternion.identity;
			Tower buildTower = Instantiate (useTower, TowerPosition, TowerRotation) as Tower;
			useCannon.BuildCannon (buildTower);
		}

		// 關閉UI介面
		gameObject.SetActive (false);
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}
}
