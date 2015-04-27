using UnityEngine;
using System.Collections;

public class ClickManager : MonoBehaviour {

	public GameObject selectTowerPannel;
	
	void Update () {
	/*
		if (Input.GetMouseButtonDown (1)) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if( Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CannonPlatform") ) ){
				CannonPlatform selectCannon = hit.transform.gameObject.GetComponent<CannonPlatform>();
				Debug.Log (selectCannon);
				selectTowerPannel.GetComponent<SelectTower>().setCannon(selectCannon);
			}
		}
*/
		if (Input.GetMouseButtonDown (0) ) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if( Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CannonPlatform") ) ){
				openTowerPanel();
			}
		}
	}

	void openTowerPanel (){
		Vector3 newPosition = Input.mousePosition;
		newPosition.x += 64.0F;
		newPosition.y -= 110.0F;
		
		if( newPosition.x > 776 ){
			newPosition.x -= 128.0F;
		}
		if( newPosition.y < 120 ){
			newPosition.y += 220.0F;
		}
		selectTowerPannel.transform.position = newPosition;
		Debug.Log ( "origin:" + Input.mousePosition + " new: " + newPosition);
		selectTowerPannel.SetActive(true);
	}
}
