using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {


	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;
	public float moveForce = 365f;
	public float maxSpeed = 50f;
	public float jumpForce = 1000f;
	public Transform platformCheck;
	public bool onPlatform = false;
	//private Animator anim;
	private Rigidbody2D rb2d;

	public int shootDirectionX = 1;
	public int shootDirectionY = 1;
	private PhaseScript phaseManager;

	void Awake()
	{
		//anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		phaseManager = GetComponent<PhaseScript> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		onPlatform = Physics2D.Linecast (transform.position, platformCheck.position, 1 << LayerMask.NameToLayer ("Platform"));
		if (Input.GetButtonDown("Jump") && onPlatform)
		{
			jump = true;
		}
		if (phaseManager.phase == PhaseScript.Phase.Action && Input.GetButtonDown ("Fire1"))
		{
			Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
			Vector2 delta = mousePosition - playerPosition;
			if (delta.x > delta.y && delta.y > -delta.x ) {
				shootDirectionX = 1;
				shootDirectionY = 0;
			} else if (delta.y > delta.x && delta.x > -delta.y ) {
				shootDirectionX = 0;
				shootDirectionY = 1;
			} else if (-delta.x > delta.y && delta.y > delta.x ) {
				shootDirectionX = -1;
				shootDirectionY = 0;
			} else if (-delta.y > delta.x && delta.x > delta.y ) {
				shootDirectionX = 0;
				shootDirectionY = -1;
			}
			GameObject bullet = Instantiate (Resources.Load("Bullet"), rb2d.transform.position, Quaternion.identity) as GameObject;
			bullet.GetComponent<BulletScript>().directionX = shootDirectionX;
			bullet.GetComponent<BulletScript>().directionY = shootDirectionY;
			bullet.transform.Rotate(0, 0, 180/Mathf.PI*Mathf.Atan2 (shootDirectionY, shootDirectionX));
		}
	}

	void FixedUpdate ()
	{
		if (phaseManager.phase != PhaseScript.Phase.Action)
			return;

		float h = Input.GetAxis ("Horizontal");
		//float v = Input.GetAxis ("Vertical");
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

	public void die(string causeOfDeath) {
		Destroy (gameObject);
		Application.LoadLevel("bdgameover");
	}
}
