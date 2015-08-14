using UnityEngine;
using System.Collections;

public class BombExplosionScript : MonoBehaviour {

	public bool isActive = false;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block" && isActive) {
			col.gameObject.GetComponent<BlockScript>().health = 0;
		}
	}

	void Update()
	{
		if (isActive) {
			transform.parent.GetComponent<Rigidbody2D>().isKinematic = true;
			DestroyObject();
		}
	}

	IEnumerator DestroyObject()
	{
		isActive = false;
		yield return new WaitForSeconds (0.2f);
		Destroy (gameObject);
		Destroy (transform.parent.gameObject);
	}
	
}
