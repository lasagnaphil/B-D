using UnityEngine;
using System.IO;
using System.Collections;
using SimpleJSON;

public class PhaseScript : MonoBehaviour {
	
	public enum Phase { Setting, Action };
	
	public Phase phase = Phase.Setting;

	// number of items able to use
	[HideInInspector] public int bombNum;
	[HideInInspector] public int replaceWoodNum;
	[HideInInspector] public int replaceSteelNum;
	[HideInInspector] public int createNum;

	[HideInInspector] public int bombNumMax;
	[HideInInspector] public int replaceWoodNumMax;
	[HideInInspector] public int replaceSteelNumMax;
	[HideInInspector] public int createNumMax;

	public int score = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);
		

		if (phase == Phase.Setting) {
			if (Input.GetButtonDown("PhaseSwitch")) {
				phase = Phase.Action;
			}
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

			//////////////////////bomb
			if (Input.GetMouseButtonDown (2)) {
				if (hitCollider.gameObject.tag == "Block" && bombNum > 0) {
					bombNum--;
					GameObject bomb = (GameObject)Instantiate (Resources.Load ("Bomb"));
					BombScript bombManager = bomb.GetComponent<BombScript>();
					bombManager.attach(hitCollider.transform);
				}
			}

			/////////////////////////wood/steal
			if (hitCollider.gameObject.tag == "Block") {
				BlockScript block = hitCollider.gameObject.GetComponent<BlockScript> ();
				if (block.type == BlockScript.BlockType.Stone) {
					if (replaceWoodNum > 0 || replaceSteelNum > 0) {
						if (Input.GetMouseButtonDown (0) && replaceWoodNum>0 ) {
							replaceWoodNum--;
							block.IfMouseClick (BlockScript.BlockType.Wood);
						} else if (Input.GetMouseButtonDown(1) && replaceSteelNum > 0) {
							replaceSteelNum--;
							block.IfMouseClick (BlockScript.BlockType.Steel);
						} else {
							block.IfMouseOver ();
						}
					}
				}
			/////////////////////////bomb timer
			} else if (hitCollider.gameObject.tag == "Bomb") {
				float delta = 1 * Input.GetAxis("Mouse ScrollWheel");
				BombScript bomb = hitCollider.gameObject.GetComponent<BombScript>();
				if (delta < 0 && bomb.time == 0) {
					Destroy (bomb.gameObject);
					bombNum++;
				} else {
					bomb.time = Mathf.Clamp(bomb.time+delta,0,10);
				}
			}

		} else if (phase == Phase.Action) {
			if (Input.GetButtonDown("Suicide")){
				GameObject.Find("Player").GetComponent<PlayerScript>().die("Suicide");
			}
			if (hitCollider == null)
				return;
			if (hitCollider.gameObject.tag == "Block") {
				BlockScript block = hitCollider.gameObject.GetComponent<BlockScript> ();
				GameObject playerObject = GameObject.Find("Player");
				Vector3 blockPosition = block.transform.position;
				Vector3 playerPosition = playerObject.transform.position;
				float dist = Vector3.Distance(blockPosition, playerPosition);
				PlayerScript player = playerObject.GetComponent<PlayerScript>();
				if (block.type == BlockScript.BlockType.Wood && dist < 1.2 && player.onPlatform) {
					if (Input.GetMouseButtonDown (1))
						Destroy(block.gameObject);
					else
						block.IfMouseOver ();
				}
			}
		}
	}
}
