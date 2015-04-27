using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTower : MonoBehaviour {
	[SerializeField]
	private GameObject Panel;
	public Tower useTower;
	private CannonPlatform useCannon;

	public void setCannon(CannonPlatform selectCannon)
	{
		useCannon = selectCannon;
	}

	public void buildTower()
	{
		if (useCannon != null) {
			Vector3 TowerPosition = useCannon.Position;
			Quaternion TowerRotation = Quaternion.identity;
			Instantiate (useTower, TowerPosition, TowerRotation);
		} else {
			Debug.Log (useCannon);
		}
		Panel.SetActive(false);
	}

	public void testMessage(){
		Debug.Log ("hello~" + useTower );
	}
}
