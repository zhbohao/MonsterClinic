using UnityEngine;
using System.Collections;
using System;

public enum YetitorLevel
{
	Brown = 1,
	Silverback = 2,
	Icemane = 3
}

[Serializable]
public class Yetitor : Staff {
	
	public YetitorLevel level;
	
	public Yetitor()
	{
		base.staffType = StaffType.Yetitor;	
		base.description = "I am a engineer";
		base.staffModel = staffModel =(GameObject) GameObject.Instantiate(HospitalPrefabs.Yetitor);
		level = YetitorLevel.Brown;
	}
	
}
