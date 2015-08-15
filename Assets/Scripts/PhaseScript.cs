using UnityEngine;
using System.Collections;

public class PhaseScript : MonoBehaviour {
	
	public enum Phase { Setting, Action };
	
	public Phase phase = Phase.Setting;

	// number of items able to use
	public int bombNum = 3;
	public int replaceNum = 2;
	public int createNum = 1;

	public int score = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);
		

		if (phase == Phase.Setting) {
			if (hitCollider == null) {
				if( Input.GetMouseButtonDown(0) && createNum > 0) {
					createNum--;
					GameObject blockStone = (GameObject)Instantiate (Resources.Load ("BlockStone"));
					blockStone.transform.position = new Vector3(Mathf.Round(mousePosition.x), mousePosition.y, 1);
					GameObject obj = (GameObject)Instantiate (Resources.Load ("Object"));
					blockStone.transform.parent = obj.transform;
				}
				return;
			}

			if (Input.GetMouseButtonDown (1)) {
				if (hitCollider.gameObject.tag == "Block" && bombNum > 0) {
					bombNum--;
					GameObject bomb = (GameObject)Instantiate (Resources.Load ("Bomb"));
					BombScript bombManager = bomb.GetComponent<BombScript>();
					bombManager.attach(hitCollider.transform);
				}
			}

			if (hitCollider.gameObject.tag == "Block" && replaceNum > 0) {
				BlockScript block = hitCollider.gameObject.GetComponent<BlockScript> ();
				if (block.type != BlockScript.BlockType.Steel) {
					if (Input.GetMouseButtonDown (0)) {
						replaceNum--;
						block.IfMouseClick ();
					}
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
			if (hitCollider == null)
				return;
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
