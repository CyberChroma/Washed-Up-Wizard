using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInputReceiver : MonoBehaviour {

	public KeyCode teleport = KeyCode.LeftShift; // The key to teleport
    public KeyCode pause = KeyCode.Escape;  // The key to pause the game
    public KeyCode advanceText = KeyCode.LeftControl;  // The key to advance text
    public KeyCode skipCutscenes = KeyCode.Return;  // The key to skip cutscenes

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool inputTD;
	[HideInInspector] public bool inputTU;
    [HideInInspector] public bool inputP;
    [HideInInspector] public bool inputA;
    [HideInInspector] public bool inputS;

    void OnDisable () {
        inputTD = false;
        inputTU = false;
        inputP = false;
        inputA = false;
        inputS = false;
    }

	// Update is called once per frame
	void Update () {
		inputTD = Input.GetKeyDown (teleport); // Getting input for teleporting
		inputTU = Input.GetKeyUp (teleport); // Getting input for teleporting
        inputP = Input.GetKeyDown (pause); // Getting input for flipping to the previous page
        inputA = Input.GetKeyDown (advanceText); // Getting input for flipping to the previous page
        inputS = Input.GetKeyDown (skipCutscenes); // Getting input for flipping to the previous page

	}
}
