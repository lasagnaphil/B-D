using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public enum BlockType {Wood, Stone, Steel};

	public int health = 100;
	public BlockType type;
	private ObjectScript obj;

	public GameObject woodBlockPrefab;
	
	// Use this for initialization
	void Start () {
		obj = transform.parent.GetComponent<ObjectScript>();

		switch (type) {
		case BlockType.Steel:
			obj.toggleGravity(false);
			break;
		case BlockType.Wood:
			//Can be destroyed by hand
		case BlockType.Stone:
			//Can be destroyed by bomb
			//Applying gravity
			break;
		}
	}

	void IfMouseOver() {
		// change alpha value of block
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
	}
	
	void IfMouseClick() {
		GetComponent<BoxCollider2D>().enabled = false;
		// create wood block
		GameObject woodBlock;
		woodBlock = Instantiate(woodBlockPrefab, transform.position, Quaternion.identity) as GameObject;
		woodBlock.transform.parent = transform.parent;
		// destroy old block
		Destroy(gameObject);
	}

	void Update()
	{
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 1f;
		GetComponent<SpriteRenderer> ().color = color;

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Collider2D hitCollider = Physics2D.OverlapPoint (mousePosition);
		if (hitCollider.gameObject.tag == "Block") {
			if(Input.GetMouseButtonDown (0))
				IfMouseClick();
			else IfMouseOver();
		}

		if (health <= 0) {
			DestroyObject(gameObject);
			Debug.Log ("block destroyed");
		}
	}
	
	void OnDestroy() {
		obj.split (this.transform);
	}
}
