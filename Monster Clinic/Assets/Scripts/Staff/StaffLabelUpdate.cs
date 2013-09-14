using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaffLabelUpdate : MonoBehaviour {
	
	public UILabel glitter;
	public UILabel name;
	public UILabel wage;
	public UILabel level;
	
	public void UpdateGlitter(int g)
	{
		glitter.text = g.ToString();
	}
	
	public void UpdateName(string n)
	{
		name.text = n;
	}
	
	public void UpdateWage(int w)
	{
		wage.text = w.ToString();
	}
	
	public void UpdateLevel <T> (T Level)
	{
		level.text = Level.ToString(); 	
	}
	
}
