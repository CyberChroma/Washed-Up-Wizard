using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAI : MonoBehaviour {

	[System.Serializable]
	public class Boundaries {
		public float minx = -20;
		public float maxx = 20;
		public float miny = -20;
		public float maxy = 20;
	}

	public float moveDelay = 2;
	public float moveSensitivity = 0.1f;
	public float attackDelay = 1;
	public float attackTime = 0.5f;
	public Boundaries boundaries;
	public GameObject[] emitters;
	public Transform targetLocation;

	private Animator anim;
	private MoveByForce enemyMove;
	private Transform player;
	private bool canMove = true;

	// Use this for initialization
	void Awake () {
		enemyMove = GetComponent<MoveByForce> ();
		anim = GetComponent<Animator> ();
		player = GameObject.Find ("Player").transform;
	}

	void OnEnable () {
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (false);
		}
		targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy));
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (canMove) {
			if (Vector3.Distance (transform.position, new Vector3 (targetLocation.position.x, transform.position.y, targetLocation.position.z)) <= 1f) {
				enemyMove.dir = Vector3.zero;
				StartCoroutine (Attack ());
				StartCoroutine (ChangeTarget ());
			} else {
				Vector3 dir = targetLocation.position - transform.position;
				dir = new Vector3 (dir.x, 0, dir.z);
				if (dir.magnitude < 1) {
					enemyMove.dir = dir;
				} else {
					enemyMove.dir = dir.normalized;
				}
			}
		}
		Quaternion targetRotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 0.5f);
		transform.rotation = Quaternion.Euler (0, targetRotation.eulerAngles.y, 0);
	}

	void OnCollisionEnter (Collision other) {
		enemyMove.dir = Vector3.zero;
		StartCoroutine (Attack ());
		StartCoroutine (ChangeTarget ());
	}

	IEnumerator Attack () {
		anim.SetTrigger ("Attack");
		yield return new WaitForSeconds (attackDelay);
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (true);
		}
		yield return new WaitForSeconds (attackTime);
		foreach (GameObject emitter in emitters) {
			emitter.SetActive (false);
		}
	}

	IEnumerator ChangeTarget () {
		canMove = false;
		yield return new WaitForSeconds (moveDelay);
		targetLocation.position = new Vector3 (Random.Range (boundaries.minx, boundaries.maxx), 0, Random.Range (boundaries.miny, boundaries.maxy));
		canMove = true;
	}
}