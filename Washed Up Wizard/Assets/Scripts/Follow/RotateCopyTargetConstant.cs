using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCopyTargetConstant : MonoBehaviour {

	public float speed = 5f; // Smoothing applied to rotating
	public Rigidbody target; // The target to rotate after

	private Rigidbody rb; // This object's rigidbody

	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	void FixedUpdate () {
		rb.rotation = Quaternion.RotateTowards (rb.rotation, target.rotation, speed); // Rotates after object
	}
}
