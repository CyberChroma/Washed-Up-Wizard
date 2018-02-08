using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectChangeSpeed : MonoBehaviour {

	public float startSpeed = 1;
	public float endSpeed = 15;
	public float changeSpeed = 2;

	private float currentSpeed;
    // References to scripts
	private SpawnObjectByTime spawnObjectByTime;
	private SpawnObjectsByTime spawnObjectsByTime;

	// Use this for initialization
	void Awake () {
        // Getting references
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
		if (spawnObjectByTime != null) { // If the reference exists
            if (spawnObjectByTime.spawnedObject != null) { // If the reference exists
				spawnObjectByTime.spawnedObject.GetComponent<MoveInDirection> ().speed = currentSpeed;
				spawnObjectByTime.spawnedObject = null;
			}
		}
        if (spawnObjectsByTime != null) { // If the reference exists
			if (spawnObjectsByTime.spawnedObjects != null) {
				foreach (GameObject spawnedObject in spawnObjectsByTime.spawnedObjects) { // Goes through each object
					spawnedObject.GetComponent<MoveInDirection> ().speed = currentSpeed;
				}
				spawnObjectsByTime.spawnedObjects = new GameObject[0];
			}
		}
		currentSpeed = Mathf.MoveTowards (currentSpeed, endSpeed, changeSpeed); // Chaning the spawn speed
	}
}
