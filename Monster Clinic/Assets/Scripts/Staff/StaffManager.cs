using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StaffManager : MonoBehaviour {
	
	public GameObject octodoctor;
	public GameObject cthuluburse;
	public GameObject yetitor;
	
	public List<Octodoctor> octodoctorList = new List<Octodoctor>();
	public List<Cthuluburse> ctuluburseList = new List<Cthuluburse>();
	public List<Yetitor> yetitorList = new List<Yetitor>();
	
	
	// Use this for initialization
	void Start () {
		///ref the gameobjects to the prefab class
		HospitalPrefabs.Octodoctor = octodoctor;
		HospitalPrefabs.Cthuluburse = cthuluburse;
		HospitalPrefabs.Yetitor = yetitor;
	}
	
	
	void OnGUI()
	{
		//just for a test buttons
		if(GUI.Button(new Rect(0,50,100,20),"Octodoctor"))
		{
			Octodoctor staffMember = new Octodoctor();	
			octodoctorList.Add(staffMember);/// we shoulded add this untill we have placed him down
		}
		if(GUI.Button(new Rect(150,50,100,20),"Cthuluburse"))
		{
			Cthuluburse staffMember = new Cthuluburse();
			ctuluburseList.Add (staffMember);/// we shoulded add this untill we have placed him down
		}
		if(GUI.Button(new Rect(300,50,100,20),"Yetitor"))
		{
			Yetitor staffMember = new Yetitor();
			yetitorList.Add (staffMember);/// we shoulded add this untill we have placed him down
		
		}
	}
}
