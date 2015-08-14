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

	void Update()
	{
		if (health <= 0) {
			DestroyObject(gameObject);
			Debug.Log ("block destroyed");
		}
	}
	
	void onDestroy() {
		obj.split (this.transform);
	}
}
