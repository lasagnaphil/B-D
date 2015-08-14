using UnityEngine;
using System.Collections;

public class ObjectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;
		this.transform.position = new Vector3 (Mathf.Round (x), y, z);
	}
}
