using UnityEngine;
using System.Collections;

public class HideShowPanel : MonoBehaviour {
	
	private bool isOpen = true;
	
	public void Hide()
	{
		/// hide the panel	
		if(!isOpen) return;
		
		Animation anim = GetComponent<Animation>();
		anim.Play("Close");
		isOpen = false;
	}
	
	public void Show()
	{
		if(isOpen) return;
		
		/// show the panel
		Animation anim = GetComponent<Animation>();
		anim.Play("Open");
		isOpen = true;
	}
}
