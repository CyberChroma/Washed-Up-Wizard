using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpinForce : MonoBehaviour {

	[System.Serializable]
	public class Left {
		public float force = 5;
		public Vector3 dir = Vector3.zero;
		public float time = 2;
	}

	[System.Serializable]
	public class Stop {
		public float time = 1;
	}

	[System.Serializable]
	public class Right {
		public float force = 5;
		public Vector3 dir = Vector3.zero;
		public float time = 2;
	}

	public enum States {
		left, stop, right
	}
		
	public Left left;
	public Stop stop;
	public Right right;
	public States[] pattern;

	private Rigidbody rb;
	private int arrayNum = -1;
	private States currentState;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
	}

	void OnEnable () {
		arrayNum = -1;
		ChangeStates ();
	}
	
	// Update is called once per frame
	void Update () {
		print (currentState);
	}

	IEnumerator WaitToChangeStates (float delay) {
		yield return new WaitForSeconds (delay);
		ChangeStates ();
	}

	void ChangeStates () {
		arrayNum++;
		if (arrayNum >= pattern.Length) {
			arrayNum = 0;
		}
		currentState = pattern [arrayNum];
		if (currentState == States.left) {
			rb.angularVelocity = left.force * left.dir;
			StartCoroutine (WaitToChangeStates (left.time));
		} else if (currentState == States.stop) {
			rb.angularVelocity = Vector3.zero;
			StartCoroutine (WaitToChangeStates (stop.time));
		} else {
			rb.angularVelocity = -right.force * right.dir;
			StartCoroutine (WaitToChangeStates (right.time));
		}
	}
}
