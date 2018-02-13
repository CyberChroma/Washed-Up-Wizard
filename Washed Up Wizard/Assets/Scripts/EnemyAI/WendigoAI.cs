using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoAI : MonoBehaviour {

	[System.Serializable]
	public class MoveSpeeds	{
		public float toPlayer;
		public float toWaypoint;
		public float changeStatesTime;
		public float randomDelay;
	}

	[System.Serializable]
	public class ProjectileAttack {
		public GameObject emitter;
		public float activateTime;
		public float aTRandomDelay;
		public int numActivates;
		public float timeBetweenActivates;
		public float tBARandomDelay;
	}

	[System.Serializable]
	public class Phase1	{
		public MoveSpeeds moveSpeeds;
		public ProjectileAttack[] projectileAttacks;
		public float timeBetweenAttacks;
		public float randomDelay;
	}

	[System.Serializable]
	public class Phase2	{
		public MoveSpeeds moveSpeeds;
		public ProjectileAttack[] projectileAttacks;
		public float timeBetweenAttacks;
		public float randomDelay;
		public int harpiesToSpawn;
	}
		
	[System.Serializable]
	public class Phase3	{
		public MoveSpeeds moveSpeeds;
		public ProjectileAttack[] projectileAttacks;
		public float timeBetweenAttacks;
		public float randomDelay;
		public int harpiesToSpawn;
	}

	public Phase1 phase1;
	public Phase2 phase2;
	public Phase3 phase3;
	public SpawnObjectByActivate[] harpySpawnObjectByActivate;
	public Transform[] wayPoints;
	public float phaseChangeDelay = 2;

	private Transform player;
	private MoveByForce moveByForce;
	private Health health;
	private int phase = 1;
	private bool canAttack = false;
	private bool isAttacking = false;
	private int moveState = 0; // 0 = stop, 1 = move towards player, 2 = move to random waypoint
	private int attackNum;
	private int oldAttackNum = -1;
	private int timesActivated;
	private bool changingPhase = false;
	private FollowTargetLerp cameraFollowTargetLerp;
	private Transform currentWaypoint;
	private GameObject inputController;
    private Animator anim;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player").transform;
		cameraFollowTargetLerp = Camera.main.GetComponentInParent<FollowTargetLerp>();
		moveByForce = GetComponent<MoveByForce>();
		health = GetComponent<Health>();
		inputController = GameObject.Find ("Input Controller");
        anim = GetComponentInChildren<Animator>();
	}

	void OnEnable () {
		StartCoroutine (WaitToChangeMoveState (Random.Range (phase1.moveSpeeds.changeStatesTime - phase1.moveSpeeds.randomDelay, phase1.moveSpeeds.changeStatesTime + phase1.moveSpeeds.randomDelay)));
		moveByForce.force = 0;
		moveByForce.dir = Vector3.zero;
		foreach (ProjectileAttack projectileAttack in phase1.projectileAttacks) {
			projectileAttack.emitter.SetActive (false);
		}
		StartCoroutine (WaitToAttack (Random.Range (phase1.timeBetweenAttacks - phase1.randomDelay, phase1.timeBetweenAttacks + phase1.randomDelay)));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (player && !changingPhase) {
			Move ();
			if (canAttack) {
				Attack ();
			}
			CalculatePhase ();
		}
		if (moveByForce.dir != Vector3.zero) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveByForce.dir), 3f * Time.deltaTime); // Rotates after object
			transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, 0));
		}
	}

	void Move () {
		if (moveState == 0) {
			moveByForce.dir = Vector3.zero;
		} else if (moveState == 1) {
			moveByForce.dir = (player.position - transform.position).normalized;
		} else {
			if (Vector3.Distance (transform.position, currentWaypoint.position) <= 0.1f) {
				moveByForce.force = 0;
				moveByForce.dir = Vector3.zero;
			} else {
				moveByForce.dir = (currentWaypoint.position - transform.position).normalized;
			}
		}
        if (moveByForce.dir == Vector3.zero) {
            anim.SetBool("IsWalking", false);
        } else {
            anim.SetBool("IsWalking", true);
        }
	}

	IEnumerator WaitToChangeMoveState (float delay) {
		yield return new WaitForSeconds (delay);
		while (isAttacking) {
			yield return null;
		}
		ChangeMoveState ();
	}

	void ChangeMoveState () {
		moveState = Random.Range (0, 3);
		if (moveState == 0) {
			moveByForce.force = 0;
		} else if (moveState == 1) {
			if (phase == 1) {
				moveByForce.force = phase1.moveSpeeds.toPlayer;
			} else if (phase == 2) {
				moveByForce.force = phase2.moveSpeeds.toPlayer;
			} else {
				moveByForce.force = phase3.moveSpeeds.toPlayer;
			}
		} else {
			currentWaypoint = wayPoints [Random.Range (0, wayPoints.Length)];
			if (phase == 1) {
				moveByForce.force = phase1.moveSpeeds.toWaypoint;
			} else if (phase == 2) {
				moveByForce.force = phase2.moveSpeeds.toWaypoint;
			} else {
				moveByForce.force = phase3.moveSpeeds.toWaypoint;
			}
		}
		if (phase == 1) {
			StartCoroutine (WaitToChangeMoveState (Random.Range (phase1.moveSpeeds.changeStatesTime - phase1.moveSpeeds.randomDelay, phase1.moveSpeeds.changeStatesTime + phase1.moveSpeeds.randomDelay)));
		} else if (phase == 2) {
			StartCoroutine (WaitToChangeMoveState (Random.Range (phase2.moveSpeeds.changeStatesTime - phase2.moveSpeeds.randomDelay, phase2.moveSpeeds.changeStatesTime + phase2.moveSpeeds.randomDelay)));
		} else {
			StartCoroutine (WaitToChangeMoveState (Random.Range (phase3.moveSpeeds.changeStatesTime - phase3.moveSpeeds.randomDelay, phase3.moveSpeeds.changeStatesTime + phase3.moveSpeeds.randomDelay)));
		}
	}

	/*void OnCollisionEnter (Collision other) {
		if (other.collider.CompareTag ("Untagged")) { // Collided with environment
			print ("Change State");
			ChangeMoveState ();
		}
	}*/

	void Attack () {
		timesActivated = 1;
		oldAttackNum = attackNum;
		if (phase == 1) {
			attackNum = Random.Range (0, phase1.projectileAttacks.Length);
			for (int i = 0; i < 2; i++) {
				if (oldAttackNum == attackNum) { // Lowers chances of throwing same attack twice, but doesn't get rid of possibility
					attackNum = Random.Range (0, phase1.projectileAttacks.Length);
				}
			}
			phase1.projectileAttacks [attackNum].emitter.SetActive (true);
			canAttack = false;
			isAttacking = true;
			StartCoroutine (WaitToStopAttacking (Random.Range (phase1.projectileAttacks [attackNum].activateTime - phase1.projectileAttacks [attackNum].aTRandomDelay, phase1.projectileAttacks [attackNum].activateTime + phase1.projectileAttacks [attackNum].aTRandomDelay)));
		} else if (phase == 2) {
			attackNum = Random.Range (0, phase2.projectileAttacks.Length + 1);
			if (attackNum == phase2.projectileAttacks.Length) {
				SpawnHarpies ();
			} else {
				if (oldAttackNum == attackNum) { // Lowers chances of throwing same attack twice, but doesn't get rid of possibility
					attackNum = Random.Range (0, phase2.projectileAttacks.Length);
				}
				phase2.projectileAttacks [attackNum].emitter.SetActive (true);
				canAttack = false;
				isAttacking = true;
				StartCoroutine (WaitToStopAttacking (Random.Range (phase2.projectileAttacks [attackNum].activateTime - phase2.projectileAttacks [attackNum].aTRandomDelay, phase2.projectileAttacks [attackNum].activateTime + phase2.projectileAttacks [attackNum].aTRandomDelay)));
			}
		} else {			
			attackNum = Random.Range (0, phase3.projectileAttacks.Length + 1);
			if (attackNum == phase3.projectileAttacks.Length) {
				SpawnHarpies ();
			} else {
				if (oldAttackNum == attackNum) { // Lowers chances of throwing same attack twice, but doesn't get rid of possibility
					attackNum = Random.Range (0, phase3.projectileAttacks.Length);
				}
				phase3.projectileAttacks [attackNum].emitter.SetActive (true);
				canAttack = false;
				isAttacking = true;
				StartCoroutine (WaitToStopAttacking (Random.Range (phase3.projectileAttacks [attackNum].activateTime - phase3.projectileAttacks [attackNum].aTRandomDelay, phase3.projectileAttacks [attackNum].activateTime + phase3.projectileAttacks [attackNum].aTRandomDelay)));
			}
		}
	}

	IEnumerator WaitToStopAttacking (float delay) {
		yield return new WaitForSeconds (delay);
		if (phase == 1) {
			phase1.projectileAttacks [attackNum].emitter.SetActive (false);
			if (phase1.projectileAttacks [attackNum].numActivates != 1) {
				while (timesActivated < phase1.projectileAttacks [attackNum].numActivates) {
					yield return new WaitForSeconds (Random.Range (phase1.projectileAttacks [attackNum].timeBetweenActivates - phase1.projectileAttacks [attackNum].tBARandomDelay, phase1.projectileAttacks [attackNum].timeBetweenActivates + phase1.projectileAttacks [attackNum].tBARandomDelay));
					timesActivated++;
					phase1.projectileAttacks [attackNum].emitter.SetActive (true);
					yield return new WaitForSeconds (delay);
					phase1.projectileAttacks [attackNum].emitter.SetActive (false);
				}
			}
			isAttacking = false;
			StartCoroutine (WaitToAttack (Random.Range (phase1.timeBetweenAttacks - phase1.randomDelay, phase1.timeBetweenAttacks + phase1.randomDelay)));
		} else if (phase == 2) {
			phase2.projectileAttacks [attackNum].emitter.SetActive (false);
			if (phase2.projectileAttacks [attackNum].numActivates != 1) {
				while (timesActivated < phase2.projectileAttacks [attackNum].numActivates) {
					yield return new WaitForSeconds (Random.Range (phase2.projectileAttacks [attackNum].timeBetweenActivates - phase2.projectileAttacks [attackNum].tBARandomDelay, phase2.projectileAttacks [attackNum].timeBetweenActivates + phase2.projectileAttacks [attackNum].tBARandomDelay));
					timesActivated++;
					phase2.projectileAttacks [attackNum].emitter.SetActive (true);
					yield return new WaitForSeconds (delay);
					phase2.projectileAttacks [attackNum].emitter.SetActive (false);
				}
			}
			isAttacking = false;
			StartCoroutine (WaitToAttack (Random.Range (phase2.timeBetweenAttacks - phase2.randomDelay, phase2.timeBetweenAttacks + phase2.randomDelay)));
		} else {
			phase3.projectileAttacks [attackNum].emitter.SetActive (false);
			if (phase3.projectileAttacks [attackNum].numActivates != 1) {
				while (timesActivated < phase3.projectileAttacks [attackNum].numActivates) {
					yield return new WaitForSeconds (Random.Range (phase3.projectileAttacks [attackNum].timeBetweenActivates - phase3.projectileAttacks [attackNum].tBARandomDelay, phase3.projectileAttacks [attackNum].timeBetweenActivates + phase3.projectileAttacks [attackNum].tBARandomDelay));
					timesActivated++;
					phase3.projectileAttacks [attackNum].emitter.SetActive (true);
					yield return new WaitForSeconds (delay);
					phase3.projectileAttacks [attackNum].emitter.SetActive (false);
				}
			}
			isAttacking = false;
			StartCoroutine (WaitToAttack (Random.Range (phase3.timeBetweenAttacks - phase3.randomDelay, phase3.timeBetweenAttacks + phase3.randomDelay)));
		}
	}

	IEnumerator WaitToAttack (float delay) {
		yield return new WaitForSeconds (delay);
		canAttack = true;
	}

	void CalculatePhase () {
		if ((health.currentHealth < health.startHealth / 3) && phase == 2) {
			StopAllCoroutines ();
			StartCoroutine (WaitToChangePhase (3));
		} else if ((health.currentHealth < health.startHealth / 3 * 2) && phase == 1) {
			StopAllCoroutines ();
			StartCoroutine (WaitToChangePhase (2));
		}
	}

	IEnumerator WaitToChangePhase (int newPhase) {
		changingPhase = true;
		if (phase == 1) {
			phase1.projectileAttacks [attackNum].emitter.SetActive (false);
		} else if (phase == 2) {
			phase2.projectileAttacks [attackNum].emitter.SetActive (false);
		}
		cameraFollowTargetLerp.target = GetComponent<Rigidbody> ();
		GetComponent<TakeDamage> ().enabled = false;
		player.GetComponent<TakeDamage> ().enabled = false;
		player.GetComponent<PlayerMoveInput> ().enabled = false;
		inputController.GetComponent<SpellInputReceiver> ().enabled = false;
		inputController.GetComponent<SpecialInputReceiver> ().enabled = false;
		moveByForce.dir = Vector3.zero;
		yield return new WaitForSeconds (phaseChangeDelay);
		phase = newPhase;
		changingPhase = false;
		cameraFollowTargetLerp.target = player.GetComponent<Rigidbody> ();
		GetComponent<TakeDamage> ().enabled = true;
		player.GetComponent<TakeDamage> ().enabled = true;
		player.GetComponent<PlayerMoveInput> ().enabled = true;
		inputController.GetComponent<SpellInputReceiver> ().enabled = true;
		inputController.GetComponent<SpecialInputReceiver> ().enabled = true;
		SpawnHarpies ();
		if (phase == 2) {
			StartCoroutine (WaitToAttack (Random.Range (phase2.timeBetweenAttacks - phase2.randomDelay, phase2.timeBetweenAttacks + phase2.randomDelay)));
			StartCoroutine (WaitToChangeMoveState (Random.Range (phase2.moveSpeeds.changeStatesTime - phase2.moveSpeeds.randomDelay, phase2.moveSpeeds.changeStatesTime + phase2.moveSpeeds.randomDelay)));
		} else {
			StartCoroutine (WaitToAttack (Random.Range (phase3.timeBetweenAttacks - phase3.randomDelay, phase3.timeBetweenAttacks + phase3.randomDelay)));
			StartCoroutine (WaitToChangeMoveState (Random.Range (phase3.moveSpeeds.changeStatesTime - phase3.moveSpeeds.randomDelay, phase3.moveSpeeds.changeStatesTime + phase3.moveSpeeds.randomDelay)));
		}
	}
		
	void SpawnHarpies () {
		if (phase == 2 && GameObject.FindGameObjectsWithTag ("Enemy").Length < phase2.harpiesToSpawn + 1) {
			for (int i = 0; i < phase2.harpiesToSpawn; i++) {
				int j = Random.Range (0, harpySpawnObjectByActivate.Length);
				harpySpawnObjectByActivate [j].Spawn ();
			}
		} else if (phase == 3 && GameObject.FindGameObjectsWithTag ("Enemy").Length < phase3.harpiesToSpawn + 1) {
			for (int i = 0; i < phase3.harpiesToSpawn; i++) {
				int j = Random.Range (0, harpySpawnObjectByActivate.Length);
				harpySpawnObjectByActivate [j].Spawn ();
			}
		}
	}
}
