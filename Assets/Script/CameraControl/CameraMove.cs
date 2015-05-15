using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public UIManager uiCtrl;
	public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
	public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth

	public float keyboardMoveSpeed = 0.5f;
	public float keyboardTurnSpeed = 2.0f;
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	private bool isPanning;			// Is the camera being panned?
	private bool isRotating;		// Is the camera being rotated?
	private bool isZooming;			// Is the camera zooming?

	// Use this for initialization
	void Start () {
		turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
		panSpeed = 4.0f;		// Speed of the camera when being panned
		zoomSpeed = 4.0f;		// Speed of the camera going back and forth
		keyboardMoveSpeed = 0.5f;
		keyboardTurnSpeed = 2.0f;
	}

	// Update is called once per frame
	void Update () {
		// --- mouse part ---
		// Get the left mouse button
		if(Input.GetMouseButtonDown(0))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isPanning = true;

			/*
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if( Physics.Raycast(ray, out hit, Mathf.Infinity) )
				Debug.Log (hit.transform.gameObject);
			*/
		}
		
		// Get the right mouse button
		if(Input.GetMouseButtonDown(1))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isRotating = true;
		}
		
		// Disable movements on button release
		if (!Input.GetMouseButton(0)) 
			isPanning=false;
		if (!Input.GetMouseButton(1)) 
			isRotating=false;
		
		// Move the camera on it's XY plane
		if (isPanning)
		{
			Vector3 beforeMovePos = transform.position;
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			Vector3 move = new Vector3(- pos.x * panSpeed, 0, - pos.y * panSpeed);
			transform.Translate(move);

			if (transform.position.x < -10 || transform.position.x > 85 || transform.position.z > 60 || transform.position.z < -40)
				transform.position = beforeMovePos;

			if( Vector3.Magnitude(move) > 0 )
				uiCtrl.closeAllPanel();
		}

		// Rotate camera along X and Y axis
		if (isRotating)
		{
			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
			transform.RotateAround(transform.position, Vector3.up, -pos.x * turnSpeed);

			if( Vector3.Magnitude(pos) > 0)
				uiCtrl.closeAllPanel();
		}

		// 	--- keyboard part ---
		//	--move--
		if (Input.GetKey(KeyCode.W))
		{
			Vector3 beforeMovePos = transform.position;
			Vector3 move = new Vector3(0, 0, keyboardMoveSpeed);
			transform.Translate(move);

			if (transform.position.x < -10 || transform.position.x > 85 || transform.position.z > 60 || transform.position.z < -40)
				transform.position = beforeMovePos;

			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.S))
		{
			Vector3 beforeMovePos = transform.position;
			Vector3 move = new Vector3(0, 0, -keyboardMoveSpeed);
			transform.Translate(move);

			if (transform.position.x < -10 || transform.position.x > 85 || transform.position.z > 60 || transform.position.z < -40)
				transform.position = beforeMovePos;

			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.A))
		{
			Vector3 beforeMovePos = transform.position;
			Vector3 move = new Vector3(-keyboardMoveSpeed, 0, 0);
			transform.Translate(move);

			if (transform.position.x < -10 || transform.position.x > 85 || transform.position.z > 60 || transform.position.z < -40)
				transform.position = beforeMovePos;

			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.D))
		{
			Vector3 beforeMovePos = transform.position;
			Vector3 move = new Vector3(keyboardMoveSpeed, 0, 0);
			transform.Translate(move);

			if (transform.position.x < -10 || transform.position.x > 85 || transform.position.z > 60 || transform.position.z < -40)
				transform.position = beforeMovePos;

			uiCtrl.closeAllPanel();
		}

		//	--Rotate--
		if (Input.GetKey(KeyCode.Q))
		{
			transform.RotateAround(transform.position, new Vector3(0, -1, 0), keyboardTurnSpeed);
			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.E))
		{
			transform.RotateAround(transform.position, new Vector3(0, 1, 0), keyboardTurnSpeed);
			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.Alpha2))
		{
			GameObject cam = GameObject.Find("Main Camera");
			if(cam)
				cam.transform.Rotate(-1, 0, 0);

			uiCtrl.closeAllPanel();
		}
		if (Input.GetKey(KeyCode.X))
		{
			GameObject cam = GameObject.Find("Main Camera");
			if(cam)
				cam.transform.Rotate(1, 0, 0);

			uiCtrl.closeAllPanel();
		}
	}

	void LateUpdate()
	{
		//Input.GetAxis("Mouse ScrollWheel") < 0 表示滾輪向前滾動
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			float _height = transform.position.y;
			_height = _height + zoomSpeed;

			if (_height > 40)
				transform.position = new Vector3(transform.position.x, 40, transform.position.z);
			else
				transform.position = new Vector3(transform.position.x, _height, transform.position.z);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			float _height = transform.position.y;
			_height = _height - zoomSpeed;
			
			if (_height < 10)
				transform.position = new Vector3(transform.position.x, 10, transform.position.z);
			else
				transform.position = new Vector3(transform.position.x, _height, transform.position.z);
		}
	}
}