using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByTouchCollision : MonoBehaviour {

	public float damageAmount = 2; // Damage amount
    public bool oneTime = true; // Whether the object can only damage once
	public string[] tagsToDamage; // Tags of objects that can be damaged
    public bool push = true; // Whether the object is pushed away when damaged (explosion force)
	public float pushForce = 4; // The force to push the object away

    [HideInInspector] public bool canDamage = true;
	private bool damaged = false; // Whether the object has damaged something at least once (For oneTime)

	void OnCollisionEnter (Collision other) {
		if (oneTime && !damaged) { // If the object can only damage something once and hasn't yet
			Damage (other);
        } else if(!oneTime) {
            Damage(other);
        }
	}

	void OnCollisionStay (Collision other) {
		if (!oneTime) { // If the object can damage multiple times
			Damage (other);
		}
	}

	void Damage (Collision other) {
        if (canDamage) {
            foreach (string damageTag in tagsToDamage) { // Goes through each tag
                if (other.collider.CompareTag(damageTag)) { // If the object is something this can damage
                    if (other.collider.GetComponent<TakeDamage>()) {
                        other.collider.GetComponent<TakeDamage>().Damage(damageAmount); // Sends the damage
                    }
                    if (push) { // If the object should be pushed
                        Vector3 dir = (other.transform.position - transform.position);
                        dir = new Vector3(dir.x, 0, dir.z).normalized;
                        other.collider.GetComponent<Rigidbody>().AddForce(dir * pushForce, ForceMode.Impulse); // Pushes the object
                    }
                }
            }
        }
	}
}
