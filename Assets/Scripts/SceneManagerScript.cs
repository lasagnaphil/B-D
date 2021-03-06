﻿using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using SimpleJSON;

public class SceneManagerScript : MonoBehaviour {

	private PhaseScript phaseScript;
	public int levelNum = 1;
	public int maxLevelNum = 8;

	void Start () {
		Color color = GetComponent<SpriteRenderer> ().color;
		color.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = color;
		phaseScript = GameObject.Find ("Player").GetComponent<PhaseScript>();
		DontDestroyOnLoad (GameObject.Find ("Canvas"));
		DontDestroyOnLoad (GameObject.Find ("Player"));
		DontDestroyOnLoad (GameObject.Find ("UIManager"));
		DontDestroyOnLoad (GameObject.Find ("BombCanvas"));
		DontDestroyOnLoad (gameObject);
		levelNum = int.Parse(File.ReadAllText ("data.txt"));
		if (levelNum != 1)
			LoadLevel (levelNum);
		else
			updatePhaseVariables (1);
	}

	void Update() {
		if (Application.loadedLevelName == "bdgameover" ||
		    Application.loadedLevelName == "bdfinish") {
			Destroy (GameObject.Find ("Canvas"));
			Destroy (GameObject.Find ("Player"));
			Destroy (GameObject.Find ("UIManager"));
			Destroy (GameObject.Find ("BombCanvas"));
			Destroy (gameObject);
		}
	}

	public void LoadNextLevel() {
		GameObject.Find ("Player").transform.position = new Vector2(-5.0f, 1.0f);
		if (levelNum == 4)
			GameObject.Find ("Player").transform.position = new Vector2 (-7.0f, 1.0f);
		GameObject.Find ("Player").GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f, 0f);
		if (levelNum == maxLevelNum) {
			Application.LoadLevel ("bdfinish");
			File.WriteAllText ("data.txt", levelNum.ToString ());
			return;
		}
		levelNum++;
		// write the current level into text
		File.WriteAllText ("data.txt", levelNum.ToString ());
		LoadLevel (levelNum);
	}

	public void LoadLevel(int i) {
		Application.LoadLevel ("bdgame" + i.ToString ());
		updatePhaseVariables (i);
	}

	void updatePhaseVariables(int i)
	{
		phaseScript.phase = PhaseScript.Phase.Setting;
		var sceneData = JSON.Parse (File.ReadAllText ("sceneData.json"));
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
