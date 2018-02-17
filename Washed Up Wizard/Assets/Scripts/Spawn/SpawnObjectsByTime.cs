using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectsByTime : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
    public Transform parent; // The object to parent the spawned object under
    public string parentString;
    public float delay = 1; // The time between spawning
    public float randDelay = 0.1f; // Adds slight randomness to the delay
	public int numToSpawn = 5; // The number of objects to spawn
	public float radius = 90; // The radius to spawn objects around
	public float offset = 0; // Adding an offet to the spawn radius

    [HideInInspector] public GameObject[] spawnedObjects; // References to the spawned objects
    private bool canSpawn = true; // Whether a object can be spawned
    private Transform spellsParent; // The default parent
    private float timeUntilNextSpawn; // The delay before starting to spawn again

	void Awake () {
		spellsParent = GameObject.Find ("Spells").transform; // Getting the reference
        if (!parent && parentString != string.Empty) {
            parent = GameObject.Find(parentString).transform;
        }
	}

	void OnEnable () {
		canSpawn = true;
        if (delay == 0) {
            Spawn();
            enabled = false;
        }
	}

	void FixedUpdate () {
		if (Time.time >= timeUntilNextSpawn) { // If the time has elapsed
			canSpawn = true;
		}
		if (canSpawn) {
			Spawn ();
		}
	}

	void Spawn () {
		if (numToSpawn != 1) { // If the number of objects to spawn is not 1
			spawnedObjects = new GameObject[numToSpawn]; // Resetting the array
			if (parent != null) { // If there is a parent reference
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
		timeUntilNextSpawn = Time.time + delay + Random.Range (-randDelay, randDelay); // Sets the delay with slight randomness
	}
}
