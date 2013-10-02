using UnityEngine;
using System.Collections;
using Pathfinding;

public class MyAStarPath : MonoBehaviour {
	
	public Vector3 targetPosition;
	private Seeker seeker;
	private CharacterController controller;
	public Path path;
	
	public float speed = 3;
	
	// The max distance from the AI to a waypoint for it to continue to the waypoint
	public float nextWaypointDistance = 10;
	
	// Current waypoint (always start at index 0)
	private int currentWaypoint = 0;
	
	
	
	// Use this for initialization
	void Start () {
		targetPosition = GameObject.Find("Target").transform.position;
		seeker = GetComponent<Seeker>();
		controller = GetComponent<CharacterController>();
		
		// set path
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}
	
	public void OnPathComplete(Path p) {
		if (!p.error) {
			path = p;
			// Reset waypoint counter
			currentWaypoint = 0;
		}
	}
	
	void FixedUpdate() {
		if (path == null) 
			return;
		
		if (currentWaypoint >= path.vectorPath.Count) 
			return;
		
		// Calculate direction of unit
		Vector3 dir = (path.vectorPath[currentWaypoint] = transform.position).normalized;
		dir *= speed * Time.deltaTime;
		controller.SimpleMove(dir); // unit move here
		
		// Check if close enough 
		if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}
}
