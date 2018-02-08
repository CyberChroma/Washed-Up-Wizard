using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectByActivate : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
	public Transform parent; // The object to parent the spawned object under
	public float cooldown = 1; // The time between spawning

	private bool canSpawn = true; // Whether this emitter can spawn objects
	private Transform spellsParent; // The default parent of the spell

	void Start () {
		spellsParent = GameObject.Find ("Spells").transform; // Getting the reference
	}

	public void Spawn () {
		if (canSpawn) { // If the emitter can spawn objects
			if (parent != null) { // If there is a parent
				Instantiate (objectToSpawn, transform.position, transform.rotation, parent); // Spawns the object as a parent of a transform
			} else {
				Instantiate (objectToSpawn, transform.position, transform.rotation, spellsParent); // Spawns the object as a parent of a transform
			}
			StartCoroutine (WaitToSpawn ());
		}
	}

	IEnumerator WaitToSpawn () {
		canSpawn = false;
		yield return new WaitForSeconds (cooldown); // Wait...
		canSpawn = true;
	}
}
