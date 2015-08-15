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
			transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
			DestroyObject();
		}
	}

	void DestroyObject()
	{
		foreach (GameObject block in collidedBlock) {
			Destroy(block);
		}
		Destroy (transform.parent.gameObject);
	}
	
}
