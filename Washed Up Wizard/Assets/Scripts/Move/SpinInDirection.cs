using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinInDirection : MonoBehaviour {

	public float rotSpeed = 10; // The rotational speed
	public Vector3 dir = Vector3.zero; // The direction
	public float rotAcceleration = 0; // The rotational force being applied
	public float minRotSpeed = -1000f;
	public float maxRotSpeed = 1000f;

	private Rigidbody rb; // Reference to the rigidbody

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	void OnEnable () {
		rb.angularVelocity = rotSpeed * dir; // Setting the rotational velocity
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.angularVelocity = rotSpeed * dir; // Adding the rotational force
		rotSpeed += rotAcceleration; // Increasing the spin speed
		if (rb.angularVelocity.magnitude < minRotSpeed) { // If the rotational velocity is less than the min velocity
			rb.angularVelocity = minRotSpeed * dir; // Setting the rotational velocity to the min velocity
        } else if (rb.angularVelocity.magnitude > maxRotSpeed) { // If the rotational velocity is greater than the min velocity
            rb.angularVelocity = maxRotSpeed * dir; // Setting the rotational velocity to the max velocity
		}	
	}
}
