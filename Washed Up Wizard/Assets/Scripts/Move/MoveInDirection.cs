using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirection : MonoBehaviour {

	public float speed = 10; // The force applied to move the object
	public float acceleration = 0; // The rate the speed increases (per second)
	public Vector3 dir = Vector3.zero; // Direction of force
	public bool local = false; // Whether the object moves on the global or local axis
	public float minSpeed = -1000f;
	public float maxSpeed = 1000f;
	private Rigidbody rb; // Reference to rigidbody

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (local) {
			rb.velocity = (transform.TransformDirection (dir.normalized) * speed); // Setting the velocity
		} else {
			rb.velocity = dir * speed; // Setting the start speed
		}
		if (rb.velocity.magnitude < minSpeed) { // If the velocity is less than the min speed
			rb.velocity = rb.velocity.normalized * minSpeed; // Makes the velocity the min speed
		} else if (rb.velocity.magnitude > maxSpeed) { // If the velocity is greater  than the max speed
			rb.velocity = rb.velocity.normalized * maxSpeed; // Make the velocity the max speed
		}
		speed += acceleration * Time.deltaTime; // Increasing the speed
	}
}