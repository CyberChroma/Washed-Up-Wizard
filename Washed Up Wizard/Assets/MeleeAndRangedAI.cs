using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAndRangedAI : MonoBehaviour {

    [System.Serializable]
    public class Boundaries { // The box the enemy can move in
        public float minx = -20;
        public float maxx = 20;
        public float miny = -20;
        public float maxy = 20;
    }

    public float moveDelay = 2; // The delay between moves
    public float moveSensitivity = 0.1f; // Used to make the enemy movement less snappy
    public float attackDelay = 1; // The delay between attacking
    public float attackTime = 0.5f; // How long the enemy will be attacking
    public bool fallStart = false;
    public float fallForce = 10;
    public float height = 0;
    public float radius = 20;
    public Boundaries boundaries; // Reference to the boundaries (above)
    public GameObject[] emitters; // Reference to the enemy's projectile emitters
    public Transform targetLocation; // The location the enemy is moving towards

    private bool active = false;
    private Animator anim; // Reference to the animator
    private Health health;
    private MoveByConstantSpeed enemyMove; // Reference to the move script
    private MoveByForce rEnemyMove; // Reference to the move script
    private Transform player; // Reference to the player
    private float moveSpeed;
    private bool canMove = true; // Whether the player can move
    private DamageByTouchCollision damageByTouchCollision;
    private float moveForce;


	void Awake () {
        enemyMove = GetComponent<MoveByConstantSpeed> (); // Getting the referenc
        rEnemyMove = GetComponent<MoveByForce> (); // Getting the reference
        anim = GetComponentInChildren<Animator> (); // Getting the reference
        health = GetComponent<Health> ();
        damageByTouchCollision = GetComponent<DamageByTouchCollision>();
        player = GameObject.Find ("Player").transform; // Getting the reference
	}

    void OnEnable () {
        if (fallStart) {
            moveForce = rEnemyMove.force;
            rEnemyMove.force = fallForce;
            rEnemyMove.dir = Vector3.down * fallForce;
            active = false;
        } else {
            active = true;
        }
        foreach (GameObject emitter in emitters) { // Cycles through each emitter
            emitter.SetActive (false); // Sets it inactive
        }
        targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy)); // Setting the object move location to a random location
        if (anim) {
            anim.speed = Random.Range(0.9f, 1.1f);
        }
    }
	
	
	void FixedUpdate () {
        if (active) { 
            if (canMove) { // If the enemy can move
                if (Vector3.Distance(transform.position, new Vector3(targetLocation.position.x, transform.position.y, targetLocation.position.z)) <= 1f) { // If the enemy has reached its target location
                    enemyMove.dir = Vector3.zero; // Sets the speed to 0
                    StartCoroutine(Attack());
                    StartCoroutine(ChangeTarget());
                }
                else {
                    Vector3 dir = targetLocation.position - transform.position; // Sets its direction
                    dir = new Vector3(dir.x, targetLocation.position.y, dir.z); // Elimintates y value
                    if (dir.magnitude < 1) { // if the magniude is less than 1 (For better smoothing)
                        enemyMove.dir = dir;
                    }
                    else { // If the magnitude is greater than 1
                        enemyMove.dir = dir.normalized; // Sets the magnitude to 1
                    }
                }
            }
            if (player) { // If the player is in the scene (Not destroyed)
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.5f); // Looks at the player
                transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
            }
        }
        if (active && player && (player.position.x > transform.position.x - radius && player.position.x < transform.position.x + radius && player.position.z > transform.position.z - radius && player.position.z < transform.position.z + radius)) { 
            Vector3 dir = player.position - transform.position; // Sets its direction
            dir = new Vector3(dir.x, 0, dir.z); // Elimintates y value
            enemyMove.dir = Vector3.Lerp (enemyMove.dir, dir.normalized, 0.25f); // Sets the magnitude to 1
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.5f); // Looks at the player
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
        }
        else if (fallStart && Vector3.Distance(transform.position, new Vector3(transform.position.x, height, transform.position.z)) < 1f) { 
            active = true;
            enemyMove.speed = moveSpeed;
            enemyMove.dir = Vector3.zero;
        }
        else {
            enemyMove.dir = Vector3.Lerp (enemyMove.dir, Vector3.zero, 0.25f);
        }
        if (health.currentHealth <= 0) {
            enemyMove.dir = Vector3.zero;
            damageByTouchCollision.canDamage = false;
            enabled = false;
            health.ChangeHealth();
        }
	}

    IEnumerator Attack () {
        anim.SetTrigger ("Attack"); // Plays the attack animation
        yield return new WaitForSeconds (attackDelay); // Waits...
        foreach (GameObject emitter in emitters) { // Goes through each emitter
            emitter.SetActive (true); // Sets the emitter active
        }
        if (attackTime != 0) {
            yield return new WaitForSeconds(attackTime); // Waits...
            foreach (GameObject emitter in emitters) { // Goes through each emitter
                emitter.SetActive(false); // Sets the emitter inactive
            }
        }
    }

    IEnumerator ChangeTarget () {
        canMove = false; 
        yield return new WaitForSeconds (moveDelay); // Waits...
        targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy)); // Sets the location to a random point
        canMove = true;
    }
}
