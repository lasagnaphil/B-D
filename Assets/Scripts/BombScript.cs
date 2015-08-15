using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	public float time = 1.0f;
	public bool callUpdate = true;
	private PhaseScript phaseManager;

	// Use this for initialization
	void Start () {
		phaseManager = GameObject.Find("Player").GetComponent<PhaseScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (phaseManager.phase != PhaseScript.Phase.Action)
			return;
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
