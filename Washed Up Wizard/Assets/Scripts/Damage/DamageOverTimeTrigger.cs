﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeTrigger : MonoBehaviour {

    public Type type;
	public float totalDamage = 5; // The total amount of damage to give
	public float duration = 3; // The time over which to deal the damage
	public bool oneTime = false; // Whether to only damage once
	public string[] tagsToDamage; // The tags to damage

	private bool damaged = false; // When this script has dealt damage (For oneTime)
    private TakeDamage takeDamage;

	void OnTriggerEnter (Collider other) {
        if (oneTime && !damaged) { // If this script only deals damage once and it hasn't yet
            DamageOverTime (other);
        }
	}

	void OnTriggerStay (Collider other) {
		if (!oneTime) { // If this script doesn't only deals damage once
            DamageOverTime (other);
		}
	}

    void DamageOverTime(Collider other)
    {
        foreach (string tag in tagsToDamage)
        { // Goes through each tag it can damage
            if (other.GetComponent<Collider>().CompareTag(tag))
            { // If the collided object is something that can be damaged
                if (other.GetComponent<TakeDamage>())
                {
                    takeDamage = other.GetComponent<TakeDamage>();
                    if ((type == Type.Fire && takeDamage.type == Type.Fire) || (type == Type.Mechanical && takeDamage.type == Type.Mechanical) || (type == Type.Toxic && takeDamage.type == Type.Toxic) || (type == Type.Earth && takeDamage.type == Type.Earth) || (type == Type.Electrical && takeDamage.type == Type.Electrical) || (type == Type.Water && takeDamage.type == Type.Water))
                    {
                        takeDamage.DamageOverTimeStart(totalDamage * 0.1f / duration, duration); // Sends the damage
                    }
                    else if ((type == Type.Fire && takeDamage.type == Type.Mechanical) || (type == Type.Mechanical && takeDamage.type == Type.Toxic) || (type == Type.Toxic && takeDamage.type == Type.Earth) || (type == Type.Earth && takeDamage.type == Type.Electrical) || (type == Type.Electrical && takeDamage.type == Type.Water) || (type == Type.Water && takeDamage.type == Type.Fire))
                    {
                        takeDamage.DamageOverTimeStart(totalDamage * 2 / duration, duration); // Sends the damage
                    }
                    else if ((type == Type.Fire && takeDamage.type == Type.Water) || (type == Type.Mechanical && takeDamage.type == Type.Fire) || (type == Type.Toxic && takeDamage.type == Type.Mechanical) || (type == Type.Earth && takeDamage.type == Type.Toxic) || (type == Type.Electrical && takeDamage.type == Type.Earth) || (type == Type.Water && takeDamage.type == Type.Electrical))
                    {
                        takeDamage.DamageOverTimeStart(totalDamage / 2 / duration, duration); // Sends the damage
                    }
                    else
                    {
                        takeDamage.DamageOverTimeStart(totalDamage / duration, duration); // Sends the damage
                    }
                    if (oneTime)
                    {
                        damaged = true; // Setting the bool
                    }
                }
            }
        }
    }
}           
