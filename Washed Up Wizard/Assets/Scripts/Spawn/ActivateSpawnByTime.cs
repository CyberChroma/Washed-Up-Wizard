using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpawnByTime : MonoBehaviour {

	public float activateTime = 2;
	public float deactivateTime = 3;
	public GameObject[] emitters;
	public float startTime = 1;

	void OnEnable () {
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (false);
		}
		StartCoroutine (WaitToActivate (startTime));
	}

	IEnumerator WaitToActivate (float delay) {
		yield return new WaitForSeconds (delay);
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (true);
		}
		StartCoroutine (WaitToDeactivate ());
	}

	IEnumerator WaitToDeactivate () {
		yield return new WaitForSeconds (deactivateTime);
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (false);
		}
		StartCoroutine (WaitToActivate (activateTime));
	}
}
