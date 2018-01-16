using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCopyTargetLerp : MonoBehaviour {

	public float smoothing = 5f; // Smoothing applied to rotating
	public Rigidbody target; // The target to rotate after

	private Rigidbody rb; // This object's rigidbody

	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
	}

	void FixedUpdate () {
		rb.rotation = Quaternion.Slerp (rb.rotation, target.rotation, smoothing / 10); // Rotates after object
	}
}
