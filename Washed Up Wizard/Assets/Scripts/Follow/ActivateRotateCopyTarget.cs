using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRotateCopyTarget : MonoBehaviour {

	public Transform[] objectsToTrigger; // References to the objects that will be activated
    public bool oneTime; // Whether the object can only be activated once

	private bool isActivated; // Whether the objects have been activated

	// Use this for initialization
	void Start () {
		isActivated = false; // Setting the bool
	}

	public void Activate () {
		if (!oneTime && !isActivated) {
			foreach (Transform triggerObject in objectsToTrigger) { // Goes through each object that must be activated
				triggerObject.GetComponent<RotateCopyTargetChangeOnReach> ().enabled = true; // Enables the script
			}
		}
	}
}
