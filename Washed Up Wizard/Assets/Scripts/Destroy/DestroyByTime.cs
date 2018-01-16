using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour {

	public float lifetime = 5; // Time before object destroys itself

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime); // Object destroys itself after a delay
	}
}
