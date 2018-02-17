﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTargetLerp : MonoBehaviour {

	public float smoothing = 0.5f; // Smoothing applied to following
	public Rigidbody target; // The target to follow
    public string targetString;

	private Rigidbody rb; // This object's rigidbody

	void Awake () {
		rb = GetComponent<Rigidbody> (); // Getting the reference
        if (!target && targetString != string.Empty) {
            target = GameObject.Find(targetString).GetComponent<Rigidbody>();
        }
	}

	void FixedUpdate () {
		if (target) {
			rb.MovePosition (Vector3.Lerp (rb.position, target.position, smoothing / 10)); // Follows the target
		}
	}
}
