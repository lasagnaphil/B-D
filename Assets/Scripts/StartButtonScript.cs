﻿using UnityEngine;
using System.Collections;

public class StartButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void loadGame() {
		Application.LoadLevel ("bdgame");
	}

	public void loadTutorial() {
		Application.LoadLevel ("bdtutorial1");
	}
}
