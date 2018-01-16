using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectByActivate : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
	public Transform parent;
	public float cooldown = 1;

	private bool canSpawn = true;
	private Transform spellsParent;

	void Start () {
		spellsParent = GameObject.Find ("Spells").transform;
	}

	public void Spawn () {
		if (canSpawn) {
			if (parent != null) {
				Instantiate (objectToSpawn, transform.position, transform.rotation, parent); // Spawns the object as a parent of a transform
			} else {
				Instantiate (objectToSpawn, transform.position, transform.rotation, spellsParent); // Spawns the object as a parent of a transform
			}
			StartCoroutine (WaitToSpawn ());
		}
	}

	IEnumerator WaitToSpawn () {
		canSpawn = false;
		yield return new WaitForSeconds (cooldown);
		canSpawn = true;
	}
}
