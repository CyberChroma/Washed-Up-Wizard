using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeTrigger : MonoBehaviour {

	public float totalDamage = 5; // The total ammount of damage to give
	public float duration = 3; // The time over which to deal the damage
	public bool oneTime = false; // Whether to only damage once
	public string[] tagsToDamage; // The tags to damage

	private bool damaged = false; // When this script has dealt damage (For oneTime)

	void OnTriggerEnter (Collider other) {
		if (oneTime && !damaged) { // If this script only deals damage once and it hasn't yet
			foreach (string tag in tagsToDamage) { // Goes through each tag it can damage
				if (other.CompareTag (tag)) { // If the collided object is something that can be damaged
					other.GetComponent<TakeDamage> ().DamageOverTimeStart (totalDamage / duration, duration); // Sends the over time damage
					damaged = true;
				}
			}
		}
	}

	void OnTriggerStay (Collider other) {
		if (!oneTime) { // If this script doesn't only deals damage once
			foreach (string tag in tagsToDamage) { // Goes through each tag it can damage
				if (other.CompareTag (tag)) { // If the collided object is something that can be damaged
					other.GetComponent<TakeDamage> ().DamageOverTimeStart (totalDamage / duration, duration); // Sends the over time damage
				}
			}
		}
	}
}
