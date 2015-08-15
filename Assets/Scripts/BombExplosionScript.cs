using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BombExplosionScript : MonoBehaviour {

	public bool isActive = false;
	private List<GameObject> collidedBlock = new List<GameObject>();

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block") {
			collidedBlock.Add(col.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block") {
			collidedBlock.Remove(col.gameObject);
		}
	}

	void FixedUpdate()
	{
		if (isActive) {
			DestroyObject();
		}
	}

	void DestroyObject()
	{
		Vector3 bombPosition = transform.parent.position;
		float dist;
		foreach (GameObject block in collidedBlock) {
			if (block == null)
				continue;
			if (block.GetComponent<BlockScript>().type == BlockScript.BlockType.Steel)
				continue;
			Vector3 blockPosition = block.transform.position;
			dist = Vector3.Distance(blockPosition, bombPosition);
			if (dist > 1.8)	continue;
			Destroy(block);
		}
		Transform player = GameObject.Find ("Player").transform;
		Vector3 playerPosition = player.position;
		dist = Vector3.Distance (bombPosition, playerPosition);
		if (dist < 1.8) {
			player.GetComponent<PlayerScript>().die("Bomb");
		}
		Destroy (transform.parent.gameObject);
	}
	
}
