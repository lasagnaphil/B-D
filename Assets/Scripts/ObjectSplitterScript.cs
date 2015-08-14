using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSplitterScript : MonoBehaviour {
	
	private Dictionary<GameObject, int> flag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void split (Transform removedBlock) {
		Transform[] raw = this.transform.GetComponentsInChildren<Transform> ();
		List<Transform> unmarked = new List<Transform>(raw);
		unmarked.Remove (this.transform);
		//Debug.Log ("-2 : " + raw.Length);
		unmarked.Remove (removedBlock);
		//Debug.Log ("-1 : " + unmarked.Count);

		while (unmarked.Count > 0) {
			List<Transform> connectedBlocks = getConnectedBlockFrom(unmarked);
			GameObject newObject = (GameObject)Instantiate(Resources.Load ("Prefabs/Object"));
			foreach(Transform block in connectedBlocks) {
				block.parent = newObject.transform;
			}
		}
		Object.Destroy (this.gameObject);
	}

	List<Transform> getConnectedBlockFrom(List<Transform> children, Transform root = null) {
		List<Transform> result = new List<Transform> ();
		//Debug.Log ("0 : " + children.Count);
		if (root == null) {
			root = children[0];
		}
		children.Remove (root);
		result.Add (root);
		//Debug.Log ("1 : " + children.Count);
		List<Transform> neighbors = getNeighbors (root, children);
		foreach (Transform neighbor in neighbors) {
			children.Remove(neighbor);
			result.Add(neighbor);
		}
		//Debug.Log ("2 : " + children.Count);
		List<List<Transform>> recursiveResults = new List<List<Transform>> ();
		foreach (Transform neighbor in neighbors) {
			recursiveResults.Add(getConnectedBlockFrom(children, neighbor));
		}

		foreach (List<Transform> recursiveResult in recursiveResults) {
			result.AddRange(recursiveResult);
		}

		return result;
	}

	List<Transform> getNeighbors(Transform target, List<Transform> blocks) {
		List<Transform> result = new List<Transform> ();
		foreach (Transform block in blocks) {
			Vector3 targetPosition = target.position;
			Vector3 blockPosition = block.position;
			float dist = Vector3.Distance(targetPosition, blockPosition);
			if (dist <= 1.1) {
				//Debug.Log(dist);
				result.Add(block);
			}
		}
		//Debug.Log ("end");
		return result;
	}
}
