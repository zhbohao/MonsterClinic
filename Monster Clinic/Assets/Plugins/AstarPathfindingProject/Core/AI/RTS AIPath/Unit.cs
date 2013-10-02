using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	// for Mouse.cs
	public Vector2 ScreenPos;
	public bool OnScreen;
	public bool Selected = false;
	
	public bool isWalkable = true;
	
	// Update is called once per frame
	void Update () {
		// if unit not selected, get screen space
		if (!Selected) {
			// track the screen position
			ScreenPos = Camera.main.WorldToScreenPoint(this.transform.position);
			
			// if within the screen
			
		}
	}
}
