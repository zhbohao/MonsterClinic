using UnityEngine;
using System.Collections;

// Level manager class
public class LevelManager : MonoBehaviour 
{
	// Public static variables
	public static Mode gameMode = Mode.None;
	public static State gameState = State.None;
	
	// Public variables
	public float floorWidth = 100f;
	public float floorHeight = 100f;
	public float tileWidth = 1f;
	public float tileHeight = 1f;
	public GameObject hoverTile;
	public GameObject IsoCamera;
	public GameObject PresCamera;
	
	// Private Variables
	private bool IsoCam = true;
	private int horizontalTiles;
	private int verticalTiles;
	private MeshRenderer hoverTileRenderer;
	private RoomType selectedRoomType;
	private Vector2 selectedCell;
	/// <summary>
	/// Array floor map is used to keep track of map
	/// -1 for empty cell outside room
	/// 0 for non-empty cell outside room
	/// 1 for empty cell inside room
	/// 2 for non-empty cell inside room
	/// 3 for walls
	/// 4 for doors
	/// +Integar.x for room ID
	/// </summary>
	private float [,] floorMap = new float[100,100];
	
	// Use this for initialization
	void Start () 
	{
		// Calculate number of tiles
		horizontalTiles = (int)(floorWidth/tileWidth);
		verticalTiles = (int)(floorHeight/tileHeight);
		
		// Initialize floor map
		for(int i=0; i<horizontalTiles; i++)
		{
			for(int j=0; j<verticalTiles; j++)
			{
				floorMap[i,j] = -1f;
			}
		}
		
		floorMap[1,1] = 1;
		
		// Instantiate tile for hover
		hoverTile = (GameObject) Instantiate(hoverTile);
		hoverTile.GetComponentInChildren<MeshRenderer>().enabled = false;
		
		// Create a mesh renderer component
		hoverTileRenderer = hoverTile.GetComponentInChildren<MeshRenderer>();
		
		// Initialize other variables
		selectedRoomType = RoomType.None;
		selectedCell = new Vector2(-1,-1);
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
		   // If state is purchase
		   if(gameState == State.Purchase)
		   {
				selectedRoomType = RoomType.Type1;
				gameState = State.Placement;
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
					getRoomPlacement();
				}
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
				
				// If empty cell outside room
				if(floorMap[(int)hitPoint.x, (int)hitPoint.y]==-1f)
				{
					// Set the color of tile to green
					hoverTileRenderer.material.SetColor ("_Color", new Color(0F, 1.0F, 0F, 0.25F));
					if(Input.GetMouseButton(0))
					{
						selectedCell = new Vector2(hitPoint.x, hitPoint.y);
						//print(hitPoint+":"+floorMap[(int)hitPoint.x, (int)hitPoint.y]);
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
	void getRoomPlacement()
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
			hoverTileRenderer.material.SetColor("_Color", new Color(0F, 1.0F, 0F, 0.25F));	
			
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
		}
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
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width*0.025f, Screen.height*0.025f, Screen.width*0.1f, Screen.height*0.05f), "Room"))
		{
			gameMode = Mode.RoomCreation;
			gameState = State.Purchase;
		}
	}
}