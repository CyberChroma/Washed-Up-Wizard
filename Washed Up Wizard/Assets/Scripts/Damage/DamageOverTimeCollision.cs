using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeCollision : MonoBehaviour {

    public Type type;
	public float totalDamage = 5; // The total amount of damage to give
	public float duration = 3; // The time over which to deal the damage
	public bool oneTime = false; // Whether to only damage once
	public string[] tagsToDamage; // The tags to damage

	private bool damaged = false; // When this script has dealt damage (For oneTime)
    private TakeDamage takeDamage;

	void OnCollisionEnter (Collision other) {
		if (oneTime && !damaged) { // If this script only deals damage once and it hasn't yet
            DamageOverTime (other);
		}
	}

	void OnCollisionStay (Collision other) {
		if (!oneTime) { // If this script doesn't only deals damage once
            DamageOverTime (other);
		}
	}

    void DamageOverTime (Collision other) {
        foreach (string tag in tagsToDamage) { // Goes through each tag it can damage
            if (other.collider.CompareTag(tag))
            { // If the collided object is something that can be damaged
                if (other.collider.GetComponent<TakeDamage>())
                {
                    takeDamage = other.collider.GetComponent<TakeDamage>();
                    if ((type == Type.Fire && takeDamage.type == Type.Fire) || (type == Type.Mechanical && takeDamage.type == Type.Mechanical) || (type == Type.Toxic && takeDamage.type == Type.Toxic) || (type == Type.Earth && takeDamage.type == Type.Earth) || (type == Type.Electrical && takeDamage.type == Type.Electrical) || (type == Type.Water && takeDamage.type == Type.Water))
                    {
                        takeDamage.Damage(0); // Sends the damage
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
