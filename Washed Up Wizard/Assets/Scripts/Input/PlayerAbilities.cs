using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

	public float teleportCooldown = 1;
	public GameObject reticle; // Reference to the retical prefab to spawn
    public GameObject teleportExplosion;

	private bool canTeleport = false; // Whether the player can teleport
	private Transform cursorPosition; // Reference to the cursor position
	private GameObject tempReticle; // The temporary reticle that is spawned
	private SpecialInputReceiver specialInputReceiver; // Reference to input manager
	private AudioSource audioSource; // Reference to the audio source
    private Health health;
    private Transform parent;
    private GameObject oldReticle;

	// Use this for initialization
	void Awake () {
        // Getting references
		cursorPosition = GameObject.Find ("Cursor Position").transform;
		specialInputReceiver = GameObject.Find ("Input Controller").GetComponent <SpecialInputReceiver> ();
		audioSource = GetComponent<AudioSource> ();
        health = GetComponent<Health>();
        parent = GameObject.Find("Spells").transform;
	}

	void OnEnable () {
		StartCoroutine (WaitToTeleport ());
	}

	// Update is called once per frame
	void Update () {
        if (health.currentHealth > 0 && Time.timeScale != 0) {
            if (canTeleport) { // If the player can teleport
                if (specialInputReceiver.inputTD) { // If they press the teleport button down
                    if (tempReticle) {
                        oldReticle = tempReticle;
                        Destroy(oldReticle);
                    }
                    tempReticle = Instantiate(reticle, cursorPosition); // Spawns the object as a parent of a transform
                }
                else if (specialInputReceiver.inputTU && tempReticle != null) { // If they have pressed down and are now releasing the button
                    Destroy(tempReticle); // Destroy the reticle
                    tempReticle = null; // Getting rid of the reference
                    if (!(Physics.Linecast(transform.position, cursorPosition.position, 1 << 13) || Physics.Linecast(transform.position, cursorPosition.position, 1 << 14))) { // Casting a line between the player's position and the teleport position. If it doesn't hit anything
                        Instantiate (teleportExplosion, transform.position, Quaternion.identity, parent);
                        transform.position = cursorPosition.position; // Sets player position to teleport position
                        Instantiate (teleportExplosion, transform.position, Quaternion.identity, parent);
                        audioSource.Play(); // Plays the sound effect
                        StartCoroutine(WaitToTeleport());
                    }
                }
            }
		}
	}

	IEnumerator WaitToTeleport () { // Waits to enable the teleport again
		canTeleport = false;
		yield return new WaitForSeconds (teleportCooldown);
		canTeleport = true;
	}
}
