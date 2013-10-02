using UnityEngine;
using System.Collections;

public class MousePoint : MonoBehaviour {

	RaycastHit hit;
	
	private float raycastLength = 500;
	
	void Update() 
	{
		GameObject Target = GameObject.Find ("Target");
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit, 1000)) 
		{
			if (hit.collider.name == "Floor") {
				Target.transform.position = hit.point;
			}
		}
		
		Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.yellow);
	}
}
