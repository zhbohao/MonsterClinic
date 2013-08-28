﻿using UnityEngine;
using System.Collections;

// Maps class
public class Maps : MonoBehaviour 
{
	// Maps
	/// <summary>
	/// Floor map is used to keep track of map
	/// -1 for empty cell outside room
	/// 0 for non-empty cell outside room
	/// 1 for empty cell inside room
	/// 2 for non-empty cell inside room
	/// 3 for walls (Horizontal)
	/// 4 for walls (Vertical)
	/// 5 for doors
	/// </summary>
	private static int [,] floorMap = new int[100,100];
	
	/// <summary>
	/// Room map is used to keep track of rooms
	/// -1 for no room
	/// 0 not defined yet
	/// 1,2,3...999 Room IDs
	/// </summary>
	private static int [,] roomMap = new int[100,100];
	
	// Use this for initialization
	void Start () 
	{
		// Initialize floor map
		for(int i=0; i<100; i++)
		{
			for(int j=0; j<100; j++)
			{
				floorMap[i,j] = -1;
				roomMap[i,j] = -1;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// Get floorMap value
	public static int GetFloorMapValue(Vector2 points)
	{
		return floorMap[(int)points.x, (int)points.y];
	}
	
	// Get floorMap value
	public static float GetFloorMapValue(int x, int y)
	{
		return floorMap[x,y];
	}
	
	// Set floorMap value
	public static void SetFloorMapValue(Vector2 points, int val)
	{
		floorMap[(int)points.x, (int)points.y] = val;
	}
	
	// Set floorMap value
	public static void SetFloorMapValue(int x, int y, int val)
	{
		floorMap[x,y] = val;
	}
	
	// Get roomMap value
	public static int GetRoomMapValue(Vector2 points)
	{
		return roomMap[(int)points.x, (int)points.y];
	}
	
	// Get roomMap value
	public static float GetRoomMapValue(int x, int y)
	{
		return roomMap[x,y];
	}
	
	// Set roomMap value
	public static void SetRoomMapValue(Vector2 points, int val)
	{
		roomMap[(int)points.x, (int)points.y] = val;
	}
	
	// Set floorMap value
	public static void SetRoomMapValue(int x, int y, int val)
	{
		roomMap[x,y] = val;
	}
}
