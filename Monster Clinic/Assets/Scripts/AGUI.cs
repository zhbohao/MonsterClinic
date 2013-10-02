using UnityEngine;
using System.Collections;

// AGUI class
public class AGUI : MonoBehaviour 
{
	// Public variables
	public GUISkin UISkin;
	public Texture createRoomIcon;
	public Texture deleteRoomIcon;
	public Texture receptionIcon;
	public Texture staffBreakIcon;
	public Texture patientWardIcon;
	public Texture diagnosticsIcon;
	public Texture slimeTreatmentIcon;
	public Texture shockTreatmentIcon;
	public Texture magicPotionIcon;
	public Texture physicalActivityIcon;
	
	// Private variables
	private
	
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	void OnGUI()
	{
		GUI.skin = UISkin;
		if(!(LevelManager.gameMode == Mode.RoomCreation && LevelManager.gameState == State.Placement) && !(LevelManager.gameMode == Mode.RoomDeletion) && (LevelManager.gameTest == Test.None))
		{
			if(GUI.Button(new Rect(Screen.width*0.005f+(0*34f), Screen.height*0.005f, 32f, 30f), new GUIContent(createRoomIcon,"Create Room")))
			{
				LevelManager.gameMode = Mode.RoomCreation;
				LevelManager.gameState = State.Purchase;
			}
			
			if(GUI.Button(new Rect(Screen.width*0.005f+(1*34f), Screen.height*0.005f, 32f, 30f), new GUIContent(deleteRoomIcon,"Delete Room")))
			{
				LevelManager.gameMode = Mode.RoomDeletion;
				LevelManager.gameState = State.Choose;
			}
			
			if(LevelManager.gameMode == Mode.RoomCreation && LevelManager.gameState == State.Purchase)
			{
				if(GUI.Button(new Rect(Screen.width*0.005f+(0*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(receptionIcon,"Reception")))
				{
					LevelManager.selectedRoomType = RoomType.Reception;
					LevelManager.gameState = State.Placement;
				}
				
				if(GUI.Button(new Rect(Screen.width*0.005f+(1*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(staffBreakIcon,"Staff Break")))
				{
					LevelManager.selectedRoomType = RoomType.StaffBreak;
					LevelManager.gameState = State.Placement;
				}
				
				if(GUI.Button(new Rect(Screen.width*0.005f+(2*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(patientWardIcon,"Patient Ward")))
				{
					LevelManager.selectedRoomType = RoomType.PatientWard;
					LevelManager.gameState = State.Placement;
				}
				
				if(GUI.Button(new Rect(Screen.width*0.005f+(3*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(diagnosticsIcon,"Diagnostics")))
				{
					LevelManager.selectedRoomType = RoomType.Diagnostics;
					LevelManager.gameState = State.Placement;
				}
				if(GUI.Button(new Rect(Screen.width*0.005f+(4*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(slimeTreatmentIcon,"Slime Treatment")))
				{
					LevelManager.selectedRoomType = RoomType.SlimeTreatment;
					LevelManager.gameState = State.Placement;
				}
				if(GUI.Button(new Rect(Screen.width*0.005f+(5*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(shockTreatmentIcon,"Shock Treatment")))
				{
					LevelManager.selectedRoomType = RoomType.ShockTreatment;
					LevelManager.gameState = State.Placement;
				}
				if(GUI.Button(new Rect(Screen.width*0.005f+(6*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(magicPotionIcon,"Magic Potion")))
				{
					LevelManager.selectedRoomType = RoomType.MagicPotion;
					LevelManager.gameState = State.Placement;
				}
				if(GUI.Button(new Rect(Screen.width*0.005f+(7*34f), Screen.height*0.005f+(34f), 32f, 30f), new GUIContent(physicalActivityIcon,"Physical Activity")))
				{
					LevelManager.selectedRoomType = RoomType.PhysicalActivity;
					LevelManager.gameState = State.Placement;
				}
			}
			if(GUI.tooltip!="")
			{
				// Set the tool tip
				GUI.Label(new Rect(Screen.width*0.5f-64f, Screen.height*0.003f, 128f, 20f), GUI.tooltip);
			}
			GUI.tooltip = null;
		}
	}
}
