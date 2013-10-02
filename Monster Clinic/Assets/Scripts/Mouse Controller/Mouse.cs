using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public static GameObject CurrentlySelectedUnit;
	public GameObject targetPref;
	public GameObject movePointTarget;
	
	RaycastHit hit;

	private Vector3 mouseDownPoint;
	
	void Awake () 
	{
		mouseDownPoint = Vector3.zero;
	}
	
	void Update() 
	{
	
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) 
		{
			// store point mouse button down
			if (Input.GetMouseButtonDown (0)) {
				mouseDownPoint = hit.point;
			}
			
			if (hit.collider.name == "Floor") {
				// if mouse right click, instantiate the target
				// 0 left
				// 1 right
				// 2 middle
				if (Input.GetMouseButtonDown(1)) {
					GameObject targetObj = Instantiate(targetPref, hit.point, Quaternion.identity) as GameObject;
					movePointTarget.transform.position = targetObj.transform.position; // set move target to the clicked point.
				}
			}
		}
		
		Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
	}
	
	#region helper functions
	
	// check if user clicked the mouse
	public bool DidUserClickLeftMouse (Vector3 hitPoint) {
		float clickZone = 1.3f;
		
		if ( (mouseDownPoint.x < hitPoint.x + clickZone && mouseDownPoint.x > hitPoint.x - clickZone) &&
			 (mouseDownPoint.y < hitPoint.y + clickZone && mouseDownPoint.y > hitPoint.y - clickZone) &&
			 (mouseDownPoint.z < hitPoint.z + clickZone && mouseDownPoint.z > hitPoint.z - clickZone) ){
			return true;
		}
		return false;
	}
	
	// diselected if user selected
	public static void DiselectedGameobjectIfSelected () {
		if (CurrentlySelectedUnit != null) {
			CurrentlySelectedUnit.transform.FindChild("Selected").gameObject.SetActive(false); // set the producter inactive
			CurrentlySelectedUnit = null;
		}
	}
	
	#endregion
}
