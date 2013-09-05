using UnityEngine;
using System.Collections;
using System;


public enum StaffType
{
	Octodoctor,
	Cthuluburse,
	Yetitor
}

[Serializable]
public class Staff  {
	
	//the staff propities
	public StaffType staffType;
	public string description;
	public int level;
	public int monthWage;
	public int experience = 0;
	
	
	public GameObject staffModel;
	public Texture2D photo;
	

	
	public Staff(StaffType type, string desc )
	{
		staffType = type;
		description = desc;
		
		switch(staffType)
		{
		case StaffType.Cthuluburse :
			staffModel =(GameObject) GameObject.Instantiate(Prefabs.Cthuluburse);
			break;
		case StaffType.Octodoctor:
			staffModel = (GameObject) GameObject.Instantiate(Prefabs.Octodoctor);
			break;
		case StaffType.Yetitor:
			staffModel =(GameObject) GameObject.Instantiate(Prefabs.Yetitor);
			break;
		}
		
		level =	UnityEngine.Random.Range(1,4);//random level to start
		
	}
	

}
