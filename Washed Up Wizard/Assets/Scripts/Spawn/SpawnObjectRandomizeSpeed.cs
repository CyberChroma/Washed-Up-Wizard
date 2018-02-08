using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectRandomizeSpeed : MonoBehaviour {

	public float minSpeed = 1;
	public float maxSpeed = 15;

    // Script references
	private SpawnObjectByTime spawnObjectByTime;
	private SpawnObjectsByTime spawnObjectsByTime;

	// Use this for initialization
	void Awake () {
        // Getting references
		spawnObjectByTime = GetComponent<SpawnObjectByTime> ();
		spawnObjectsByTime = GetComponent<SpawnObjectsByTime> ();
	}
	
	// Update is called once per frame
	void Update () {
		RandomzeSpeed ();
	}

	void RandomzeSpeed () {
        if (spawnObjectByTime != null) { // If the reference exists
            if (spawnObjectByTime.spawnedObject != null) { // If the reference exists
				spawnObjectByTime.spawnedObject.GetComponent<MoveInDirection> ().speed = Random.Range (minSpeed, maxSpeed);
				spawnObjectByTime.spawnedObject = null;
			}
		}
        if (spawnObjectsByTime != null) { // If the reference exists
            if (spawnObjectsByTime.spawnedObjects != null) { // If the reference exists
				foreach (GameObject spawnedObject in spawnObjectsByTime.spawnedObjects) { // Goes through each object
					spawnedObject.GetComponent<MoveInDirection> ().speed = Random.Range (minSpeed, maxSpeed);
				}
				spawnObjectsByTime.spawnedObjects = new GameObject[0];
			}
		}
	}
}
