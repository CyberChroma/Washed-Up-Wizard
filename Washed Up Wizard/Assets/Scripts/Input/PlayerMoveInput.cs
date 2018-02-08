﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour {

	public float moveSensitivity = 0.3f;
	public float moveSpeed = 5f;

	private Vector3 moveVector;
	private float v = 0; // Vertical direction
	private float h = 0; // Horizontal direction
	private Rigidbody rb;
	private MoveInputReceiver moveInputReceiver; // Reference to input manager

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		moveInputReceiver = GameObject.Find ("Input Controller").GetComponent<MoveInputReceiver>(); // Getting the reference
	}

	void OnDisable () {
        // Resetting variables
		v = 0;
		h = 0;
		moveVector = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Calculating vertical direction
		if (moveInputReceiver.inputMB) {
			v = Mathf.MoveTowards (v, -1, moveSensitivity);
		} else if (moveInputReceiver.inputMF) {
			v = Mathf.MoveTowards (v, 1, moveSensitivity);
		} else {
			v = Mathf.MoveTowards (v, 0, moveSensitivity);
		}
		// Calculating horizontal direction
		if (moveInputReceiver.inputML) {
			h = Mathf.MoveTowards (h, -1, moveSensitivity);
		} else if (moveInputReceiver.inputMR) {
			h = Mathf.MoveTowards (h, 1, moveSensitivity);
		} else {
			h = Mathf.MoveTowards (h, 0, moveSensitivity);
		}
		moveVector = new Vector3 (h, 0, v); // Setting move vector for direction
		if (moveVector.magnitude > 1) { // If the move vector is greater than 1
			moveVector.Normalize (); // Setting direction
		}
		moveVector *= moveSpeed * Time.deltaTime; // Multiplying move vector for magnutude
		if (moveVector != Vector3.zero) { // Of the move vector is not zero
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveVector), 5f * Time.deltaTime); // Rotates in direction of movement
			transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0)); // Ignores x and z rotation
		}
		rb.MovePosition (rb.position + moveVector); // Moves player
	}
}
