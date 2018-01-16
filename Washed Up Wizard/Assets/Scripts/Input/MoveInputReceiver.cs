using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInputReceiver : MonoBehaviour {

	public KeyCode moveForward = KeyCode.W; // The key to move forward
	public KeyCode moveBack = KeyCode.S; // The key to move back
	public KeyCode moveLeft = KeyCode.A; // The key to move left
	public KeyCode moveRight = KeyCode.D; // The key to move right

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool inputMF;
	[HideInInspector] public bool inputMB;
	[HideInInspector] public bool inputML;
	[HideInInspector] public bool inputMR;

	// Update is called once per frame
	void Update () {
		inputMF = Input.GetKey (moveForward); // Getting input for moving forward
		inputMB = Input.GetKey (moveBack); // Getting input for moving back
		inputML = Input.GetKey (moveLeft); // Getting input for moving left
		inputMR = Input.GetKey (moveRight); // Getting input for moving right
	}
}
