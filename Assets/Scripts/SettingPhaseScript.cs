using UnityEngine;
using System.Collections;

public class SettingPhaseScript : MonoBehaviour {

	private PlayerScript player;

	// Use this for initialization
	void Start () {
		player = GetComponent<PlayerScript> ();
		Debug.Log (player.phase);
	}
	
	// Update is called once per frame
	void Update () {
		if (player.phase != PlayerScript.Phase.Setting)
			return;

		if (Input.GetMouseButtonDown (0)) {
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

			if (hitCollider == null) return;
			if (hitCollider.gameObject.tag != "Block") return;

			GameObject bomb = (GameObject)Instantiate(Resources.Load("Bomb"));
			bomb.transform.position = hitCollider.transform.position;
		}
	}
}
