using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemyAI : MonoBehaviour {

    // This enemy's attack pattern is to move towards the player shooting a projectile, but stay a certain distance,=

	public float moveDelay = 2; // The delay between moves
    public float timeBeforeAttack = 0.5f; // The delay before attacking
    public float timeBetweenAttacks = 2; // The delay between attacking
    public float maxDistance = 10;
    public float minDistance = 5;
    public float radius = 30;
	public GameObject[] emitters; // Reference to the enemy's projectile emitters

    private NavMeshAgent nav;
    private Animator anim; // Reference to the animator
    private Health health;
	private Transform player; // Reference to the player
	private bool canMove = true; // Whether the player can move
    private bool canAttack = true;

	// Use this for initialization
	void Awake () {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator> (); // Getting the reference
        health = GetComponent<Health> ();
		player = GameObject.Find ("Player").transform; // Getting the reference
	}

	void OnEnable () {
		foreach (GameObject emitter in emitters) { // Cycles through each emitter
			emitter.SetActive (false); // Sets it inactive
		}
        if (anim) {
            anim.speed = Random.Range(0.9f, 1.1f);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (player && (player.position.x > transform.position.x - radius && player.position.x < transform.position.x + radius && player.position.z > transform.position.z - radius && player.position.z < transform.position.z + radius))
        { 
            if (canMove)
            { // If the enemy can move
                if (Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z)) <= minDistance)
                {
                    Debug.DrawRay (transform.position, (transform.position - player.position).normalized, Color.blue); 
                    nav.destination = (transform.position + (transform.position - player.position).normalized) ; 
                }
                else if (Vector3.Distance(transform.position, new Vector3(player.position.x, transform.position.y, player.position.z)) <= maxDistance)
                {
                    nav.destination = Vector3.Lerp (nav.destination, new Vector3 (transform.position.x + (Random.Range (-1, 2) * 5), transform.position.y, transform.position.z + (Random.Range (-1, 2) * 5)), 0.1f);
                }
                else
                {
                    nav.destination = player.position;
                }
            }
            if (canAttack)
            {
                StartCoroutine (WaitToAttack ());
                canAttack = false;
            }
        }
        else
        {
            nav.destination = transform.position;
        }
        if (player) { // If the player is in the scene (Not destroyed)
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.5f); // Looks at the player
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
        }
        if (health.currentHealth <= 0) {
            enabled = false;
            health.ChangeHealth();
            nav.destination = transform.position;
        }
	}

    IEnumerator WaitToAttack () {
        anim.SetTrigger("Attack"); // Plays the attack animation
        yield return new WaitForSeconds(timeBeforeAttack); // Waits...
		foreach (GameObject emitter in emitters) { // Goes through each emitter
			emitter.SetActive (true); // Sets the emitter active
		}
        yield return new WaitForSeconds(timeBetweenAttacks); // Waits...
        canAttack = true;
	}
}
