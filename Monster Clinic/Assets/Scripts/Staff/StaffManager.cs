using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StaffManager : MonoBehaviour {
	
	public GameObject octodoctor;
	public GameObject cthuluburse;
	public GameObject yetitor;
	
	public List<Octodoctor> octodoctorList = new List<Octodoctor>();
	public List<Cthuluburse> ctuluburseList = new List<Cthuluburse>();
	public List<Yetitor> yetitorList = new List<Yetitor>();
	
	public StaffType temp;
	public GameObject staffTempRef;
	public MeshRenderer staffTempRefRend;
	

	
	// Use this for initialization
	void Start () {
		///ref the gameobjects to the prefab class
		HospitalPrefabs.Octodoctor = octodoctor;
		HospitalPrefabs.Cthuluburse = cthuluburse;
		HospitalPrefabs.Yetitor = yetitor;
		
		temp = StaffType.None;
		staffTempRef = null;
	}
	
	
	void OnGUI()
	{
		//just for a test buttons
		if(GUI.Button(new Rect(0,50,100,20),"Octodoctor"))
		{
			if(LevelManager.gameState == State.Placement)
				return;
				
			/// make the temp the staff i want
			temp = StaffType.Octodoctor;
			LevelManager.gameState = State.Placement;			
			SpawnTempStaff(HospitalPrefabs.Octodoctor);
		}
		if(GUI.Button(new Rect(150,50,100,20),"Cthuluburse"))
		{
			if(LevelManager.gameState == State.Placement)
				return;///if we are in the placement mode you cant purchase another staff
			
			///same again
			temp = StaffType.Cthuluburse;
			LevelManager.gameState = State.Placement;
			SpawnTempStaff(HospitalPrefabs.Cthuluburse);
		}
		if(GUI.Button(new Rect(300,50,100,20),"Yetitor"))
		{
			if(LevelManager.gameState == State.Placement)
				return;
			
			temp = StaffType.Yetitor;
			LevelManager.gameState = State.Placement;
			SpawnTempStaff(HospitalPrefabs.Yetitor);
		}
	}
	
	void SpawnTempStaff(GameObject staffPrefab)
	{
		// Instantiate staff temp
		staffTempRef = (GameObject) Instantiate(staffPrefab);
		staffTempRef.GetComponentInChildren<MeshRenderer>().enabled = false;
		
		// Create a mesh renderer component
		staffTempRefRend =staffTempRef.GetComponentInChildren<MeshRenderer>();
	}
	
	void Update()
	{
		if(LevelManager.gameState == State.Placement)
		{
			if(Input.GetKeyUp(KeyCode.Escape))
			{
				LevelManager.gameState = State.None;
				temp = StaffType.None;
				Destroy(staffTempRef);
				staffTempRefRend = null;
				
				return;
			}
			
		// Cast a ray from screen mouse position to 3d world space
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit cameraRayHit;
		
		// Check if the casted ray collides only floor
		if(Physics.Raycast(cameraRay, out cameraRayHit, 1000, 1 << 8))
		{
				// Move the hover tile to collison point and switch it on
				Vector3 hitPoint = cameraRayHit.point;
				hitPoint = getTilePoints(hitPoint);
				staffTempRef.transform.position = new Vector3(hitPoint.x*1f, 0f , hitPoint.y*1f);
				staffTempRefRend.enabled = true;
				staffTempRef.transform.localScale = new Vector3(1F,1F,1F);
				
				// If empty cell outside room
				if(Maps.GetFloorMapValue((int)hitPoint.x, (int)hitPoint.y)==0)
				{
					// Set the color of tile to green
					staffTempRefRend.material.SetColor ("_Color", new Color(0F, 1.0F, 0F, 0.25F));
					if(Input.GetMouseButtonDown(0))
					{
						////plant the staff down
						PositionStaff(new Vector2(hitPoint.x, hitPoint.y));
					}	
				}
				// If not an empty cell outside room
				else
				{
					// Set the color of tile to red
					staffTempRefRend.material.SetColor ("_Color", new Color(1.0F, 0F, 0F, 0.25F));
				}
		}
		// If no collison, switch off hover tile
		else
		{
			staffTempRefRend.enabled = false;
		}
			
		}
	}
	
	void PositionStaff(Vector2 pos)
	{
		//put the staff in the list
		AddStaff();
		staffTempRef = null;
		LevelManager.gameState = State.None;
		temp = StaffType.None;
		
	}
	
	void AddStaff()
	{
		switch(temp)
		{
		case StaffType.Cthuluburse:
			Cthuluburse c = new Cthuluburse();
			c.staffModel = staffTempRef;
			ctuluburseList.Add (c);
			break;
		case StaffType.Octodoctor:
			Octodoctor o = new Octodoctor();
			o.staffModel = staffTempRef;
			octodoctorList.Add (o);
			break;
		case StaffType.Yetitor:
			Yetitor y = new Yetitor();
			y.staffModel = staffTempRef;
			yetitorList.Add (y);
			break;
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
}
