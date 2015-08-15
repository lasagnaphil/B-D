using UnityEngine;
using System.Collections;

public class HeadCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Block")
			return;
		if (col.attachedRigidbody.velocity.magnitude < 0.1)
			return;

		transform.parent.gameObject.GetComponent<PlayerScript> ().die ("FallingBlock");
	}
}
