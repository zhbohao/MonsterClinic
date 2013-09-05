using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StaffManager : MonoBehaviour {
	
	public GameObject Octodoctor;
	public GameObject Cthuluburse;
	public GameObject Yetitor;
	
	public List<Staff> staffList = new List<Staff>();
	
	
	// Use this for initialization
	void Start () {
		///ref the gameobjects to the prefab class
		Prefabs.Octodoctor = Octodoctor;
		Prefabs.Cthuluburse = Cthuluburse;
		Prefabs.Yetitor = Yetitor;
	}
	
	
	void OnGUI()
	{
		//just for a test buttons
		if(GUI.Button(new Rect(0,50,100,20),"Octodoctor"))
		{
			Staff staff = new Staff( StaffType.Octodoctor, "This is a Doctor");	
			staffList.Add(staff);
		}
		if(GUI.Button(new Rect(150,50,100,20),"Cthuluburse"))
		{
			Staff staff = new Staff( StaffType.Cthuluburse, "This is a nurse");	
			staffList.Add(staff);
		}
		if(GUI.Button(new Rect(300,50,100,20),"Yetitor"))
		{
			Staff staff = new Staff( StaffType.Yetitor, "This is a engineer");	
			staffList.Add(staff);
		}
	}
}
