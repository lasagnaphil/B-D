﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
	
	private PhaseScript phaseManager;
	public Text phaseLabel;
	public Text scoreLabel;
	public Text bombLabel;
	public Text replaceLabel;
	public Text createLabel;
	
	void Awake () {
		phaseManager = GameObject.Find ("Player").GetComponent<PhaseScript> ();
	}

	void Update () {
		if (phaseManager.phase == PhaseScript.Phase.Setting) {
			phaseLabel.text = "Planning Phase";
		} else if (phaseManager.phase == PhaseScript.Phase.Action) {
			phaseLabel.text = "Action Phase";
		}
		scoreLabel.text = "Score: " + phaseManager.score.ToString ();
		bombLabel.text = phaseManager.bombNum.ToString ();
		replaceLabel.text = phaseManager.replaceNum.ToString ();
		createLabel.text = phaseManager.createNum.ToString ();
	}
}