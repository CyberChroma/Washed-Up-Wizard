using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeCollision : MonoBehaviour {

	public float totalDamage = 5; // The total amount of damage to give
	public float duration = 3; // The time over which to deal the damage
	public bool oneTime = false; // Whether to only damage once
	public string[] tagsToDamage; // The tags to damage

	private bool damaged = false; // When this script has dealt damage (For oneTime)

	void OnCollisionEnter (Collision other) {
		if (oneTime && !damaged) { // If this script only deals damage once and it hasn't yet
			foreach (string tag in tagsToDamage) { // Goes through each tag it can damage
				if (other.collider.CompareTag (tag)) { // If the collided object is something that can be damaged
					other.collider.GetComponent<TakeDamage> ().DamageOverTimeStart (totalDamage / duration, duration); // Sends the over time damage
					damaged = true; // Setting the bool
				}
			}
		}
	}

	void OnCollisionStay (Collision other) {
		if (!oneTime) { // If this script doesn't only deals damage once
			foreach (string tag in tagsToDamage) { // Goes through each tag it can damage
				if (other.collider.CompareTag (tag)) { // If the collided object is something that can be damaged
					other.collider.GetComponent<TakeDamage> ().DamageOverTimeStart (totalDamage / duration, duration); // Sends the over time damage
				}
			}
		}
	}
}
