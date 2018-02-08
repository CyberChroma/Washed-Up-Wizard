using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public GameObject parent; // The object parent (to destroy)
	public float startHealth = 10; // The health the object starts with
	public float tempStopHitsTime = 2; // Time between hits
	public AudioClip audioClip; // The death sound
	public GameObject soundPlayer; // The object that plays the sound
	public float volume = 0.5f; // The volume of the sound

	[HideInInspector] public bool healthChanged; // UI updates when true
	[HideInInspector] public float currentHealth; // The current health
	[HideInInspector] public bool canBeHit = true; // Whether the object can be hit

	private AudioSource audioSource; // Reference to the sound player

	// Use this for initialization
	void Start () {
		// Setting start values
		currentHealth = startHealth;
	}

	public void ChangeHealth () {
		healthChanged = true; // Updates the UI
		if (currentHealth <= 0) { // If the object has no health left
			if (audioClip) { // If the audio clip is not null
				audioSource = Instantiate (soundPlayer, transform.position, Quaternion.identity).GetComponent<AudioSource> (); // Creates the sound player and gets the reference to the audio source
			}
			audioSource.volume = volume; // Sets the volume
			audioSource.clip = audioClip; // Sets the clip
			audioSource.Play (); // Plays the sound
			// Destroying gameobject (Temporary)
			Destroy (parent);
		}
	}

	public IEnumerator TempStopHits () { // Temporarily stops the object from taking more damage
		canBeHit = false;
		yield return new WaitForSeconds (tempStopHitsTime);
		canBeHit = true;
	}
}
