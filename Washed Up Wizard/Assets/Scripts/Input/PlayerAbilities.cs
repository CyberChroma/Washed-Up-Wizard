using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	public float teleportCooldown = 1;
	public GameObject reticle;

	private bool canTeleport = false;
	private Transform cursorPosition;
	private GameObject tempReticle;
	private SpecialInputReceiver specialInputReceiver; // Reference to input manager

	// Use this for initialization
	void Awake () {
		cursorPosition = GameObject.Find ("Cursor Position").transform;
		specialInputReceiver = GameObject.Find ("Input Controller").GetComponent <SpecialInputReceiver> ();
	}

	void OnEnable () {
		StartCoroutine (WaitToTeleport ());
	}

	// Update is called once per frame
	void Update () {
		if (canTeleport) {
			if (specialInputReceiver.inputTD) {
				tempReticle = Instantiate (reticle, cursorPosition); // Spawns the object as a parent of a transform
			} else if (specialInputReceiver.inputTU) {
				if (tempReticle != null) {
					Destroy (tempReticle);
					tempReticle = null;
				}
				if (!Physics.Linecast (transform.position, cursorPosition.position)) {
					transform.position = cursorPosition.position;
					StartCoroutine (WaitToTeleport ());
				}
			}
		}
	}

	IEnumerator WaitToTeleport () {
		canTeleport = false;
		yield return new WaitForSeconds (teleportCooldown);
		canTeleport = true;
	}
}
