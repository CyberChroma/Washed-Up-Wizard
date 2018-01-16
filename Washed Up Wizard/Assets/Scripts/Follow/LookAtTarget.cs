using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

	public Transform target; // The object to look at
	public bool once = false;
	public float speed = 10;

	void Awake () {
		if (target == null && GameObject.Find ("Player")) {
			target = GameObject.Find ("Player").transform;
		}
	}

	void OnEnable () {
		Look ();
	}

	// Update is called once per frame
	void Update () {
		if (!once) {
			Look ();
		}
	}

	void Look () {
		if (target) {
			Quaternion targetRotation = Quaternion.RotateTowards (transform.rotation, Quaternion.LookRotation (target.position - transform.position), speed);
			transform.rotation = Quaternion.Euler (0, targetRotation.eulerAngles.y, 0);
		}
	}
}
