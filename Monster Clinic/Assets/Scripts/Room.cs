using UnityEngine;
using System.Collections;

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
	Type1,
    Type2
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
public class Room : MonoBehaviour 
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
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	// Constructor room
	Room()
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
	Room(RoomType roomType, Vector2 leftDown, Vector2 leftUp, Vector2 RightDown, Vector2 RightUp)
	{
		roomId = ++roomIdCounter;
		type = roomType;
		leftBottom = leftDown;
		leftTop = leftUp;
		rightBottom = RightDown;
		rightTop = RightUp;
		// Value (-1,-1) for door means room has no door
		door = new Vector2(-1,-1);
	}
}
