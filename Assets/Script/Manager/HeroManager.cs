using UnityEngine;
using System.Collections;

public class HeroManager : MonoBehaviour {

	bool enableHeroTag = false;
	bool isHeroCreated = false;
	GameObject heroPrefab;
	Vector3 offset;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (enableHeroTag)
		{
			var cam = GameObject.Find ("MoveCamera");

			//cam.GetComponent<Transform> ().position = heroPrefab.GetComponent<Transform> ().position + new Vector3(0, 2, -1);
			//cam.GetComponent<Transform> ().LookAt(heroPrefab.transform);

			float currentAngle = cam.GetComponent<Transform> ().transform.eulerAngles.y;
			float desiredAngle = heroPrefab.GetComponent<Transform> ().transform.eulerAngles.y;
			float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * 1);
			
			Quaternion rotation = Quaternion.Euler(0, angle, 0);
			cam.GetComponent<Transform> ().transform.position = heroPrefab.GetComponent<Transform> ().transform.position - (rotation * offset);
			
			cam.GetComponent<Transform> ().transform.LookAt(heroPrefab.GetComponent<Transform> ().transform);
		}

	}

	public void CreateHero()
	{
		if (!enableHeroTag) 
		{
			if(!isHeroCreated)
			{
				var path = string.Format ("3rdPersonCharacter/Characters/ThirdPersonCharacter/Prefabs/ThirdPersonController");
				var prefab = Resources.Load<GameObject> (path);
				heroPrefab = GameObject.Instantiate (prefab);
				heroPrefab.transform.parent = GameObject.Find ("Map_1").transform;
				heroPrefab.GetComponent<Transform> ().position = new Vector3 (0, 1, 0);

				isHeroCreated = true;
			}

			var cam = GameObject.Find ("MoveCamera");
			cam.GetComponent<Transform> ().position = heroPrefab.GetComponent<Transform> ().position + new Vector3 (0, 2, -3);
			cam.GetComponent<CameraMove> ().enabled = false;

			var cam2 = GameObject.Find ("Main Camera");
			var rotation = Quaternion.Euler (-10, 0, 0);
			cam2.GetComponent<Transform> ().rotation = rotation;

			offset = heroPrefab.GetComponent<Transform> ().position - cam.GetComponent<Transform> ().transform.position;

			enableHeroTag = true;
		} 
		else 
		{
			var cam = GameObject.Find ("MoveCamera");
			cam.GetComponent<Transform> ().position = new Vector3 (0, 15, -15);
			cam.GetComponent<Transform> ().rotation = Quaternion.Euler (0, 0, 0);
			cam.GetComponent<CameraMove> ().enabled = true;

			var cam2 = GameObject.Find ("Main Camera");
			var rotation = Quaternion.Euler (45, 0, 0);
			cam2.GetComponent<Transform> ().rotation = rotation;

			enableHeroTag = false;
		}
	}
}
