using UnityEngine;
using System.Collections;

public class BlockScript : MonoBehaviour {

	public int health = 100;

	void Update()
	{
		if (health <= 0) {
			DestroyObject(gameObject);
			Debug.Log ("block destroyed");
		}
	}
}
