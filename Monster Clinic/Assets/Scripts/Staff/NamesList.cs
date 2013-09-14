using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class NamesList : MonoBehaviour {
	
	public static List<string> nameList = new List<string>();
    public TextAsset dateFile;
     
    void Awake () 
	{
		
	  	string[] tempList = dateFile.text.Split('\n');
		for(int a = 0; a < tempList.Length; a++)
		{
			nameList.Add (tempList[a]);	
		}
    }
	
	public static string GetRandomName()
	{
		int n = Random.Range(0, nameList.Count - 1);
		string name = nameList[n];
		RemoveName(name);
		return (name);
	}
	
	static void RemoveName(string n)
	{
		nameList.Remove(n);	
	}
}
