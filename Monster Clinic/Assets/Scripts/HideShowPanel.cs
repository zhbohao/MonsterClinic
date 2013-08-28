using UnityEngine;
using System.Collections;

public class HideShowPanel : MonoBehaviour {
	
	public bool isOpen = true; 
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Toggle()
	{
		if(isOpen)
		{
			Hide();
			isOpen = !isOpen;
		}
		else
		{
			Show ();
			isOpen = !isOpen;
		}
	}
	
	
	void Hide()
	{
		/// hide the panel	
		
		Animation anim = GetComponent<Animation>();
		anim.animation["Close"].speed = 1;
		anim.Play("Close");
	}
	
	void Show()
	{
		/// show the panel
		Animation anim = GetComponent<Animation>();
		anim.animation["Close"].speed = -1;
		anim.animation["Close"].time = anim.animation["Close"].length;
		anim.animation.Play("Close");
	}
}
