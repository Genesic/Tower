using UnityEngine;
using System.Collections;

public class SelectTower : MonoBehaviour {

	public void setTower (Tower useTower)
	{
		Camera.main.GetComponent<ClickManager> ().setUseTower (useTower);
	}
}
