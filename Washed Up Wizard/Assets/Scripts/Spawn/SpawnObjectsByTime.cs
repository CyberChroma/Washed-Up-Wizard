using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsByTime : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
	public Transform parent;
	public float delay = 1;
	public float randDelay = 0.1f;
	public int numToSpawn = 5;
	public float radius = 90;
	public float offset = 0;

	[HideInInspector] public GameObject[] spawnedObjects;
	private bool canSpawn = true;
	private Transform spellsParent;
	private float timeUntilNextSpawn;

	void Awake () {
		spellsParent = GameObject.Find ("Spells").transform;
	}

	void OnEnable () {
		canSpawn = true;
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
		if (numToSpawn != 1) {
			spawnedObjects = new GameObject[numToSpawn];
			if (parent != null) {
				for (int i = 0; i < numToSpawn; i++) {
					spawnedObjects [i] = Instantiate (objectToSpawn, parent.position, Quaternion.Euler (parent.rotation.eulerAngles.x, parent.rotation.eulerAngles.y + (-(radius / 2) + (radius / (numToSpawn - 1)) * i) + offset, parent.rotation.eulerAngles.z), parent); // Spawns the object as a parent of a transform
				}
			} else {
				for (int i = 0; i < numToSpawn; i++) {
					spawnedObjects [i] = Instantiate (objectToSpawn, transform.position, Quaternion.Euler (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (-(radius / 2) + (radius / (numToSpawn - 1)) * i) + offset, transform.rotation.eulerAngles.z), spellsParent); // Spawns the object as a parent of a transform
				}
			}
		}
		canSpawn = false;
		timeUntilNextSpawn = Time.time + delay + Random.Range (-randDelay, randDelay);
	}
}
