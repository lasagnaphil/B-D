using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	[Range(0, 10)] public float time = 0f;
	public bool callUpdate = false;
	private PhaseScript phaseManager;
	private Transform player;
	private Transform attachedBlock;

	// Use this for initialization
	void Start () {
		phaseManager = GameObject.Find("Player").GetComponent<PhaseScript>();
		player = GameObject.Find("Player").GetComponent<Transform>();
	}

	public void attach(Transform block) {
		attachedBlock = block;
		transform.position = attachedBlock.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (phaseManager.phase != PhaseScript.Phase.Action)
			return;
		//GetComponentInChildren<BoxCollider2D> ().enabled = true;
		transform.position = attachedBlock.position;
		/*Vector3 playerPosition = player.position;
		Vector3 bombPosition = transform.position;
		float dist = Vector3.Distance (playerPosition, bombPosition);
		if (dist < 2.2)
			callUpdate = true;*/
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
