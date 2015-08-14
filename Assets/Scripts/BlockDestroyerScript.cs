using UnityEngine;
using System.Collections;

public class BlockDestroyerScript : MonoBehaviour {

	private ObjectSplitterScript obj;

	// Use this for initialization
	void Start () {
		obj = transform.parent.GetComponent<ObjectSplitterScript>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;
		this.transform.position.Set (Mathf.Round (x), y, z);
	}

	void onDestroy() {
		obj.split (this.transform);
	}
}
