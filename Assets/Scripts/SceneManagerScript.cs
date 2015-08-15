using UnityEngine;
using System.IO;
using System.Collections;
using SimpleJSON;

public class SceneManagerScript : MonoBehaviour {

	private PhaseScript phaseScript;
	public int levelNum = 1;

	void Start () {
		phaseScript = GameObject.Find ("Player").GetComponent<PhaseScript>();
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
		GameObject.Find ("Player").transform.position = new Vector2(-5.0f, 1.0f);
		levelNum++;
		// write the current level into text
		File.WriteAllText ("Assets/data.txt", levelNum.ToString ());
		LoadLevel (levelNum);
	}

	public void LoadLevel(int i) {
		Application.LoadLevel ("bdgame" + i.ToString ());
		phaseScript.phase = PhaseScript.Phase.Setting;
		JSONNode sceneData = JSON.Parse (File.ReadAllText ("Assets/sceneData.json"));
		phaseScript.bombNumMax = sceneData["scenes"][i-1]["bomb"].AsInt;
		phaseScript.replaceWoodNumMax = sceneData["scenes"][i-1]["replaceWood"].AsInt;
		phaseScript.replaceSteelNumMax = sceneData["scenes"][i-1]["replaceSteel"].AsInt;
		phaseScript.createNumMax = sceneData["scenes"][i-1]["create"].AsInt;
	}

}
