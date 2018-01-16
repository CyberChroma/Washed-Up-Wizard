using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetChangeOnActivate : MonoBehaviour {

	public Rigidbody[] targets; // The targets the object should go through

	private FollowTargetConstant followTargetConstant; // Reference to the script
	private FollowTargetLerp followTargetLerp; // Reference to the script
	private Rigidbody rb; // Reference to the rigidbody
	private int targetNum = 0; // The current target number

	// Use this for initialization
	void Awake () {
		// Getting references
		rb = GetComponent<Rigidbody> ();
		followTargetConstant = GetComponent<FollowTargetConstant> ();
		followTargetLerp = GetComponent<FollowTargetLerp> ();
		if (followTargetConstant) { // If this has a value
			followTargetConstant.target = targets [0]; // Setting its target
		}
		if (followTargetLerp) { // If this has a value
			followTargetLerp.target = targets [0]; // Setting its target
		}
	}

	void OnEnable () {
		NextTarget ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (followTargetConstant && followTargetLerp) { // If both of these have a value
			if (Vector3.Distance (rb.position, followTargetConstant.target.position) <= 0.01f) { // If the object has basically reached it's target
				enabled = false;
			}
		} else if (followTargetConstant) { // If this has a value
			if (Vector3.Distance (rb.position, followTargetConstant.target.position) <= 0.01f) { // If the object has basically reached it's target
				enabled = false;
			}
		} else if (followTargetLerp) { // If this has a value
			if (Vector3.Distance (rb.position, followTargetLerp.target.position) <= 0.1f) { // If the object has basically reached it's target
				enabled = false;
			}
		}
	}

	void NextTarget () {
		targetNum++; // Increasing the target number
		if (targetNum > targets.Length - 1) { // If the end of the array has been reached
			targetNum = 0; // Reset the target number.
		}
		if (followTargetConstant) { // If this has a value
			rb.position = followTargetConstant.target.position; // Moving the object to the target's position
			followTargetConstant.target = targets [targetNum]; // Setting the target to the next in the array
		}
		if (followTargetLerp) { // If this has a value
			rb.position = followTargetLerp.target.position; // Moving the object to the target's position
			followTargetLerp.target = targets [targetNum]; // Setting the target to the next in the array
		}
	}
}
