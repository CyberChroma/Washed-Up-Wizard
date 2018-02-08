using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {

	private Health health; // The health script to track
	private bool takingDamageOverTime = false; // Whether the object is currently taking damage over time
	private float currentDamagePerSecond; // The amount of damage the object is taking per second
	private bool canTakeDamage = true; // Whether the object can take damage at this time

	// Use this for initialization
	void Awake () {
		health = GetComponent<Health> (); // Getting the reference
	}

	void OnEnable () {
		canTakeDamage = true; // Setting the bool
	}

	void OnDisable () {
        canTakeDamage = false; // Setting the bool
	}

	// Update is called once per frame
	void Update () {
		if (takingDamageOverTime) { // If the object is taking damage over time
			DamageOverTime (currentDamagePerSecond * Time.deltaTime); // Damage them by the damage per second
		}
	}

	public void Damage (float damageAmount) {
		if (health.canBeHit && canTakeDamage) { // If the object can be hit
			health.currentHealth -= damageAmount; // Lose health
			health.ChangeHealth (); // Tests if the object is out of health
			if (gameObject.activeSelf) {
				StartCoroutine (health.TempStopHits ()); // Temporarily stops the object from taking damage
			}
		}
	}

	public void DamageOverTimeStart (float damagePerSecond, float duration) { // Makes the object start taking damage over a period of time
		if (currentDamagePerSecond == 0 && canTakeDamage) { // If the object isn't taking damage per second and can take damage
			currentDamagePerSecond = damagePerSecond; // Setting value 
			StartCoroutine (StopDamageOverTime (duration)); // Starts the duration
			StartCoroutine (health.TempStopHits ()); // Temporarily stops the object from taking damage
		}
	}

	void DamageOverTime (float damageAmount) {
		health.currentHealth -= damageAmount; // Lose health
		health.ChangeHealth (); // Tests if the object is out of health
	}

	IEnumerator StopDamageOverTime (float duration) { // Waits a duration before stopping the damage over time
		takingDamageOverTime = true;
		yield return new WaitForSeconds (duration);
		currentDamagePerSecond = 0;
		takingDamageOverTime = false;
	}
}
