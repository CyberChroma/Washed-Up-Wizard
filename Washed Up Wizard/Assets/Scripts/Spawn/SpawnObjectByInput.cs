using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnObjectByInput : MonoBehaviour {

	public GameObject objectToSpawn; // The object to spawn
    public Transform parent; // The object to parent the spawned object under
    public float cooldown = 1; // The time between spawning
	public bool onRelease = false; // Whether the object should be spawned when a button is released
	public GameObject reticle; // The reticle to use when the button is held down
	public int emitterNum = 0; // The emitter number
	public Slider cooldownWheel; // The cooldown wheel 

    [HideInInspector] public float cooldownValue = 0;
	private bool canSpawn = true; // Whether the emitter can spawn objects
    // References to scripts and gameobjects
	private SpellInputReceiver spellInputReceiver;
	private SpellCreator spellCreator;
	private Transform cursorPosition;
	private GameObject tempReticle;
	private Transform spellsParent;

	void Start () {
        // Getting references
		spellInputReceiver = GameObject.Find ("Input Controller").GetComponent<SpellInputReceiver>();
		cursorPosition = GameObject.Find ("Cursor Position").transform;
		spellsParent = GameObject.Find ("Spells").transform;
		spellCreator = GameObject.Find ("Spell Creation Controller").GetComponent<SpellCreator> ();
	}

	void Update () {
		if (emitterNum != 0 && !spellCreator.creatingSpell) { // If the emitter is active and the user is not creating a spell
			if (onRelease) { // If the emitter is activated when the user releases a button
				if (spellInputReceiver.inputSD [emitterNum - 1]) { // If the user has pressed the required button
					SpawnReticle (reticle, out tempReticle);
				}
                if (spellInputReceiver.inputSU [emitterNum - 1]) { // If the user has released the required button
					if (tempReticle) { // If there is a temp reticle active
						Destroy (tempReticle); // Destroys the reticle
						tempReticle = null; // Gets rid of the reference
					}
                    Spawn (objectToSpawn);
				}
			} else {
                if (spellInputReceiver.inputSD [emitterNum - 1]) {  // If the user has pressed the required button
					Spawn (objectToSpawn);
				}
			}
		}
		if (!canSpawn) {
            cooldownValue -= Time.deltaTime;
            cooldownWheel.value = cooldownValue;
		}
	}

	void Spawn (GameObject spawnObject) { // Creates and parents the objet
		if (canSpawn) {
			if (parent != null) {
				Instantiate (spawnObject, parent); // Spawns the object as a parent of a transform
			} else {
				Instantiate (spawnObject, transform.position, transform.rotation, spellsParent); // Spawns the object as a parent of a transform
			}
			StartCoroutine (WaitToSpawn ());
		}
	}

	void SpawnReticle (GameObject spawnObject, out GameObject objectReference) { // Spawns the reticle and creates a reference
        if (tempReticle) {
            Destroy(tempReticle);
        }
        if (canSpawn) {
			objectReference = Instantiate (spawnObject, cursorPosition); // Spawns the object as a parent of a transform
		} else {
			objectReference = null;
		}
	}

	IEnumerator WaitToSpawn () { // Waits before spawning another object
		canSpawn = false;
		cooldownWheel.gameObject.SetActive (true);
        cooldownValue = cooldown;
		cooldownWheel.value = cooldown;
		yield return new WaitForSeconds (cooldown);
		canSpawn = true;
		cooldownWheel.gameObject.SetActive (false);
	}
}
