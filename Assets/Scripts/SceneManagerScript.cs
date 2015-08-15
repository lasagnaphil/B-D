using UnityEngine;
using System.IO;
using System.Text;
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
		else
			updatePhaseVariables (1);
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
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		levelNum++;
		// write the current level into text
		File.WriteAllText ("Assets/data.txt", levelNum.ToString ());
		LoadLevel (levelNum);
	}

	public void LoadLevel(int i) {
		Application.LoadLevel ("bdgame" + i.ToString ());
		updatePhaseVariables (i);
	}

	void updatePhaseVariables(int i)
	{
		phaseScript.phase = PhaseScript.Phase.Setting;
		var sceneData = JSON.Parse (File.ReadAllText ("Assets/sceneData.json"));
		phaseScript.bombNumMax = sceneData[i.ToString()]["bomb"].AsInt;
		phaseScript.replaceWoodNumMax = sceneData[i.ToString()]["replaceWood"].AsInt;
		phaseScript.replaceSteelNumMax = sceneData[i.ToString()]["replaceSteel"].AsInt;
		phaseScript.createNumMax = sceneData[i.ToString()]["create"].AsInt;
		phaseScript.bombNum = phaseScript.bombNumMax;
		phaseScript.replaceWoodNum = phaseScript.replaceWoodNumMax;
		phaseScript.replaceSteelNum = phaseScript.replaceSteelNumMax;
		phaseScript.createNum = phaseScript.createNumMax;
	}

}
