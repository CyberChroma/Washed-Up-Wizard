﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour {

    // This enemy's attack pattern is to run at the player and attack them
	public float moveSensitivity = 0.1f; // Used to make the enemy movement less snappy
    public float radius = 20;

	private Animator anim; // Reference to the animator
    private Health health;
	private Transform player; // Reference to the player
    private DamageByTouchCollision damageByTouchCollision;
    private NavMeshAgent nav;

	// Use this for initialization
	void Awake () {
        anim = GetComponentInChildren<Animator> (); // Getting the reference
        health = GetComponent<Health> ();
        damageByTouchCollision = GetComponent<DamageByTouchCollision>();
		player = GameObject.Find ("Player").transform; // Getting the reference
        nav = GetComponent<NavMeshAgent>();
        anim.logWarnings = false;
	}

	void OnEnable () {
        if (anim) {
            anim.speed = Random.Range(0.9f, 1.1f);
        }
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (player && (player.position.x > transform.position.x - radius && player.position.x < transform.position.x + radius && player.position.z > transform.position.z - radius && player.position.z < transform.position.z + radius)) { 
            nav.destination = player.position;
            if (nav.velocity != Vector3.zero) {
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nav.velocity), 0.5f); // Looks at the player
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
            }
        }
        else {
            nav.destination = transform.position;
        }
        if (health.currentHealth <= 0) {
            damageByTouchCollision.canDamage = false;
            nav.destination = transform.position;
            enabled = false;
            health.ChangeHealth();
        }
	}

	void OnCollisionEnter (Collision other) { // If the enemy collided with something, change its move target
        if (other.collider.CompareTag ("Player") && anim) {
            anim.SetTrigger("Attack");
        }
	}
}
