using UnityEngine;
using System.Collections;

public class BlockDestroyTesterScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetMouseButtonDown (0)) {
			BlockDestroyerScript d = transform.GetComponent<BlockDestroyerScript>();
			d.destroyBlock();
		}
	}
}
