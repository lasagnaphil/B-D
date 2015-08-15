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

	void Update()
	{
		if (isActive) {
			DestroyObject();
		}
	}

	void DestroyObject()
	{
		foreach (GameObject block in collidedBlock) {
			if (block == null)
				continue;
			if (block.GetComponent<BlockScript>().type == BlockScript.BlockType.Steel)
				continue;
			Destroy(block);
		}
		Destroy (transform.parent.gameObject);
	}
	
}
