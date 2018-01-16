using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectChangeSpeed : MonoBehaviour {

	public float startSpeed = 1;
	public float endSpeed = 15;
	public float changeSpeed = 2;

	private float currentSpeed;
	private SpawnObjectByTime spawnObjectByTime;
	private SpawnObjectsByTime spawnObjectsByTime;

	// Use this for initialization
	void Awake () {
		spawnObjectByTime = GetComponent<SpawnObjectByTime> ();
		spawnObjectsByTime = GetComponent<SpawnObjectsByTime> ();
	}

	void OnEnable () {
		currentSpeed = startSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		ChangeSpeed ();
	}

	void ChangeSpeed () {
		if (spawnObjectByTime != null) {
			if (spawnObjectByTime.spawnedObject != null) {
				spawnObjectByTime.spawnedObject.GetComponent<MoveInDirection> ().speed = currentSpeed;
				spawnObjectByTime.spawnedObject = null;
			}
		}
		if (spawnObjectsByTime != null) {
			if (spawnObjectsByTime.spawnedObjects != null) {
				foreach (GameObject spawnedObject in spawnObjectsByTime.spawnedObjects) {
					spawnedObject.GetComponent<MoveInDirection> ().speed = currentSpeed;
				}
				spawnObjectsByTime.spawnedObjects = new GameObject[0];
			}
		}
		currentSpeed = Mathf.MoveTowards (currentSpeed, endSpeed, changeSpeed);
	}
}
