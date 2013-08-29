using UnityEngine;
using System.Collections;

public class PanelManager : MonoBehaviour {
	
	public HideShowPanel standardGUI; /// normal game gui
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		///check which mode we are in to show the gui
		if(LevelManager.gameMode == Mode.RoomCreation)
		{
			standardGUI.Hide();	
		}
		else
		if(LevelManager.gameMode == Mode.None)
		{
			standardGUI.Show();	
		}
		
		//print (LevelManager.gameMode);
	}
}
