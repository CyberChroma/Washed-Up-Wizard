using System.Collections;
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
		moveVector = new Vector3 (h, 0, v);
		if (moveVector.magnitude > 1) {
			moveVector.Normalize (); // Setting direction
		}
		moveVector *= moveSpeed * Time.deltaTime;
		if (moveVector != Vector3.zero) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveVector), 5f * Time.deltaTime); // Rotates after object
			transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0));
		}
		rb.MovePosition (rb.position + moveVector);
	}
}
