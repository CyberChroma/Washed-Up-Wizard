using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInputReceiver : MonoBehaviour {

	public KeyCode teleport = KeyCode.LeftShift; // The key to teleport
	public KeyCode interact = KeyCode.E; // The key to interact

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool inputTD;
	[HideInInspector] public bool inputTU;
	[HideInInspector] public bool inputI;

    void OnDisable () {
        inputTD = false;
        inputTU = false;
        inputI = false;
    }

	// Update is called once per frame
	void Update () {
		inputTD = Input.GetKeyDown (teleport); // Getting input for teleporting
		inputTU = Input.GetKeyUp (teleport); // Getting input for teleporting
		inputI = Input.GetKeyDown (interact); // Getting input for interacting
	}
}
