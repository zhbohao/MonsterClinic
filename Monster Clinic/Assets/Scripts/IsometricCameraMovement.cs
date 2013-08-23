using UnityEngine;
using System.Collections;

public class IsometricCameraMovement : MonoBehaviour 
{
	// Private variables
	private float movementX;
	private float movementY;
	private GameObject IsoCamera;
	
	// Use this for initialization
	void Start () 
	{
		IsoCamera = GameObject.Find("Camera");
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get input from keyboard
		movementX = 0.5f*Input.GetAxis("Horizontal");
		movementY = 0.5f*Input.GetAxis("Vertical");
		
		// Move the camera
		transform.Translate(movementX + movementY, 0, movementY - movementX);
		
		// Zoom in camera
		if(Input.GetKey(KeyCode.E) && IsoCamera.camera.orthographicSize > 5)
        {
        	IsoCamera.camera.orthographicSize = IsoCamera.camera.orthographicSize - 1;
        }
		// Zoom out camera
		if(Input.GetKey(KeyCode.Q) && IsoCamera.camera.orthographicSize < 50)
        {
        	IsoCamera.camera.orthographicSize = IsoCamera.camera.orthographicSize + 1;
        }
	}
}
