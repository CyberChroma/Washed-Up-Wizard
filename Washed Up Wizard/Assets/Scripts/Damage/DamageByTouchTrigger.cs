using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByTouchTrigger : MonoBehaviour {

	public float damageAmount = 2; // Damage amount
	public bool oneTime = false; // Whether the object can only damage once
	public string[] tagsToDamage; // Tags of objects that can be damaged
    public bool push = true; // Whether the object is pushed away when damaged (explosion force)
    public float pushForce = 5; // The force to push the object away

	private bool damaged = false; // Whether the object has damaged something at least once (For oneTime)

	void OnTriggerEnter (Collider other) {
		if (oneTime && !damaged) { // If the object can only damage something once and hasn't yet
			Damage (other);
		}
	}

	void OnTriggerStay (Collider other) {
		if (!oneTime) { // If the object can damage multiple times
			Damage (other);
		}
	}

	void Damage (Collider other) {
		foreach (string damageTag in tagsToDamage) { // Goes through each tag
			if (other.CompareTag (damageTag)) { // If the object is something this can damage
				if (other.GetComponent<TakeDamage> ()) {
					other.GetComponent<TakeDamage> ().Damage (damageAmount); // Sends the damage
				}
                if (push) { // If the object should be pushed
                    other.GetComponent<Rigidbody> ().AddForce ((other.transform.position - transform.position) * pushForce, ForceMode.Impulse); // Pushes the object
				}
			}
		}
	}
}
