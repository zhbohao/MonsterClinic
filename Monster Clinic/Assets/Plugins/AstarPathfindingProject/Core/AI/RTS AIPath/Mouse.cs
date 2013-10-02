using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	RaycastHit hit;
	
	public Vector3 rightClickPoint;
	
	public static ArrayList currentlySelectedUnits = new ArrayList(); // of gameObject
	public static ArrayList unitsOnScreen = new ArrayList(); 			// of gameObject
	public static ArrayList unitsInDrag = new ArrayList();			// of gameObject
	private bool finishedDragOnThisFrame;
	public static bool finishedSelectionOnThisFrame;
	private bool startedDrag;
	
	public GUIStyle MouseDragSkin;
	
	public GameObject Target;
	
	private static Vector3 mouseDownPoint;
	private static Vector3 currentMousePoint;	// in world space
	
	public static bool userIsDragging;
	private static float timeLimitBeforeDeclareDrag = 1f;
	private static float timeLeftBeforeDeclareDrag;
	private static Vector2 mouseDragStart;
	
	private static float clickDragZone = 1.3f;
	
	// GUI
	private float boxWidth;
	private float boxHeight;
	private float boxTop;
	private float boxLeft;
	public static Vector2 boxStart;
	public static Vector2 boxFinish;
	
	void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			currentMousePoint = hit.point;
			
			// store point at mouse button down
			if (Input.GetMouseButtonDown (0)) {
				mouseDownPoint = hit.point;
				timeLeftBeforeDeclareDrag = timeLimitBeforeDeclareDrag;
				mouseDragStart = Input.mousePosition;
				startedDrag = true;
			}
			else if(Input.GetMouseButton(0)){
				// if the user is not dragging , lets do the tests
				if(!userIsDragging){
					timeLeftBeforeDeclareDrag -= Time.deltaTime;
					
//					if (timeLeftBeforeDeclareDrag <= 0f || UserDraggingByPosition(mouseDragStart, Input.mousePosition))
						userIsDragging = true;
				}
			}
			else if (Input.GetMouseButtonUp(0)) {
				if (userIsDragging)
					finishedDragOnThisFrame = true;
				userIsDragging = false;
				finishedSelectionOnThisFrame = true;
			}
			
			// deselect if start drag, not click!
//			if (!Common.ShiftKeysDown()) {
				
//			}
		}
	}
}

