using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10.0f;
	public float initialScale;
	[HideInInspector] public int directionX = 1;
	[HideInInspector] public int directionY = 0;

	private Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		//Vector3 theScale = transform.localScale;
		//initialScale = theScale.x;
	}

	void FixedUpdate()
	{
		rb2d.velocity = new Vector2 (speed * directionX, speed * directionY);
		//Vector3 theScale = transform.localScale;
		//theScale.x = direction * initialScale;
		//transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Block") {
			Transform block = col.transform;
			block.GetComponent<BlockScript>().attachedBomb.GetComponent<BombScript>().callUpdate = true;
			Destroy (gameObject);
		}
	}
}
