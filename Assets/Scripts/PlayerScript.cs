using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;
	public float moveForce = 365f;
	public float maxSpeed = 50f;
	public float jumpForce = 1000f;
	public Transform platformCheck;
	private bool onPlatform = false;
	//private Animator anim;
	private Rigidbody2D rb2d;

	public GameObject bulletPrefab;
	public int shootDirectionX = 1;
	public int shootDirectionY = 1;

	void Awake()
	{
		//anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		onPlatform = Physics2D.Linecast (transform.position, platformCheck.position, 1 << LayerMask.NameToLayer ("Platform"));
		if (Input.GetButtonDown("Jump") && onPlatform)
		{
			jump = true;
		}
		if (Input.GetButtonDown ("Fire1"))
		{
			GameObject bullet;
			bullet = Instantiate (Resources.Load("Bullet"), rb2d.transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<BulletScript>().directionY = shootDirectionY;
			bullet.GetComponent<BulletScript>().directionX = (Mathf.Abs(shootDirectionY) == 0)?
				shootDirectionX : 0;
		}
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		if (h > 0)
			shootDirectionX = 1;
		else if (h < 0)
			shootDirectionX = -1;
		float v = Input.GetAxis ("Vertical");
		if (v > 0)
			shootDirectionY = 1;
		else if (v < 0)
			shootDirectionY = -1;
		else
			shootDirectionY = 0;
		//anim.SetFloat ("Speed", Mathf.Abs (h));
		if (h * rb2d.velocity.x < maxSpeed)
			rb2d.AddForce (Vector2.right * h * moveForce);
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		if (jump) {
			//Animator.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
