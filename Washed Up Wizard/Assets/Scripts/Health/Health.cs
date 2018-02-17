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
    public bool disableOnDeath = false;
    public float disableDelay = 1;

	[HideInInspector] public bool healthChanged; // UI updates when true
	[HideInInspector] public float currentHealth; // The current health
	[HideInInspector] public bool canBeHit = true; // Whether the object can be hit
    private bool dead = false;
    private bool healingOverTime = false; // Whether the object is currently taking damage over time
    private float currentHealPerSecond = 0; // The amount of damage the object is taking per second

    private Animator anim;
	private AudioSource audioSource; // Reference to the sound player

	// Use this for initialization
	void Start () {
		// Setting start values
		currentHealth = startHealth;
        anim = GetComponentInChildren<Animator>();
	}

	public void ChangeHealth () {
		healthChanged = true; // Updates the UI
        if (currentHealth <= 0 && !dead) { // If the object has no health left
			if (audioClip) { // If the audio clip is not null
				audioSource = Instantiate (soundPlayer, transform.position, Quaternion.identity).GetComponent<AudioSource> (); // Creates the sound player and gets the reference to the audio source
			}
			audioSource.volume = volume; // Sets the volume
			audioSource.clip = audioClip; // Sets the clip
			audioSource.Play (); // Plays the sound
            if (anim) {
                anim.SetTrigger("Death");
            }
            if (disableOnDeath) {
                StartCoroutine (Disable ());
            }
            dead = true;
		}
	}

    void Update () {
        if (healingOverTime) {
            HealOverTime (currentHealPerSecond * Time.deltaTime); // Damage them by the damage per second
        }
    }
    public void HealOverTimeStart (float healthPerSecond, float duration) { // Makes the object start healing over a period of time
        if (currentHealPerSecond == 0) { // If the object isn't healing
            currentHealPerSecond = healthPerSecond; // Setting value 
            StartCoroutine (StopHealOverTime (duration)); // Starts the duration
        }
    }

    void HealOverTime (float healAmount) {
        currentHealth += healAmount; // Gain health
        if (currentHealth > startHealth) { // If the health is greater than the max health
            currentHealth = startHealth; // Sets the health to the max health
        }
        ChangeHealth (); // Tests if the object is out of health
    }

    IEnumerator StopHealOverTime (float duration) { // Waits a duration before stopping the heal over time
        healingOverTime = true;
        yield return new WaitForSeconds (duration);
        currentHealPerSecond = 0;
        healingOverTime = false;
    }

	public IEnumerator TempStopHits () { // Temporarily stops the object from taking more damage
		canBeHit = false;
		yield return new WaitForSeconds (tempStopHitsTime);
		canBeHit = true;
	}

    IEnumerator Disable () {
        yield return new WaitForSeconds(disableDelay);
        gameObject.SetActive(false);
    }
}
