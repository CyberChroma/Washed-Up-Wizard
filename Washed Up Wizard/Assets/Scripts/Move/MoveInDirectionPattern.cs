using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirectionPattern : MonoBehaviour {

	[System.Serializable]
	public class State {
		public float speed = 10;
		public float speedChangeSpeed = 1;
		public Vector3 dir = Vector3.up;
		public float dirChangeSpeed = 1;
		public float duration = 1;
	}

	public State[] states;

	private int stateNum = 0;
	private MoveInDirection moveInDirection;
	private float timeToNextState;

	// Use this for initialization
	void Awake () {
		moveInDirection = GetComponent<MoveInDirection> ();
	}

	void OnEnable () {
		moveInDirection.speed = states [stateNum].speed;
		moveInDirection.dir = states [stateNum].dir;
		timeToNextState = Time.time + states [stateNum].duration;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		moveInDirection.speed = Mathf.MoveTowards (moveInDirection.speed, states [stateNum].speed, states [stateNum].speedChangeSpeed);
		moveInDirection.dir = Vector3.MoveTowards (moveInDirection.dir, states [stateNum].dir, states [stateNum].dirChangeSpeed);
		if (timeToNextState <= Time.time) {
			NextState ();
		}
	}

	void NextState () {
		stateNum++;
		if (stateNum >= states.Length) {
			stateNum = 0;
		}
		timeToNextState = Time.time + states [stateNum].duration;
	}
}
