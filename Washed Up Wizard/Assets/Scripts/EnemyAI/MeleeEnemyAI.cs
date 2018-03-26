using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour {

    // This enemy's attack pattern is to run at the player and attack them
	public float moveSensitivity = 0.1f; // Used to make the enemy movement less snappy
    public bool fallStart = false;
    public float fallSpeed = 10;
    public float height = 0;
    public float radius = 20;

    private bool active = false;
	private Animator anim; // Reference to the animator
    private Health health;
    private MoveByConstantSpeed moveByConstantSpeed; // Reference to the move script
    private MoveByForce moveByForce;
	private Transform player; // Reference to the player
    private float moveSpeed;
    private DamageByTouchCollision damageByTouchCollision;
    private NavMeshAgent nav;

	// Use this for initialization
	void Awake () {
        moveByConstantSpeed = GetComponent<MoveByConstantSpeed> (); // Getting the reference
        moveByForce = GetComponent<MoveByForce> (); // Getting the reference
        anim = GetComponentInChildren<Animator> (); // Getting the reference
        health = GetComponent<Health> ();
        damageByTouchCollision = GetComponent<DamageByTouchCollision>();
		player = GameObject.Find ("Player").transform; // Getting the reference
        nav = GetComponent<NavMeshAgent>();
	}

	void OnEnable () {
        if (fallStart) {
            if (moveByConstantSpeed)
            {
                moveSpeed = moveByConstantSpeed.speed;
                moveByConstantSpeed.speed = fallSpeed;
                moveByConstantSpeed.dir = Vector3.down * fallSpeed;
            }
            else if (moveByForce)
            {
                moveSpeed = moveByForce.force;
                moveByForce.force = fallSpeed;
                moveByForce.dir = Vector3.down * fallSpeed;
            }
            active = false;
        } else {
            active = true;
        }
        if (anim) {
            anim.speed = Random.Range(0.9f, 1.1f);
        }
	}

	// Update is called once per frame
	void FixedUpdate () {
        if (active && player && (player.position.x > transform.position.x - radius && player.position.x < transform.position.x + radius && player.position.z > transform.position.z - radius && player.position.z < transform.position.z + radius)) { 
            nav.destination = player.position;
            Vector3 dir = player.position - transform.position; // Sets its direction
            dir = new Vector3(dir.x, 0, dir.z); // Elimintates y value
            if (moveByConstantSpeed)
            {
                moveByConstantSpeed.dir = Vector3.Lerp (moveByConstantSpeed.dir, dir.normalized, 0.25f); // Sets the magnitude to 1

            }
            else if (moveByForce)
            {
                moveByForce.dir = Vector3.Lerp (moveByForce.dir, dir.normalized, 0.25f); // Sets the magnitude to 1
            }
            if (nav.velocity != Vector3.zero) {
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(nav.velocity), 0.5f); // Looks at the player
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
            }
        }
        else if (fallStart && Vector3.Distance(transform.position, new Vector3(transform.position.x, height, transform.position.z)) < 1f) { 
            active = true;
            if (moveByConstantSpeed)
            {
                moveByConstantSpeed.speed = moveSpeed;
                moveByConstantSpeed.dir = Vector3.zero;
            }
            else if (moveByForce)
            {
                moveByForce.force = moveSpeed;
                moveByForce.dir = Vector3.zero;
            }
        }
        else {
            if (moveByConstantSpeed)
            {
                moveByConstantSpeed.dir = Vector3.Lerp(moveByConstantSpeed.dir, Vector3.zero, 0.25f);
            }
            else if (moveByForce)
            {
                moveByForce.dir = Vector3.Lerp(moveByForce.dir, Vector3.zero, 0.25f);
            }
        }
        if (health.currentHealth <= 0) {
            if (moveByConstantSpeed)
            {
                moveByConstantSpeed.dir = Vector3.zero;
            }
            else if (moveByForce)
            {
                moveByForce.dir = Vector3.zero;
            }
            damageByTouchCollision.canDamage = false;
            nav.destination = transform.position;
            enabled = false;
            health.ChangeHealth();
        }
	}

	void OnCollisionEnter (Collision other) { // If the enemy collided with something, change its move target.
        if (other.collider.CompareTag ("Player") && anim) {
            anim.SetTrigger("Attack");
        }
	}
}
