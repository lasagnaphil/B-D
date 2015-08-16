using UnityEngine;
using System.Collections;

public class ExitTutorialScript : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player") {
			GameObject.Find ("TutorialManager").GetComponent<TutorialManagerScript> ().LoadNextLevel ();
			Destroy(gameObject);
		}
	}
}
