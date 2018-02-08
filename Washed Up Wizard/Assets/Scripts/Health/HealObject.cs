using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour {

	public float healAmount = 2; // The amount to heal

	private Health health; // Reference to the health script

	// Use this for initialization
	void Awake () {
		health = GetComponentInParent<Health> (); // Getting the reference
	}

	void OnEnable () {
		Heal ();
	}

	public void Heal () {
		health.currentHealth += healAmount; // Increases the health
		if (health.currentHealth > health.startHealth) { // If the health is greater than the max health
			health.currentHealth = health.startHealth; // Sets the health to the max health
		}
		health.ChangeHealth (); // Tests if the object is out of health
	}
}
