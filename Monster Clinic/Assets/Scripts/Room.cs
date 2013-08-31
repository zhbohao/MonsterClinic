using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Enum mode
public enum Mode
{
    None,
	RoomCreation,
    RoomFurnishing,
    RoomDeletion
}

// Enum state
public enum State
{
    None,
	Purchase,
    Placement
}

// Enum room type
public enum RoomType
{
    None,
	Reception,
	StaffBreak,
	PatientWard,
	Diagnostics,
	SlimeTreatment,
	ShockTreatment,
	MagicPotion,
	PhysicalActivity,
	Type1
}

// Enum room type
public enum RectanglePosition
{
    None,
	LeftBottom,
	LeftTop,
	RightBottom,
	RightTop
}

// Room class
public class Room 
{
	// Public static variables
	public static int roomIdCounter = 0;
	
	// Public variables
	public int roomId;
	public RoomType type;
	public Vector2 leftBottom;
	public Vector2 leftTop;
	public Vector2 rightBottom;
	public Vector2 rightTop;
	public Vector2 door;
	public List<GameObject> leftWall;
	public List<GameObject> rightWall;
	public List<GameObject> bottomWall;
	public List<GameObject> topWall;
	
	// Constructor room
	public Room()
	{
		roomId = ++roomIdCounter;
		type = RoomType.None;
		leftBottom = new Vector2();
		leftTop = new Vector2();
		rightBottom = new Vector2();
		rightTop = new Vector2();
		door = new Vector2();
	}
	
	// Overloaded constructor room
	public Room(RoomType roomType, Vector2 leftDown, Vector2 leftUp, Vector2 rightDown, Vector2 rightUp, GameObject horizontalWall, GameObject verticalWall, Transform t)
	{
		roomId = ++roomIdCounter;
		type = roomType;
		leftBottom = leftDown;
		leftTop = leftUp;
		rightBottom = rightDown;
		rightTop = rightUp;
		// Value (-1,-1) for door means room has no door
		Vector2 door = new Vector2(-1f,-1f);
		leftWall = new List<GameObject>();
		rightWall = new List<GameObject>();
		bottomWall = new List<GameObject>();
		topWall = new List<GameObject>();
		
		// Calculate length of walls
		float xD = rightBottom.x - leftBottom.x;
		float yD = leftTop.y - leftBottom.y;
		GameObject wall;
		
		// Build left wall
		for(int i = 0; i<yD; i++)
		{
			wall = (GameObject) GameObject.Instantiate(verticalWall, new Vector3(leftBottom.x,0F,leftBottom.y+i), t.rotation);
			leftWall.Add(wall);
		}
		// Build right wall
		for(int i = 0; i<yD; i++)
		{
			wall = (GameObject) GameObject.Instantiate(verticalWall, new Vector3(rightBottom.x,0F,rightBottom.y+i), t.rotation);
			rightWall.Add(wall);
		}
		// Build bottom wall
		for(int i = 0; i<xD; i++)
		{
			wall = (GameObject) GameObject.Instantiate(horizontalWall, new Vector3(leftBottom.x+i,0F,leftBottom.y), t.rotation);
			bottomWall.Add(wall);
		}
		// Build top wall
		for(int i = 0; i<xD; i++)
		{
			wall = (GameObject) GameObject.Instantiate(horizontalWall, new Vector3(leftTop.x+i,0F,leftTop.y), t.rotation);
			topWall.Add(wall);
		}
	}
}
