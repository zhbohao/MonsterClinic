using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	// Public variables
	public float floorWidth = 100f;
	public float floorHeight = 100f;
	public float tileWidth = 10f;
	public float tileHeight = 10f;
	public GameObject hoverTile;
	public GameObject IsoCamera;
	public GameObject PresCamera;
	
	// Private Variables
	public bool IsoCam = true;
	
	// Use this for initialization
	void Start () 
	{
		// Instantiate tile for hover
		hoverTile = (GameObject) Instantiate(hoverTile);
		hoverTile.GetComponentInChildren<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for camera toggle F12 key press
		if(Input.GetKeyDown(KeyCode.F12))
		{
			// Switch camera
			swicthCamera();
		}
		
		// Cast a ray from screen mouse position to 3d world space
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit cameraRayHit;
		
		// Check if the casted ray collides
		if(Physics.Raycast(cameraRay, out cameraRayHit, 1000))
		{
				// Move the hover tile to collison point and switch it on
				Vector3 hitPoint = cameraRayHit.point;
				hitPoint = getTilePoints(hitPoint);
				hoverTile.transform.position = new Vector3(hitPoint.x*tileWidth, 0f , hitPoint.y*tileHeight);
				hoverTile.GetComponentInChildren<MeshRenderer>().enabled = true;
		}
		// If no collison, Switch off hover tile
		else
		{
			hoverTile.GetComponentInChildren<MeshRenderer>().enabled = false;
		}
	}
	
	// Convert world space floor points to tile points
	Vector2 getTilePoints(Vector3 floorPoints)
	{
		Vector2 tilePoints = new Vector2();
		
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / tileWidth);
		tilePoints.y = (int)(floorPoints.z / tileHeight);
		
		// Return the tile points
		return tilePoints;
	}
	
	// Switches the camera between isometric and prespective
	void swicthCamera()
	{
		GameObject cam;
		
		if(IsoCam)
			{
				cam = GameObject.Find("Isometric Camera");
				if(cam==null)
				{
					cam = GameObject.Find("Isometric Camera(Clone)");
				}
				GameObject.Destroy(cam);
				GameObject.Instantiate(PresCamera);
			}
			else
			{
				cam = GameObject.Find("Main Camera(Clone)");
				GameObject.Destroy(cam);
				GameObject.Instantiate(IsoCamera);
			}
			IsoCam = !IsoCam;
	}
}
