using UnityEngine;
using System.Collections;

public class DestroyParentGameObject : MonoBehaviour {

	void DestroyObject() {
		Destroy (transform.parent.gameObject);
	}
}
