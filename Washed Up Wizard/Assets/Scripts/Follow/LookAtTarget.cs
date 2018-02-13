using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

	public Transform target; // The object to look at
	public bool once = false; // If the object should look once
	public float speed = 10; // The speed to turn

	void Awake () {
		if (target == null && GameObject.Find ("Player")) { // If the the player exists and a target is not assigned
			target = GameObject.Find ("Player").transform; // Setting the player as the target
		}
	}

	void OnEnable () {
		Look ();
	}

	// Update is called once per frame
	void Update () {
		if (!once) { // If the object can only look once
			Look ();
		}
	}

	void Look () {
        if (target && Quaternion.LookRotation (target.position - transform.position) != Quaternion.identity) { // If the target exists
			Quaternion targetRotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (target.position - transform.position), speed); // Rotate towards the target
			transform.rotation = Quaternion.Euler (0, targetRotation.eulerAngles.y, 0); // Ignoring the x and z rotations
		}
	}
}
