using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialInputReceiver : MonoBehaviour {

	public KeyCode teleport = KeyCode.LeftShift; // The key to teleport
    public KeyCode toggleSpellBook = KeyCode.Tab; // The key to toggle spell book
    public KeyCode nextPage = KeyCode.E; // The key to interact
    public KeyCode previous = KeyCode.Q; // The key to interact

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool inputTD;
	[HideInInspector] public bool inputTU;
	[HideInInspector] public bool inputS;
    [HideInInspector] public bool inputN;
    [HideInInspector] public bool inputP;

    void OnDisable () {
        inputTD = false;
        inputTU = false;
        inputS = false;
        inputN = false;
        inputP = false;
    }

	// Update is called once per frame
	void Update () {
		inputTD = Input.GetKeyDown (teleport); // Getting input for teleporting
		inputTU = Input.GetKeyUp (teleport); // Getting input for teleporting
        inputS = Input.GetKeyDown (toggleSpellBook); // Getting input for toggling spell book
        inputN = Input.GetKeyDown (nextPage); // Getting input for flipping to the next page
        inputP = Input.GetKeyDown (previous); // Getting input for flipping to the previous page
	}
}
