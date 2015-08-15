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
		if (phase != Phase.Setting)
			return;
		
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
		if (hitCollider == null) return;

		if (Input.GetMouseButtonDown (1)) {
			if (hitCollider.gameObject.tag == "Block") {
				GameObject bomb = (GameObject)Instantiate(Resources.Load("Bomb"));
				bomb.transform.position = hitCollider.transform.position;
			}
		}

		if (hitCollider.gameObject.tag == "Block") {
			BlockScript block = hitCollider.gameObject.GetComponent<BlockScript>();
			if (block.type != BlockScript.BlockType.Steel) {
				if(Input.GetMouseButtonDown (0))
					block.IfMouseClick();
				else block.IfMouseOver();
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			phase = Phase.Action;
		}
	}
}
