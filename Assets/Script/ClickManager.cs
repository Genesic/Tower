using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

	public Tower useTower;

	public void setUseTower ( Tower tower)
	{
		useTower = tower;
	}

	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if( Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CannonPlatform") ) ){
				if (useTower != null ){
					hit.transform.gameObject.GetComponent<CannonPlatform>().BuildCannon(useTower);
					Vector3 TowerPosition = hit.transform.position;
					TowerPosition.y += 1;
					Quaternion TowerRotation = Quaternion.identity;
					Instantiate ( useTower, TowerPosition, TowerRotation);
				} else {
					Debug.Log ( "Fail: useTower = " + useTower);
				}
			}
		}
	}
}
