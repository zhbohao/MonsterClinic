using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Level manager class
public class LevelManager : MonoBehaviour 
{
	// Public static variables
	public static Mode gameMode = Mode.None;
	public static State gameState = State.None;
	public static Test gameTest = Test.None;
	public static RoomType selectedRoomType = RoomType.None;
	
	// Public variables
	public float floorWidth = 100f;
	public float floorHeight = 100f;
	public float tileWidth = 1f;
	public float tileHeight = 1f;
	public GameObject hoverTile;
	public GameObject horizontalWall;
	public GameObject verticalWall;
	public GameObject IsoCamera;
	public GameObject PresCamera;
	
	// Private Variables
	private bool IsoCam = true;
	private MeshRenderer hoverTileRenderer;
	private Vector2 selectedCell;
	private Room choosedRoom;
	private List<Room> rooms;
	
	// Use this for initialization
	void Start () 
	{	
		// Instantiate tile for hover
		hoverTile = (GameObject) Instantiate(hoverTile);
		hoverTile.GetComponentInChildren<MeshRenderer>().enabled = false;
		
		// Create a mesh renderer component
		hoverTileRenderer = hoverTile.GetComponentInChildren<MeshRenderer>();
		
		// Initialize other variables
		selectedRoomType = RoomType.None;
		selectedCell = new Vector2(-1,-1);
		rooms = new List<Room>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for camera toggle F12 key press
		if(Input.GetKeyDown(KeyCode.F12))
		{
			// Switch camera
			swicthCamera();
		}
		
		// If game mode is room creation
		if(gameMode == Mode.RoomCreation)
		{
		   bool roomPlaced = false;
		   // If state is purchase
		   if(gameState == State.Purchase)
		   {
				// AGUI is handling this part
		   }
		   // If state is placement
		   else if (gameState == State.Placement)
		   {
		   		// If cell is not selected
				if(selectedCell.x==-1)
				{
					getFirstClick();
				}
				// If cell is already selected
				else
				{
					roomPlaced = getRoomPlacement();
				}
		   }
		   // If escape key is pressed, exit room creation mode
		   if(Input.GetKey(KeyCode.Escape) || roomPlaced)
		   {
				gameMode = Mode.None;
				gameState = State.None;
				selectedRoomType = RoomType.None;
				selectedCell = new Vector2(-1,-1);
				hoverTileRenderer.enabled = false;
		   }
		}
		
		// If game mode is room deletion
		if(gameMode == Mode.RoomDeletion)
		{
			// If game state is choose
			if(gameState == State.Choose)
			{
				// Select a room
				choosedRoom = chooseRoom();
				
				// If user has highlighted the room
				if(choosedRoom != null)
				{
					// If user has highlighted the room
					if(Input.GetMouseButtonUp(0))
					{
						// Swicth off hover tile
						hoverTileRenderer.enabled = false;
						hoverTile.transform.localScale = new Vector3(1F,1F,1F);
						
						// Change the state to deletion
						gameState = State.Deletion;
					}
				}
				
				// If excape key is pressed
				if(Input.GetKey(KeyCode.Escape))
				{
					// Swicth off hover tile
					hoverTileRenderer.enabled = false;
					hoverTile.transform.localScale = new Vector3(1F,1F,1F);
					
					// Reset the mode and state
					gameMode = Mode.None;
					gameState = State.None;
				}
			}
			
			// If game state is deletion
			if(gameState == State.Deletion)
			{
				// Reset values for map
				setBlockValueRoomMap(choosedRoom.leftBottom, choosedRoom.xLength, choosedRoom.yLength, 0);
				setBlockValueFloorMap(choosedRoom.leftBottom, choosedRoom.xLength, choosedRoom.yLength, 0);
				
				// Delete room
				rooms.Remove(choosedRoom);
				choosedRoom.deleteWallsDoors();
				choosedRoom = null;
				
				// Reset the mode and state
				gameMode = Mode.None;
				gameState = State.None;
			}
		}
	}
	
	// Convert world space floor points to tile points
	Vector2 getTilePoints(Vector3 floorPoints)
	{
		Vector2 tilePoints = new Vector2();
		
		// Convert the space points to tile points
		tilePoints.x = (int)(floorPoints.x / tileWidth);
		tilePoints.y = (int)(floorPoints.z / tileHeight);
		
		// Return the tile points
		return tilePoints;
	}
	
	// Switches the camera between isometric and prespective
	void swicthCamera()
	{
		GameObject cam;
		
		if(IsoCam)
			{
				cam = GameObject.Find("Isometric Camera");
				if(cam==null)
				{
					cam = GameObject.Find("Isometric Camera(Clone)");
				}
				GameObject.Destroy(cam);
				GameObject.Instantiate(PresCamera);
			}
			else
			{
				cam = GameObject.Find("Main Camera(Clone)");
				GameObject.Destroy(cam);
				GameObject.Instantiate(IsoCamera);
			}
			IsoCam = !IsoCam;
	}
	
	// Get the first click on room placement
	void getFirstClick()
	{
		// Cast a ray from screen mouse position to 3d world space
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit cameraRayHit;
		
		// Check if the casted ray collides only floor
		if(Physics.Raycast(cameraRay, out cameraRayHit, 1000, 1 << 8))
		{
				// Move the hover tile to collison point and switch it on
				Vector3 hitPoint = cameraRayHit.point;
				hitPoint = getTilePoints(hitPoint);
				hoverTile.transform.position = new Vector3(hitPoint.x*tileWidth, 0f , hitPoint.y*tileHeight);
				hoverTileRenderer.enabled = true;
				hoverTile.transform.localScale = new Vector3(1F,1F,1F);
				
				// If empty cell outside room
				if(Maps.GetFloorMapValue((int)hitPoint.x, (int)hitPoint.y)==0)
				{
					// Set the color of tile to green
					hoverTileRenderer.material.SetColor ("_Color", new Color(0F, 1.0F, 0F, 0.25F));
					if(Input.GetMouseButtonDown(0))
					{
						selectedCell = new Vector2(hitPoint.x, hitPoint.y);
					}	
				}
				// If not an empty cell outside room
				else
				{
					// Set the color of tile to red
					hoverTileRenderer.material.SetColor ("_Color", new Color(1.0F, 0F, 0F, 0.25F));
				}
		}
		// If no collison, switch off hover tile
		else
		{
			hoverTileRenderer.enabled = false;
		}
	}
	
	// Get room placement
	bool getRoomPlacement()
	{
		// Cast a ray from screen mouse position to 3d world space
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit cameraRayHit;
		
		// Check if the casted ray collides only floor, then highlight the rectangular area
		if(Physics.Raycast(cameraRay, out cameraRayHit, 1000, 1 << 8))
		{
			// Move the hover tile to collison point
			Vector3 hitPoint = cameraRayHit.point;
			Vector2 mouseOverCell = getTilePoints(hitPoint);
			
			// Calculate x and y distances, cell corner position
			int xDist;
			int yDist;
			xDist = (int)(mouseOverCell.x-selectedCell.x);
			yDist = (int)(mouseOverCell.y-selectedCell.y);
			RectanglePosition selectedCellPos = getRectPosition(xDist, yDist);
			
			// Position the hover tile
			if(selectedCellPos == RectanglePosition.LeftBottom)
			{
				hoverTile.transform.position = new Vector3 (selectedCell.x, 0F, selectedCell.y);
			}
			else if(selectedCellPos == RectanglePosition.RightBottom)
			{
				hoverTile.transform.position = new Vector3 (selectedCell.x+(float)xDist, 0F, selectedCell.y);
			}
			else if(selectedCellPos == RectanglePosition.LeftTop)
			{
				hoverTile.transform.position = new Vector3 (selectedCell.x, 0F, selectedCell.y+(float)yDist);
			}
			else
			{
				hoverTile.transform.position = new Vector3 (selectedCell.x+(float)xDist, 0F, selectedCell.y+(float)yDist);
			}
			
			// Scale the hover tile
			float xD = Mathf.Abs((float)xDist)+1F;
			float yD = Mathf.Abs((float)yDist)+1F;
			hoverTile.transform.localScale = new Vector3(xD,1F,yD);
			
			// Convert left bottom in vector two
			Vector2 leftBottomPosition = getTilePoints(hoverTile.transform.position);
			
			// Check if room purchase is valid
			if(isBlockSumZero(leftBottomPosition, xD, yD))
			{
				// Change color of hover tile to green
				hoverTileRenderer.material.SetColor("_Color", new Color(0F, 1.0F, 0F, 0.25F));
				
				// If mouse click, build the room
				if(Input.GetMouseButtonUp(0))
				{
					// If room size is minimum 2x2
					if(xD >= 2f && yD>=2f)
					{
						Vector2 lt = new Vector2(leftBottomPosition.x,leftBottomPosition.y+yD);
						Vector2 rb = new Vector2(leftBottomPosition.x+xD,leftBottomPosition.y);
						Vector2 rt = new Vector2(leftBottomPosition.x+xD,leftBottomPosition.y+yD);
						Room room = new Room(selectedRoomType,leftBottomPosition,lt,rb,rt,horizontalWall,verticalWall,transform);
						rooms.Add(room);
						
						// Update the floor map and room map
						setBlockValueFloorMap(leftBottomPosition, xD, yD, 2);
						setBlockValueRoomMap(leftBottomPosition, xD, yD, room.roomId);
						
						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				// Change color of hover tile to red
				hoverTileRenderer.material.SetColor("_Color", new Color(1.0F, 0F, 0F, 0.25F));
				return false;
			}
		}
		return false;
	}
	
	// Choose room and return
	Room chooseRoom()
	{
		// Room to return
		Room markedRoom = null;
		
		// Cast a ray from screen mouse position to 3d world space
		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit cameraRayHit;
		
		// Check if the casted ray collides only floor, then highlight the rectangular area
		if(Physics.Raycast(cameraRay, out cameraRayHit, 1000, 1 << 8))
		{
			// Move the hover tile to collison point
			Vector3 hitPoint = cameraRayHit.point;
			Vector2 mouseOverCell = getTilePoints(hitPoint);
			
			// Check if cell lies outside room, return null
			if(Maps.GetRoomMapValue(mouseOverCell)==0)
			{
				markedRoom = null;
				hoverTileRenderer.enabled = false;
				return markedRoom;
			}
			// If cell lies inside the room
			else
			{
				// Get the room Id
				int markedRoomId = Maps.GetRoomMapValue(mouseOverCell);
				
				// Search the room in rooms list
				foreach(Room r in rooms)
				{
					if(r.roomId == markedRoomId)
					{
						markedRoom = r;
						break;
					}
				}
				
				// Highlight and return the marked room
				hoverTile.transform.position = new Vector3 (markedRoom.leftBottom.x, 0F, markedRoom.leftBottom.y);
				hoverTile.transform.localScale = new Vector3(markedRoom.xLength,1F,markedRoom.yLength);
				hoverTileRenderer.material.SetColor("_Color", new Color(0.0F, 0F, 1.0F, 0.25F));
				hoverTileRenderer.enabled = true;
				return markedRoom;
			}
		}
		hoverTileRenderer.enabled = false;
		return markedRoom;
	}
	
	// Move game object to cell
	void moveToCell(GameObject moveObject, Vector2 moveCell)
	{
		moveObject.transform.position = new Vector3(moveCell.x*tileWidth, 0F, moveCell.y*tileHeight);
	}
	
	// Get the rectangle position of first cell
	RectanglePosition getRectPosition(int xDist, int yDist)
	{
		// Check the corner / quadrant of the cell and return
		if(xDist == 0)
		{
			if(yDist == 0)
			{
				return RectanglePosition.LeftBottom;
			}
			else if(yDist > 0)
			{
				return RectanglePosition.LeftBottom;
			}
			else
			{
				return RectanglePosition.LeftTop;
			}
		}
		if(yDist == 0)
		{
			if(xDist > 0)
			{
				return RectanglePosition.LeftBottom;
			}
			else
			{
				return RectanglePosition.RightBottom;
			}
		}
		if(xDist > 0)
		{
			if(yDist > 0)
			{
				return RectanglePosition.LeftBottom;
			}
			else
			{
				return RectanglePosition.LeftTop;
			}
		}
		else
		{
			if(yDist > 0)
			{
				return RectanglePosition.RightBottom;
			}
			else
			{
				return RectanglePosition.RightTop;
			}
		}	
	}
	
	// Check if the sum of values of given block is zero in floor map
	bool isBlockSumZero(Vector2 leftBottom, float hRange, float vRange)
	{
		int sum = 0;
		for(int i = 0; i<hRange; i++)
		{
			for(int j = 0; j<vRange; j++)
			{
				sum = sum + Maps.GetFloorMapValue(((int)leftBottom.x)+i, ((int)leftBottom.y)+j);
			}
		}
		if(sum == 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
	// Set block value for floor map
	void setBlockValueFloorMap(Vector2 leftBottom, float hRange, float vRange, int val)
	{
		for(int i = 0; i<hRange; i++)
		{
			for(int j = 0; j<vRange; j++)
			{
				Maps.SetFloorMapValue(((int)leftBottom.x)+i, ((int)leftBottom.y)+j, val);
			}
		}
	}
	
	// Set block value for room map
	void setBlockValueRoomMap(Vector2 leftBottom, float hRange, float vRange, int val)
	{
		for(int i = 0; i<hRange; i++)
		{
			for(int j = 0; j<vRange; j++)
			{
				Maps.SetRoomMapValue(((int)leftBottom.x)+i, ((int)leftBottom.y)+j, val);
			}
		}
	}
}