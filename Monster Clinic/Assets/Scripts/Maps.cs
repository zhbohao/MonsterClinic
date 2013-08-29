using UnityEngine;
using System.Collections;

// Maps class
public class Maps
{
	// Maps
	
	/// <summary>
	/// Floor map is used to keep track of map
	/// 0 for empty cell outside room
	/// 1 for non-empty cell outside room
	/// 2 for empty cell inside room
	/// 3 for non-empty cell inside room
	/// </summary>
	private static int [,] floorMap = new int[100,100];
	
	/// <summary>
	/// Room map is used to keep track of rooms
	/// 0 for no room
	/// 1,2,3...999 Room IDs
	/// </summary>
	private static int [,] roomMap = new int[100,100];
	
	/// <summary>
	/// Object map is used to keep track reference to game objects (walls and doors)
	/// </summary>
	/*private static GameObject [,] objectsMap = new GameObject[100,100];*/
	
	// Get floorMap value
	public static int GetFloorMapValue(Vector2 points)
	{
		return floorMap[(int)points.x, (int)points.y];
	}
	
	// Get floorMap value
	public static int GetFloorMapValue(int x, int y)
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
	public static int GetRoomMapValue(int x, int y)
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
	/*
	// Get objectsMap value
	public static GameObject GetObjectsMapValue(Vector2 points)
	{
		return objectsMap[(int)points.x, (int)points.y];
	}
	
	// Get objectsMap value
	public static GameObject GetObjectsMapValue(int x, int y)
	{
		return objectsMap[x,y];
	}
	
	// Set objectsMap value
	public static void SetObjectsMapValue(Vector2 points, GameObject val)
	{
		objectsMap[(int)points.x, (int)points.y] = val;
	}
	
	// Set objectsMap value
	public static void SetObjectsMapValue(int x, int y, GameObject val)
	{
		objectsMap[x,y] = val;
	}
	
	// Delete objectsMap value
	public static void DeleteObjectsMapValue(Vector2 points)
	{
		GameObject.Destroy(objectsMap[(int)points.x, (int)points.y]);
	}
	
	// Delete objectsMap value
	public static void DeleteObjectsMapValue(int x, int y)
	{
		GameObject.Destroy(objectsMap[x,y]);
	}*/
}
