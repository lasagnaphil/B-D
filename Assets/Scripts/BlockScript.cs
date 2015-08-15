using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public enum BlockType {Wood, Stone, Steel};

	public int health = 100;
	public BlockType type;
	private ObjectScript obj;
	private bool highlighted = false;

	
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

	public void IfMouseOver() {
		// change alpha value of block
		highlighted = true;
		//Color color = GetComponent<SpriteRenderer> ().color;
		//color.a = 0.5f;
		//GetComponent<SpriteRenderer> ().color = color;
	}
	
	public void IfMouseClick() {
		GetComponent<BoxCollider2D>().enabled = false;
		// create wood block
		GameObject woodBlock;
		woodBlock = Instantiate(Resources.Load("BlockWood"), transform.position, Quaternion.identity) as GameObject;
		woodBlock.transform.parent = transform.parent;
		// destroy old block
		Destroy(gameObject);
	}

	void FixedUpdate()
	{
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = highlighted? 0.5f : 1f;
		GetComponent<SpriteRenderer> ().color = color;
		if (highlighted)
			highlighted = false;
		
		if (health <= 0) {
			DestroyObject(gameObject);
			//Debug.Log ("block destroyed");
		}
	}
	
	void OnDestroy() {
		obj = transform.parent.GetComponent<ObjectScript>();
		obj.split (this.transform);
	}
}
