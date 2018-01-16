using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCopyTargetChangeOnReach : MonoBehaviour {

	public Rigidbody[] targets; // The targets the object should go through
	public float delay; // The delay before switching targets

	private RotateCopyTargetConstant rotateCopyTargetConstant; // Reference to the script
	private RotateCopyTargetLerp rotateCopyTargetLerp; // Reference to the script
	private Rigidbody rb; // Reference to the rigidbody
	private int targetNum = 0; // The current target number
	private bool rotating = true; // Whether this object is currently rotating to copy a target

	// Use this for initialization
	void Awake () {
		// Getting references
		rb = GetComponent<Rigidbody> ();
		rotateCopyTargetConstant = GetComponent<RotateCopyTargetConstant> ();
		rotateCopyTargetLerp = GetComponent<RotateCopyTargetLerp> ();
		if (rotateCopyTargetConstant) { // If this has a value
			rotateCopyTargetConstant.target = targets [0]; // Setting its target
		}
		if (rotateCopyTargetLerp) { // If this has a value
			rotateCopyTargetLerp.target = targets [0]; // Setting its target
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (rotating) { // If this object is rotating to copy a target
			if (rotateCopyTargetConstant && rotateCopyTargetLerp) { // If both of these have a value
				if (Quaternion.Angle (rb.rotation, rotateCopyTargetConstant.target.rotation) <= 0.01f) { // If the object has basically reached it's target's rotation
					StartCoroutine (NextTarget ());
				}
			} else if (rotateCopyTargetConstant) { // If this has a value
				if (Quaternion.Angle (rb.rotation, rotateCopyTargetConstant.target.rotation) <= 0.01f) { // If the object has basically reached it's target's rotation
					StartCoroutine (NextTarget ());
				}
			} else if (rotateCopyTargetLerp) { // If this has a value
				if (Quaternion.Angle (rb.rotation, rotateCopyTargetLerp.target.rotation) <= 0.1f) { // If the object has basically reached it's target's rotation
					StartCoroutine (NextTarget ());
				}
			}
		}
	}

	IEnumerator NextTarget () {
		rotating = false; // Setting the bool
		yield return new WaitForSeconds (delay); // Waits...
		targetNum++; // Increasing the target number
		if (targetNum > targets.Length - 1) { // If the end of the array has been reached
			targetNum = 0; // Reset the target number.
		}
		if (rotateCopyTargetConstant) { // If this has a value
			rb.rotation = rotateCopyTargetConstant.target.rotation; // Rotating the object to the target's rotation
			rotateCopyTargetConstant.target = targets [targetNum]; // Setting the target to the next in the arra
		}
		if (rotateCopyTargetLerp) { // If this has a value
			rb.rotation = rotateCopyTargetLerp.target.rotation; // Rotating the object to the target's rotation
			rotateCopyTargetLerp.target = targets [targetNum]; // Setting the target to the next in the arra
		}
		rotating = true; // Setting the bool
	}
}
