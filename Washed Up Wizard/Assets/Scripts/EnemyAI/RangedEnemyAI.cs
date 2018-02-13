using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour {

    // This enemy's attack pattern is to move, shoot a progrctile, then move again

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
    public Boundaries boundaries; // Reference to the boundaries (above)
	public GameObject[] emitters; // Reference to the enemy's projectile emitters
	public Transform targetLocation; // The location the enemy is moving towards

	private Animator anim; // Reference to the animator
	private MoveByForce enemyMove; // Reference to the move script
	private Transform player; // Reference to the player
	private bool canMove = true; // Whether the player can move

	// Use this for initialization
	void Awake () {
		enemyMove = GetComponent<MoveByForce> (); // Getting the reference
		anim = GetComponent<Animator> (); // Getting the reference
		player = GameObject.Find ("Player").transform; // Getting the reference
	}

	void OnEnable () {
		foreach (GameObject emitter in emitters) { // Cycles through each emitter
			emitter.SetActive (false); // Sets it inactive
		}
		targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy)); // Setting the object move location to a random location
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) { // If the enemy can move
            if (Vector3.Distance (transform.position, new Vector3 (targetLocation.position.x, transform.position.y, targetLocation.position.z)) <= 1f) { // If the enemy has reached its target location
				enemyMove.dir = Vector3.zero; // Sets the speed to 2
				StartCoroutine (Attack ());
				StartCoroutine (ChangeTarget ());
			} else {
				Vector3 dir = targetLocation.position - transform.position; // Sets its direction
                dir = new Vector3 (dir.x, targetLocation.position.y, dir.z); // Elimintates y value
                if (dir.magnitude < 1) { // if the magniude is less than 1 (For better smoothing)
					enemyMove.dir = dir;
				} else { // If the magnitude is greater than 1
					enemyMove.dir = dir.normalized; // Sets the magnitude to 1
				}
			}
		}
        if (player) { // If the player is in the scene (Not destroyed)
			Quaternion targetRotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 0.5f); // Looks at the player
			transform.rotation = Quaternion.Euler (0, targetRotation.eulerAngles.y, 0); // Ignores x and z values
		}
	}

	void OnCollisionEnter (Collision other) { // If the enemy collided with something, change its move target.
		enemyMove.dir = Vector3.zero;
		StartCoroutine (Attack ());
		StartCoroutine (ChangeTarget ());
	}

	IEnumerator Attack () {
		anim.SetTrigger ("Attack"); // Plays the attack animation
		yield return new WaitForSeconds (attackDelay); // Waits...
		foreach (GameObject emitter in emitters) { // Goes through each emitter
			emitter.SetActive (true); // Sets the emitter active
		}
        if (attackTime != 0)
        {
            yield return new WaitForSeconds(attackTime); // Waits...
            foreach (GameObject emitter in emitters)
            { // Goes through each emitter
                emitter.SetActive(false); // Sets the emitter inactive
            }
        }
	}

	IEnumerator ChangeTarget () {
		canMove = false; 
		yield return new WaitForSeconds (moveDelay); // Waits...
        while (true) {
		    targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy)); // Sets the location to a random point
            if (!Physics.Linecast (transform.position, targetLocation.position)) {
                break;
            }
        }
        canMove = true;
	}
}
