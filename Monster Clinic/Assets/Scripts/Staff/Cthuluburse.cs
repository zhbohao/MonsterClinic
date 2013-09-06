using UnityEngine;
using System.Collections;
using System;


public enum CthulLevel
{
	one = 1,
	two = 2,
	three = 3
}

[Serializable]
public class Cthuluburse : Staff {
	
	public CthulLevel level;
	
	public Cthuluburse()
	{
		base.staffType = StaffType.Cthuluburse;
		base.description = "I am a nurse";
		base.staffModel =(GameObject) GameObject.Instantiate(HospitalPrefabs.Cthuluburse, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
		level = CthulLevel.one;
	}
}
