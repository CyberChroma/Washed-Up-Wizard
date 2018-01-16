using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByTouchCollision : MonoBehaviour {

	public float damageAmount = 2; // Damage amount
	public bool oneTime = false; // Whether the object can only damage once
	public string[] tagsToDamage; // Tags of objects that can be damaged
	public bool push = true;
	public float pushForce = 5;

	private bool damaged = false; // Whether the object has damaged something at least once (For oneTime)

	void OnCollisionEnter (Collision other) {
		if (oneTime && !damaged) { // If the object can only damage something once and hasn't yet
			Damage (other);
		}
	}

	void OnCollisionStay (Collision other) {
		if (!oneTime) { // If the object can damage multiple times
			Damage (other);
		}
	}

	void Damage (Collision other) {
		foreach (string damageTag in tagsToDamage) { // Goes through each tag
			if (other.collider.CompareTag (damageTag)) { // If the object is something this can damage
				if (other.collider.GetComponent<TakeDamage> ()) {
					other.collider.GetComponent<TakeDamage> ().Damage (damageAmount); // Sends the damage
				}
				if (push) {
					other.collider.GetComponent<Rigidbody> ().AddForce ((other.transform.position - transform.position) * pushForce, ForceMode.Impulse);
				}
			}
		}
	}
}
