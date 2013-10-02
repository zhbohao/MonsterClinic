using UnityEngine;
using System.Collections;

// Test box class
public class TestBox : MonoBehaviour 
{
	
	// Public variables
	public GameObject testBox;
	public GUISkin UISkin;
	
	// Private variables
	private GameObject testingBox;
	private MeshRenderer boxRenderer;
	
	// Use this for initialization
	void Start () 
	{
		testingBox = (GameObject) GameObject.Instantiate(testBox);
		boxRenderer = testingBox.GetComponent<MeshRenderer>();
		testingBox.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(LevelManager.gameTest == Test.Box)
		{
			// Cast a ray from screen mouse position to 3d world space
			Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit cameraRayHit;
			
			// Check if the casted ray collides only floor
			if(Physics.Raycast(cameraRay, out cameraRayHit, 1000, 1 << 8))
			{
					Vector3 hitPoint = cameraRayHit.point;
					hitPoint = getTilePoints(hitPoint);
					testingBox.transform.position = new Vector3(hitPoint.x+0.5f, 0f , hitPoint.y+0.5f);
					testingBox.SetActive(true);	
				
					// If empty cell
					if(Maps.GetFloorMapValue((int)hitPoint.x, (int)hitPoint.y)==0)
					{
						boxRenderer.material.SetColor("_Color", new Color(0F, 1.0F, 0F, 0.25F));
						if(Input.GetMouseButtonDown(0))
						{
							GameObject.Instantiate(testBox, testingBox.transform.position, testingBox.transform.rotation);
							Maps.SetFloorMapValue(new Vector2(testingBox.transform.position.x, testingBox.transform.position.z), 1);
							testingBox.SetActive(false);
							LevelManager.gameTest = Test.None;
						}	
					}
					else
					{
						boxRenderer.material.SetColor("_Color", new Color(1.0F, 0.0F, 0F, 0.25F));
					}
			}
			else
			{
				testingBox.SetActive(false);
			}
		}
	}
	
	// Convert world space floor points to tile points
	Vector2 getTilePoints(Vector3 floorPoints)
	{
		Vector2 tilePoints = new Vector2();
		
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / 1f);
		tilePoints.y = (int)(floorPoints.z / 1f);
		
		// Return the tile points
		return tilePoints;
	}
	
	void OnGUI()
	{
		GUI.skin = UISkin;
		
		if(LevelManager.gameTest != Test.Box)
		{
			if(GUI.Button(new Rect(Screen.width*0.005f+(2*34f), Screen.height*0.005f, 90f, 30f), "Test Box"))
			{
					LevelManager.gameTest = Test.Box;
			}
		}
	}
}
