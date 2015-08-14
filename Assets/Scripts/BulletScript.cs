using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float speed = 10.0f;
	public float initialScale;
	[HideInInspector] public int direction = 1;

	private Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = GetComponent<Rigidbody2D> ();
		//Vector3 theScale = transform.localScale;
		//initialScale = theScale.x;
	}

	void Update()
	{
		rb2d.velocity = new Vector2 (speed * direction, 0);
		//Vector3 theScale = transform.localScale;
		//theScale.x = direction * initialScale;
		//transform.localScale = theScale;
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Block") {
			col.gameObject.GetComponent<BlockScript>().health -= 100;
			Destroy (gameObject);
		}
	}
}
