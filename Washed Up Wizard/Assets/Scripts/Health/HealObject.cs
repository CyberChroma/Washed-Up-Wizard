using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : MonoBehaviour {

	public float healAmount = 2;

	private Health health;

	// Use this for initialization
	void Awake () {
		health = GetComponentInParent<Health> ();
	}

	void OnEnable () {
		Heal ();
	}

	public void Heal () {
		health.currentHealth += healAmount;
		if (health.currentHealth > health.startHealth) {
			health.currentHealth = health.startHealth;
		}
		health.ChangeHealth (); // Tests if the object is out of health
	}
}
