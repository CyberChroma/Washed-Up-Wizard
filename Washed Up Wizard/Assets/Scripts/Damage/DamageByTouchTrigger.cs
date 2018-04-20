﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByTouchTrigger : MonoBehaviour {

    public Type type;
	public float damageAmount = 2; // Damage amount
	public bool oneTime = false; // Whether the object can only damage once
	public string[] tagsToDamage; // Tags of objects that can be damaged
    public bool push = true; // Whether the object is pushed away when damaged (explosion force)
    public float pushForce = 1; // The force to push the object away

	private bool damaged = false; // Whether the object has damaged something at least once (For oneTime)
    private TakeDamage takeDamage;

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
                    takeDamage = other.GetComponent<Collider>().GetComponent<TakeDamage>();
                    if ((type == Type.Fire && takeDamage.type == Type.Fire) || (type == Type.Mechanical && takeDamage.type == Type.Mechanical) || (type == Type.Toxic && takeDamage.type == Type.Toxic) || (type == Type.Earth && takeDamage.type == Type.Earth) || (type == Type.Electrical && takeDamage.type == Type.Electrical) || (type == Type.Water && takeDamage.type == Type.Water)) {
                        takeDamage.Damage(damageAmount * 0.1f); // Sends the damage
                    } else if ((type == Type.Fire && takeDamage.type == Type.Mechanical) || (type == Type.Mechanical && takeDamage.type == Type.Toxic) || (type == Type.Toxic && takeDamage.type == Type.Earth) || (type == Type.Earth && takeDamage.type == Type.Electrical) || (type == Type.Electrical && takeDamage.type == Type.Water) || (type == Type.Water && takeDamage.type == Type.Fire)) {
                        takeDamage.Damage(damageAmount * 2); // Sends the damage
                    } else if ((type == Type.Fire && takeDamage.type == Type.Water) || (type == Type.Mechanical && takeDamage.type == Type.Fire) || (type == Type.Toxic && takeDamage.type == Type.Mechanical) || (type == Type.Earth && takeDamage.type == Type.Toxic) || (type == Type.Electrical && takeDamage.type == Type.Earth) || (type == Type.Water && takeDamage.type == Type.Electrical)) {
                        takeDamage.Damage(damageAmount / 2); // Sends the damage
                    } else {
                        takeDamage.Damage(damageAmount); // Sends the damage
                    }
                }
                if (push) { // If the object should be pushed
                    Vector3 dir = (other.transform.position - transform.position);
                    dir = new Vector3(dir.x, 0, dir.z).normalized;
                    if (other.GetComponent<Rigidbody> ()) {
                        other.GetComponent<Rigidbody> ().AddForce (dir * pushForce, ForceMode.Impulse); // Pushes the object
                    }
				}
                if (oneTime) {
                    damaged = true;
                }
			}
		}
	}
}
