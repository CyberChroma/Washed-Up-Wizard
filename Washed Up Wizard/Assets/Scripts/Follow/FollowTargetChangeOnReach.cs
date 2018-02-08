using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetChangeOnReach : MonoBehaviour {

	public Rigidbody[] targets; // The targets the object should go through
	public float delay; // The delay before switching targets

	private FollowTargetConstant followTargetConstant; // Reference to the script
	private FollowTargetLerp followTargetLerp; // Reference to the script
	private Rigidbody rb; // Reference to the rigidbody
	private int targetNum = 0; // The current target number
	private bool following = true; // Whether this object is currently following a target

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
	
	// Update is called once per frame
	void FixedUpdate () {
		if (following) { // If this object is following a target
			if (followTargetConstant && followTargetLerp) { // If both of these have a value
				if (Vector3.Distance (rb.position, followTargetConstant.target.position) <= 0.01f) { // If the object has basically reached it's target
					StartCoroutine (NextTarget ());
				}
			} else if (followTargetConstant) { // If this has a value
				if (Vector3.Distance (rb.position, followTargetConstant.target.position) <= 0.01f) { // If the object has basically reached it's target
					StartCoroutine (NextTarget ());
				}
			} else if (followTargetLerp) { // If this has a value
				if (Vector3.Distance (rb.position, followTargetLerp.target.position) <= 0.1f) { // If the object has basically reached it's target
					StartCoroutine (NextTarget ());
				}
			}
		}
	}

	IEnumerator NextTarget () {
		following = false; // Setting the bool
		yield return new WaitForSeconds (delay); // Waits...
		targetNum++; // Increasing the target number
		if (targetNum > targets.Length - 1) { // If the end of the array has been reached
			targetNum = 0; // Reset the target number
		}
		if (followTargetConstant) { // If this has a value
			rb.position = followTargetConstant.target.position; // Moving the object to the target's position
			followTargetConstant.target = targets [targetNum]; // Setting the target to the next in the array
		}
		if (followTargetLerp) { // If this has a value
			rb.position = followTargetLerp.target.position; // Moving the object to the target's position
			followTargetLerp.target = targets [targetNum]; // Setting the target to the next in the array
		}
		following = true; // Setting the bool
	}
}
