using UnityEngine;
using System.Collections;

public class PhaseScript : MonoBehaviour {
	
	public enum Phase { Setting, Action };
	
	public Phase phase = Phase.Setting;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);
		if (hitCollider == null)
			return;

		if (phase == Phase.Setting) {
			if (Input.GetMouseButtonDown (1)) {
				if (hitCollider.gameObject.tag == "Block") {
					GameObject bomb = (GameObject)Instantiate (Resources.Load ("Bomb"));
					BombScript bombManager = bomb.GetComponent<BombScript>();
					bombManager.attach(hitCollider.transform);
				}
			}

			if (hitCollider.gameObject.tag == "Block") {
				BlockScript block = hitCollider.gameObject.GetComponent<BlockScript> ();
				if (block.type != BlockScript.BlockType.Steel) {
					if (Input.GetMouseButtonDown (0))
						block.IfMouseClick ();
					else
						block.IfMouseOver ();
				}
			} 
			else if (hitCollider.gameObject.tag == "Bomb") {
				float delta = 1 * Input.GetAxis("Mouse ScrollWheel");
				BombScript bomb = hitCollider.gameObject.GetComponent<BombScript>();
				bomb.time = Mathf.Clamp(bomb.time+delta,0,10);
			}

			if (Input.GetButtonDown("PhaseSwitch")) {
				phase = Phase.Action;
			}

		} else if (phase == Phase.Action) {
			if (hitCollider.gameObject.tag == "Block") {
				BlockScript block = hitCollider.gameObject.GetComponent<BlockScript> ();
				Vector3 blockPosition = block.transform.position;
				Vector3 playerPosition = GameObject.Find("Player").transform.position;
				float dist = Vector3.Distance(blockPosition, playerPosition);
				if (block.type == BlockScript.BlockType.Wood && dist < 1.5) {
					if (Input.GetMouseButtonDown (0))
						Destroy(block.gameObject);
					else
						block.IfMouseOver ();
				}
			}
		}
	}
}
