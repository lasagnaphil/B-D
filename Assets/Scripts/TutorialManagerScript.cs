using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using SimpleJSON;

public class TutorialManagerScript : MonoBehaviour {

	private PhaseScript phaseScript;
	public int levelNum = 1;

	void Start () {
		phaseScript = GameObject.Find ("Player").GetComponent<PhaseScript>();
		DontDestroyOnLoad (GameObject.Find ("Canvas"));
		DontDestroyOnLoad (GameObject.Find ("Player"));
		DontDestroyOnLoad (GameObject.Find ("UIManager"));
		DontDestroyOnLoad (GameObject.Find ("BombCanvas"));
		DontDestroyOnLoad (gameObject);
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
		LoadLevel (levelNum);
	}

	public void LoadLevel(int i) {
		if (i == 10) {
			Destroy (GameObject.Find ("Canvas"));
			Destroy (GameObject.Find ("Player"));
			Destroy (GameObject.Find ("UIManager"));
			Destroy (GameObject.Find ("BombCanvas"));
			Destroy (gameObject);
			Application.LoadLevel ("bdtitle");

		} else {
			Application.LoadLevel ("bdtutorial" + i.ToString ());
			updatePhaseVariables (i);
		}
	}

	void updatePhaseVariables(int i)
	{
		phaseScript.phase = PhaseScript.Phase.Setting;
		var sceneData = JSON.Parse (File.ReadAllText ("tutorialData.json"));
		phaseScript.bombNumMax = sceneData[i.ToString()]["bomb"].AsInt;
		phaseScript.timeBombNumMax = sceneData [i.ToString ()] ["timeBomb"].AsInt;
		phaseScript.replaceWoodNumMax = sceneData[i.ToString()]["replaceWood"].AsInt;
		phaseScript.replaceSteelNumMax = sceneData[i.ToString()]["replaceSteel"].AsInt;
		phaseScript.createNumMax = sceneData[i.ToString()]["create"].AsInt;
		phaseScript.bombNum = phaseScript.bombNumMax;
		phaseScript.timeBombNum = phaseScript.timeBombNumMax;
		phaseScript.replaceWoodNum = phaseScript.replaceWoodNumMax;
		phaseScript.replaceSteelNum = phaseScript.replaceSteelNumMax;
		phaseScript.createNum = phaseScript.createNumMax;
	}

}
