using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnByTime : MonoBehaviour {

	public float activateTime = 2; // The delay to activate the emitter
	public float deactivateTime = 3; // The delay to deactivate the emitter
	public GameObject[] emitters; // The emitters to activate
	public float startTime = 1; // The delay to initially start the emitter

	void OnEnable () {
		foreach (GameObject emitter in emitters) { // Goes through each emitter
			emitter.SetActive (false); // Disables it
		}
		StartCoroutine (WaitToActivate (startTime));
	}

	IEnumerator WaitToActivate (float delay) {
		yield return new WaitForSeconds (delay);
        foreach (GameObject emitter in emitters) { // Goes through each emitter
            emitter.SetActive (true); // Enables it
		}
		StartCoroutine (WaitToDeactivate ());
	}

	IEnumerator WaitToDeactivate () {
		yield return new WaitForSeconds (deactivateTime); // Waits...
        foreach (GameObject emitter in emitters) { // Goes through each emitter
            emitter.SetActive (false); // Disables it
		}
		StartCoroutine (WaitToActivate (activateTime)); // Waits...
	}
}
