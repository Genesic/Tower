using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Canvas canvas;
	public ErrMessage errMsg;
	public StatusManager statusManager;
	public GameObject statusPanel;
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
		Vector3 newPosition = getPanelPosition(selectTowerPanel);
		selectTowerPanel.GetComponent<RectTransform> ().position = newPosition;
		selectTowerPanel.SetActive(true);
	}

	// 開啟塔狀態介面
	public void openTowerStatusPanel (CannonPlatform selectCannon){
		closeAllPanel ();
		TowerStatusPanel.GetComponent<TowerStatusManager>().set_useCannon(selectCannon);
		Vector3 newPosition = getPanelPosition(TowerStatusPanel);
		TowerStatusPanel.GetComponent<RectTransform> ().position = newPosition;
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
		float canvas_width = canvas.GetComponent<RectTransform>().rect.width * canvas.scaleFactor;
		float canvas_height = canvas.GetComponent<RectTransform> ().rect.height * canvas.scaleFactor;
		RectTransform  statusRt = statusPanel.GetComponent<RectTransform> ();

		float width = statusRt.rect.width * canvas.scaleFactor;
		float height = statusRt.rect.height * canvas.scaleFactor;

		if (check.x > canvas_width - width  && check.y > canvas_height - height )
			return false;
		
		if (check.x < width && check.y > canvas_height - height)
			return false;
		
		return true;
	}

	// 關閉所有介面
	public void closeAllPanel() {
		selectTowerPanel.SetActive(false);
		TowerStatusPanel.SetActive(false);
	}
	
	// 計算介面開啟後的位置
	Vector3 getPanelPosition( GameObject panel){
		float canvas_width = canvas.GetComponent<RectTransform>().rect.width * canvas.scaleFactor;
		float canvas_height = canvas.GetComponent<RectTransform> ().rect.height * canvas.scaleFactor;
		RectTransform rt = panel.GetComponent<RectTransform> ();
		Vector2 newPosition = Input.mousePosition;
		float offset = 10.0f;
		float width = (rt.rect.width + offset ) * canvas.scaleFactor;
		float height = (rt.rect.height ) * canvas.scaleFactor;
		
		if (newPosition.x + width > canvas_width ) {
			newPosition.x -= width / 2;
		} else {
			newPosition.x += width / 2;
		}
		
		if (newPosition.y + height > canvas_height ) {
			newPosition.y -= height / 2;
		} else {
			newPosition.y += height / 2 ;
		}

		return newPosition;
	}

}
