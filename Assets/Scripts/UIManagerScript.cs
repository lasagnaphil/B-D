using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour {
	
	private PhaseScript phaseManager;
	public Text phaseLabel;
	public Text scoreLabel;
	public Text bombLabel;
	public Text replaceWoodLabel;
	public Text replaceSteelLabel;
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
		replaceWoodLabel.text = phaseManager.replaceWoodNum.ToString ();
		replaceSteelLabel.text = phaseManager.replaceSteelNum.ToString ();
		createLabel.text = phaseManager.createNum.ToString ();
	}
}
