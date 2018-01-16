using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByConstantSpeed : MonoBehaviour {

	public float speed = 10; // The speed to move at
	public Vector3 dir = Vector3.zero; // Direction of speed
	public bool relative = false; // Whether the object moves on the global or local axis

	private Rigidbody rb; // Reference to rigidbody

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (relative) {
			rb.velocity = (transform.TransformDirection (dir.normalized) * speed); // Setting the velocity
		} else {
			rb.velocity = (dir.normalized * speed); // Setting the velocity
		}
	}
}