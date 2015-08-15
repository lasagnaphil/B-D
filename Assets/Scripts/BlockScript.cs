using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public enum BlockType {Wood, Stone, Steel};

	public int health = 100;
	public BlockType type;
	private ObjectScript obj;

	
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
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
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

	void Update()
	{
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 1f;
		GetComponent<SpriteRenderer> ().color = color;

		if (health <= 0) {
			DestroyObject(gameObject);
			Debug.Log ("block destroyed");
		}
	}
	
	void OnDestroy() {
		obj = transform.parent.GetComponent<ObjectScript>();
		obj.split (this.transform);
	}
}
