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
		Debug.Log (useCannon);
		if (useCannon.IsEmpty) {
			useCannon.BuildCannon (useTower);
			Vector3 TowerPosition = useCannon.Position;
			Quaternion TowerRotation = Quaternion.identity;
			Instantiate (useTower, TowerPosition, TowerRotation);
		}
		gameObject.SetActive (false);
	}
}
