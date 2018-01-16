using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	public float rotSpeed = 10; // The rotational speed
	public Vector3 dir = Vector3.zero;
	public float rotAcceleration = 0; // The rotational force being applied
	public float minRotSpeed = -1000f;
	public float maxRotSpeed = 1000f;

	private Rigidbody rb; // Reference to the rigidbody

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	void OnEnable () {
		rb.angularVelocity = rotSpeed * dir;
	}

	// Update is called once per frame
	void FixedUpdate () {
		rb.angularVelocity = rotSpeed * dir; // Adding the rotational force
		rotSpeed += rotAcceleration;
		if (rb.angularVelocity.magnitude < minRotSpeed) {
			rb.angularVelocity = minRotSpeed * dir;
		} else if (rb.angularVelocity.magnitude > maxRotSpeed) {
			rb.angularVelocity = maxRotSpeed * dir;
		}	
	}
}
