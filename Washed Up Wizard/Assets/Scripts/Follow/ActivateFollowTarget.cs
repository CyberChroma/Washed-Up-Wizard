using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFollowTarget : MonoBehaviour {

	public Transform[] objectsToTrigger; // References to the objects that will be activated
	public bool oneTime;

	private bool isActivated; // Bool for whether the objects have been activated

	// Use this for initialization
	void Start () {
		isActivated = false; // Setting the bool
	}

	public void Activate () {
		if (!oneTime && !isActivated) {
			foreach (Transform triggerObject in objectsToTrigger) { // Goes through each object that must be activated
				triggerObject.GetComponent<FollowTargetChangeOnActivate> ().enabled = true; // Enables the script
			}
		}
	}
}
