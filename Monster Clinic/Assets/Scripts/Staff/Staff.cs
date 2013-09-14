using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public enum StaffType
{
	None,
	Octodoctor,
	Cthuluburse,
	Yetitor
}

[Serializable]
public class Staff  {
	
	//the staff propities
	public StaffType staffType;
	public string description;
	public int monthWage;
	public int experience = 0;
	public GameObject staffModel;
	public Texture2D photo;
	public string name;
	public int cost;
	
	public Staff()
	{
		name = NamesList.GetRandomName();
		monthWage = 30;
		cost = 40;
		description = "hey hey";
	}
}
