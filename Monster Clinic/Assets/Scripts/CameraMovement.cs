using UnityEngine;
using System.Collections;

// Camera movement class
public class CameraMovement : MonoBehaviour 
{
	// Public variables
	public bool useDefaultPosition = true;
	public Vector3 position;
	public bool allowMovement = true;
	public bool allowZoom = true;
	public float speed = 100;
	
	// Use this for initialization
	void Start () 
	{
		// If camera is set to use default position, save default position
		if(useDefaultPosition)
		{
			position = transform.position;
		}
		// If camera is not set to use default position, save custom position
		else
		{
			transform.position = position;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If camera movement is allowed
		if(allowMovement)
		{
			// If hit D key, move right
			if(Input.GetKey(KeyCode.D))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
			}
			// If hit A key, move left
			if(Input.GetKey(KeyCode.A))
			{
				transform.Translate(Vector3.right * Time.deltaTime * speed * -1, Space.World);
			}
			// If hit W key, move up
			if(Input.GetKey(KeyCode.W))
			{
				transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
			}
			// If hit S key, move down
			if(Input.GetKey(KeyCode.S))
			{
				transform.Translate(Vector3.forward * Time.deltaTime * speed * -1, Space.World);
			}
		}
		// If camera zoom is allowed
		if(allowZoom)
		{
			// If hit Q key, zoom out
			if(Input.GetKey(KeyCode.Q))
			{
				transform.Translate(Vector3.up * Time.deltaTime * speed, Space.World);
			}
			// If hit E key, Zoom in
			if(Input.GetKey(KeyCode.E))
			{
				transform.Translate(Vector3.up * Time.deltaTime * speed * -1, Space.World);
			}
		}
	}
}
