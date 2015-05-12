using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public ErrMessage errMsg;
	public GameObject selectTowerPanel;
	public GameObject TowerStatusPanel;

	public ErrMessage getErrMsg(){
		return errMsg;
	}

	public GameObject getSelectTowerPanel(){
		return selectTowerPanel;
	}

	public GameObject getTowerStatusPanel(){
		return TowerStatusPanel;
	}

	// 檢查滑鼠點在cube上時要開起的panel
	public void setMouseDownCubePanel(CannonPlatform selectCannon) {
		if ( check_other_ui_panel() ) {
			if ( !anyPanelOpen() ){
				if ( selectCannon.IsEmpty ){
					openBuildTowerPanel(selectCannon);
				} else {
					openTowerStatusPanel(selectCannon);
				}
			}
		}
	}

	// 檢查滑鼠點在tower上時要開起的panel
	public void setMouseDownTowerPanel(CannonPlatform selectCannon) {
		if ( check_other_ui_panel() ) {
			if ( !anyPanelOpen() ){
				openTowerStatusPanel(selectCannon);
			}
		}
	}
	
	// 開啟蓋塔介面
	public void openBuildTowerPanel (CannonPlatform selectCannon){
		closeAllPanel ();
		selectTowerPanel.GetComponent<TowerManager>().set_useCannon(selectCannon);
		Vector3 newPosition = getPanelPosition();
		selectTowerPanel.transform.position = newPosition;
		selectTowerPanel.SetActive(true);
	}

	// 開啟塔狀態介面
	public void openTowerStatusPanel (CannonPlatform selectCannon){
		closeAllPanel ();
		TowerStatusPanel.GetComponent<TowerStatusManager>().set_useCannon(selectCannon);
		Vector3 newPosition = getPanelPosition();
		TowerStatusPanel.transform.position = newPosition;
		TowerStatusPanel.SetActive(true);
	}

	// 檢查是否有任何介面是開啟的
	bool anyPanelOpen (){
		if (selectTowerPanel.activeSelf)
			return true;
		    
		if (TowerStatusPanel.activeSelf)  
			return true;
		
		return false;
	}
	

	// 檢查是否有點到其他UI介面
	bool check_other_ui_panel(){
		Vector2 check = Input.mousePosition;
		if (check.x > 833 && check.y > 456)
			return false;
		
		if (check.x < 121 && check.y > 460)
			return false;
		
		return true;
	}

	// 關閉所有介面
	void closeAllPanel() {
		selectTowerPanel.SetActive(false);
		TowerStatusPanel.SetActive(false);
	}
	
	// 計算介面開啟後的位置
	Vector3 getPanelPosition(){
		Vector3 newPosition = Input.mousePosition;
		newPosition.x += 64.0F;
		newPosition.y -= 110.0F;
		
		if( newPosition.x > 776 ){
			newPosition.x -= 128.0F;
		}
		if( newPosition.y < 120 ){
			newPosition.y += 220.0F;
		}
		
		return newPosition;
	}

}
