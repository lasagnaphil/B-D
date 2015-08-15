using UnityEngine;
using System.IO;
using System.Collections;
using SimpleJSON;

public class SceneManagerScript : MonoBehaviour {

	public GameObject player;
	public int levelNum = 1;

	void Start () {
		DontDestroyOnLoad (GameObject.Find ("Canvas"));
		DontDestroyOnLoad (GameObject.Find ("Player"));
		DontDestroyOnLoad (GameObject.Find ("UIManager"));
		DontDestroyOnLoad (GameObject.Find ("BombCanvas"));
		DontDestroyOnLoad (gameObject);
		levelNum = int.Parse(File.ReadAllText ("Assets/data.txt"));
		if (levelNum != 1)
			LoadLevel (levelNum);
	}

	void Update() {
		if (Application.loadedLevelName == "bdgameover") {
			Destroy (GameObject.Find ("Canvas"));
			Destroy (GameObject.Find ("Player"));
			Destroy (GameObject.Find ("UIManager"));
			Destroy (GameObject.Find ("BombCanvas"));
			Destroy (gameObject);
		}
	}

	public void LoadNextLevel() {
		player.transform.position = new Vector2(-5.0f, 1.0f);
		levelNum++;
		// write the current level into text
		File.WriteAllText ("Assets/data.txt", levelNum.ToString ());
		LoadLevel (levelNum);
	}

	public void LoadLevel(int i) {
		Application.LoadLevel ("bdgame" + i.ToString ());
		JSONNode sceneData = JSON.Parse (File.ReadAllText ("Assets/sceneData.json"));
		GameObject.Find ("Player").GetComponent<PhaseScript>().bombNumMax = sceneData["scenes"][i-1]["bomb"].AsInt;
		GameObject.Find ("Player").GetComponent<PhaseScript>().replaceWoodNumMax = sceneData["scenes"][i-1]["replaceWood"].AsInt;
		GameObject.Find ("Player").GetComponent<PhaseScript>().replaceSteelNumMax = sceneData["scenes"][i-1]["replaceSteel"].AsInt;
		GameObject.Find ("Player").GetComponent<PhaseScript>().createNumMax = sceneData["scenes"][i-1]["create"].AsInt;
	}

}
