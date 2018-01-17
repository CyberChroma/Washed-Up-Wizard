using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	public GameObject parent;
	public float startHealth = 10; // The health the object starts with
	public float tempStopHitsTime = 2; // Time between hits
	public AudioClip audioClip;
	public GameObject soundPlayer;
	public float volume = 0.5f;

	[HideInInspector] public bool healthChanged; // UI updates when true
	[HideInInspector] public float currentHealth; // The current health
	[HideInInspector] public bool canBeHit = true;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		// Setting start values
		currentHealth = startHealth;
	}

	public void ChangeHealth () {
		healthChanged = true; // Updates the UI
		if (currentHealth <= 0) { // If the object has no health left
			if (audioClip) {
				audioSource = Instantiate (soundPlayer, transform.position, Quaternion.identity).GetComponent<AudioSource> ();
			}
			audioSource.volume = volume;
			audioSource.clip = audioClip;
			audioSource.Play ();
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
