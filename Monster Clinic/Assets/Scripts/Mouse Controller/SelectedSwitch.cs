using UnityEngine;
using System.Collections;

public class SelectedSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SetActive (false);
	}
	
	public void SetActive (bool b) {
		if ( b ) {
			//GetComponent<AIRTSPath>().enabled = true;
			GetComponentInChildren<Projector>().enabled = true;
		}
		else {
			//GetComponent<AIRTSPath>().enabled = false;
			GetComponentInChildren<Projector>().enabled = false;
		}
	}
}
