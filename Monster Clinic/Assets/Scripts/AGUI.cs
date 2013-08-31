using UnityEngine;
using System.Collections;

// AGUI class
public class AGUI : MonoBehaviour 
{
	// Public variables
	public GUISkin UISkin;
	public Texture createRoomIcon;
	public Texture receptionIcon;
	public Texture staffBreakIcon;
	public Texture patientWardIcon;
	public Texture diagnosticsIcon;
	public Texture slimeTreatmentIcon;
	public Texture shockTreatmentIcon;
	public Texture magicPotionIcon;
	public Texture physicalActivityIcon;
	
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
		if(GUI.Button(new Rect(Screen.width*0.005f, Screen.height*0.005f, 32f, 30f), createRoomIcon))
		{
			LevelManager.gameMode = Mode.RoomCreation;
			LevelManager.gameState = State.Purchase;
		}
	}
}
