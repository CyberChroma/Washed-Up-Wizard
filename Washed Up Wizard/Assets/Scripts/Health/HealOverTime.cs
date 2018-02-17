using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealOverTime : MonoBehaviour {

	public float totalHeal = 2; // The amount to heal
    public float duration = 2;

	private Health health; // Reference to the health script

	// Use this for initialization
	void Awake () {
		health = GetComponentInParent<Health> (); // Getting the reference
	}

	void OnEnable () {
        health.HealOverTimeStart(totalHeal / duration, duration);
	}
}
