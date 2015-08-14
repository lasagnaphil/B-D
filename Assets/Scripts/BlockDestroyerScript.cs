using UnityEngine;
using System.Collections;

public class BlockDestroyerScript : MonoBehaviour {

	private ObjectSplitterScript obj;

	// Use this for initialization
	void Start () {
		obj = transform.parent.GetComponent<ObjectSplitterScript>();
	}
	


	void onDestroy() {
		obj.split (this.transform);
	}
}
