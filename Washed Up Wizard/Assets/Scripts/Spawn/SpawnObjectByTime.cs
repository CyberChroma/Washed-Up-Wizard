using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectByTime : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
	public Transform parent;
	public float delay = 1;
	public float randDelay = 0.1f;

	[HideInInspector] public GameObject spawnedObject;
	private bool canSpawn = true;
	private Transform spellsParent;
	private float timeUntilNextSpawn;

	void Awake () {
		spellsParent = GameObject.Find ("Spells").transform;
	}

	void OnEnable () {
		canSpawn = true;
	}

	void OnDisable () {
		canSpawn = false;
	}
	void FixedUpdate () {
		if (Time.time >= timeUntilNextSpawn) {
			canSpawn = true;
		}
		if (canSpawn) {
			Spawn ();
		}
	}

	void Spawn () {
		if (parent != null) {
			spawnedObject = Instantiate (objectToSpawn, parent); // Spawns the object as a parent of a transform
		} else {
			spawnedObject = Instantiate (objectToSpawn, transform.position, transform.rotation, spellsParent); // Spawns the object as a parent of a transform
		}
		canSpawn = false;
		timeUntilNextSpawn = Time.time + delay + Random.Range (-randDelay, randDelay);
	}
}
