using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {
	
	public Tower[] tower_list;
	private CannonPlatform useCannon;

	public void set_useCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
	}
	
	public void build_tower(int selectTower)
	{
		Tower useTower = tower_list [selectTower -1];
		if (useCannon.IsEmpty) {
			useCannon.BuildCannon (useTower);
			Vector3 TowerPosition = useCannon.Position;
			Quaternion TowerRotation = Quaternion.identity;
			Instantiate (useTower, TowerPosition, TowerRotation);
		}

		// 關閉UI介面
		gameObject.SetActive (false);
	}

	public void close_panel(){
		gameObject.SetActive (false);
	}
}
