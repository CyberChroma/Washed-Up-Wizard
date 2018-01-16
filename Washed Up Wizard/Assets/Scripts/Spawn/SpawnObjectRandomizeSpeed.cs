using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectRandomizeSpeed : MonoBehaviour {

	public float minSpeed = 1;
	public float maxSpeed = 15;

	private SpawnObjectByTime spawnObjectByTime;
	private SpawnObjectsByTime spawnObjectsByTime;

	// Use this for initialization
	void Awake () {
		spawnObjectByTime = GetComponent<SpawnObjectByTime> ();
		spawnObjectsByTime = GetComponent<SpawnObjectsByTime> ();
	}
	
	// Update is called once per frame
	void Update () {
		RandomzeSpeed ();
	}

	void RandomzeSpeed () {
		if (spawnObjectByTime != null) {
			if (spawnObjectByTime.spawnedObject != null) {
				spawnObjectByTime.spawnedObject.GetComponent<MoveInDirection> ().speed = Random.Range (minSpeed, maxSpeed);
				spawnObjectByTime.spawnedObject = null;
			}
		}
		if (spawnObjectsByTime != null) {
			if (spawnObjectsByTime.spawnedObjects != null) {
				foreach (GameObject spawnedObject in spawnObjectsByTime.spawnedObjects) {
					spawnedObject.GetComponent<MoveInDirection> ().speed = Random.Range (minSpeed, maxSpeed);
				}
				spawnObjectsByTime.spawnedObjects = new GameObject[0];
			}
		}
	}
}
