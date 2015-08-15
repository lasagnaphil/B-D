using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public float time = 1.0f;
	public bool callUpdate = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (callUpdate) {
			time -= Time.deltaTime;
			if (time <= 0) {
				Explode ();
				callUpdate = false;
			}
		}
	}

	void Explode() {
		transform.GetChild (0).GetComponent<BombExplosionScript> ().isActive = true;
	}
}
