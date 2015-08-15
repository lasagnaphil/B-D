using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombScript : MonoBehaviour {

	[Range(0, 10)] public float time = 0f;
	public bool callUpdate = false;
	private PhaseScript phaseManager;
<<<<<<< HEAD
	private Transform player;
=======
>>>>>>> origin/game-ui
	private Transform attachedBlock;
	//private GameObject bombText;
	public GUIStyle guiStyle;

	// Use this for initialization
	void Start () {
		phaseManager = GameObject.Find("Player").GetComponent<PhaseScript>();
<<<<<<< HEAD
		player = GameObject.Find("Player").GetComponent<Transform>();
=======
		/*bombText = Instantiate (Resources.Load ("BombText"), transform.position, Quaternion.identity) as GameObject;
		bombText.transform.SetParent (GameObject.Find ("BombCanvas").transform);
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint (Camera.main, transform.position);
		bombText.GetComponent<RectTransform>().anchoredPosition = screenPoint;*/
>>>>>>> origin/game-ui
	}

	void OnGUI () {
		Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint (Camera.main, transform.position);
		screenPoint.y = Screen.height - screenPoint.y;
		GUI.Label (new Rect (screenPoint.x-25, screenPoint.y-10, 50, 20), (Mathf.Round(10*time) / 10).ToString() + "s", guiStyle);
	}
	public void attach(Transform block) {
		attachedBlock = block;
		transform.position = attachedBlock.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//bombText.GetComponent<Text> ().text = time.ToString () + "s";
		transform.position = attachedBlock.position;
		if (phaseManager.phase != PhaseScript.Phase.Action)
			return;
		//GetComponentInChildren<BoxCollider2D> ().enabled = true;
		/*Vector3 playerPosition = player.position;
		Vector3 bombPosition = transform.position;
		float dist = Vector3.Distance (playerPosition, bombPosition);
		if (dist < 2.2)
			callUpdate = true;*/
		if (callUpdate) {
			time -= Time.deltaTime;
			if (time <= 0) {
				Explode ();
				callUpdate = false;
			}
		}
	}

	void Explode() {
		transform.GetChild (0).GetComponent<BombExplosionScript> ().isActive = true;
	}
}
