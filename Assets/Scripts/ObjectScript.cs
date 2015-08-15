using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float x = this.transform.position.x;
		float y = this.transform.position.y;
		float z = this.transform.position.z;
		this.transform.position = new Vector3 (Mathf.Round (x), y, z);
	}

	public void toggleGravity(bool value) {
		Rigidbody2D body = this.GetComponent<Rigidbody2D> ();
		body.gravityScale = value ? 1 : 0;
	}

	 /// <summary>
	 /// Split this object into small connected parts.
	 /// </summary>
	 /// <param name="removedBlock">Removed block.</param>
	public void split (Transform removedBlock) {
		if (this == null)
			return;
		Transform[] raw = this.transform.GetComponentsInChildren<Transform> ();
		List<Transform> unmarked = new List<Transform>(raw);
		unmarked.Remove (this.transform);
		unmarked.Remove (removedBlock);
		
		while (unmarked.Count > 0) {
			List<Transform> connectedBlocks = getConnectedBlockFrom(unmarked);
			GameObject newObject = (GameObject)Instantiate(Resources.Load ("Object"));
			foreach(Transform block in connectedBlocks) {
				block.parent = newObject.transform;
			}
		}
		Object.Destroy (this.gameObject);
	}

	/// <summary>
	/// Gets the connected blocks from the children blocks.
	/// </summary>
	/// <returns>List of the connected blocks.</returns>
	/// <param name="children">List of the total blocks.</param>
	/// <param name="root">Root block of the connected blocks.</param>
	List<Transform> getConnectedBlockFrom(List<Transform> children, Transform root = null) {
		List<Transform> result = new List<Transform> ();

		if (root == null) {
			root = children[0];
		}
		children.Remove (root);
		result.Add (root);

		List<Transform> neighbors = getNeighbors (root, children);
		foreach (Transform neighbor in neighbors) {
			children.Remove(neighbor);
			result.Add(neighbor);
		}

		List<List<Transform>> recursiveResults = new List<List<Transform>> ();
		foreach (Transform neighbor in neighbors) {
			recursiveResults.Add(getConnectedBlockFrom(children, neighbor));
		}
		
		foreach (List<Transform> recursiveResult in recursiveResults) {
			result.AddRange(recursiveResult);
		}
		
		return result;
	}

	/// <summary>
	/// Gets the neighbors of the target block.
	/// </summary>
	/// <returns>List of the neighbors of the target block.</returns>
	/// <param name="target">Target block.</param>
	/// <param name="blocks">List of the total blocks.</param>
	List<Transform> getNeighbors(Transform target, List<Transform> blocks) {
		List<Transform> result = new List<Transform> ();
		foreach (Transform block in blocks) {
			Vector3 targetPosition = target.position;
			Vector3 blockPosition = block.position;
			float dist = Vector3.Distance(targetPosition, blockPosition);
			if (dist <= 1.1) {
				result.Add(block);
			}
		}
		return result;
	}
}
