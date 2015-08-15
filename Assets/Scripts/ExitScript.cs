using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			GameObject.Find ("SceneManager").GetComponent<SceneManagerScript> ().LoadNextLevel ();
			Destroy(gameObject);
		}
	}
}
