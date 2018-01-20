using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectByInput : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
	public Transform parent;
	public float cooldown = 1;
	public bool OnRelease = false;
	public GameObject reticle;
	public int emitterNum = 1;
	public Slider cooldownWheel;

	private bool canSpawn = true;
	private SpellInputReceiver spellInputReceiver;
	private SpellCreator spellCreator;
	private Transform cursorPosition;
	private GameObject tempReticle;
	private Transform spellsParent;

	void Start () {
		spellInputReceiver = GameObject.Find ("Input Controller").GetComponent<SpellInputReceiver>(); // Getting the reference
		cursorPosition = GameObject.Find ("Cursor Position").transform;
		spellsParent = GameObject.Find ("Spells").transform;
		spellCreator = GameObject.Find ("Spell Creation Controller").GetComponent<SpellCreator> ();
	}

	void Update () {
		if (emitterNum != 0 && !spellCreator.creatingSpell) {
			if (OnRelease) {
				if (spellInputReceiver.inputSD [emitterNum - 1]) {
					SpawnReticle (reticle, out tempReticle);
				}
				if (spellInputReceiver.inputSU [emitterNum - 1]) {
					if (tempReticle) {
						Spawn (objectToSpawn);
						Destroy (tempReticle);
						tempReticle = null;
					}
				}
			} else {
				if (spellInputReceiver.inputSU [emitterNum - 1]) {
					Spawn (objectToSpawn);
				}
			}
		}
		if (!canSpawn) {
			cooldownWheel.value -= Time.deltaTime;
		}
	}

	void Spawn (GameObject spawnObject) {
		if (canSpawn) {
			if (parent != null) {
				Instantiate (spawnObject, parent); // Spawns the object as a parent of a transform
			} else {
				Instantiate (spawnObject, transform.position, transform.rotation, spellsParent); // Spawns the object as a parent of a transform
			}
			StartCoroutine (WaitToSpawn ());
		}
	}

	void SpawnReticle (GameObject spawnObject, out GameObject objectReference) {
		if (canSpawn) {
			objectReference = Instantiate (spawnObject, cursorPosition); // Spawns the object as a parent of a transform
		} else {
			objectReference = null;
		}
	}

	IEnumerator WaitToSpawn () {
		canSpawn = false;
		cooldownWheel.gameObject.SetActive (true);
		cooldownWheel.value = cooldown;
		yield return new WaitForSeconds (cooldown);
		canSpawn = true;
		cooldownWheel.gameObject.SetActive (false);
	}
}
