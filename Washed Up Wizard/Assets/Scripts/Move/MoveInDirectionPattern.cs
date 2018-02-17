using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInDirectionPattern : MonoBehaviour {

	[System.Serializable]
	public class State {
        public float speed = 10; // The speed of the object
		public float speedChangeSpeed = 1; // How fast the speed changes
		public Vector3 dir = Vector3.up; // The direction to move
		public float dirChangeSpeed = 1; // How fast the direction changes
		public float duration = 1; // The time this state lasts
	}

	public State[] states;

	private int stateNum = 0; // The state the object is currently in
	private MoveInDirection moveInDirection; // Reference to the move script
	private float timeToNextState; // The time to change states

	// Use this for initialization
	void Awake () {
		moveInDirection = GetComponent<MoveInDirection> (); // Getting the reference
	}

	void OnEnable () {
        stateNum = 0;
		moveInDirection.speed = states [stateNum].speed; // Sets the speed to the first state
		moveInDirection.dir = states [stateNum].dir; // Sets the direction to the first state
		timeToNextState = Time.time + states [stateNum].duration; // Sets the time to the first state
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        moveInDirection.speed = Mathf.MoveTowards (moveInDirection.speed, states [stateNum].speed, states [stateNum].speedChangeSpeed * Time.deltaTime); // Changing the speed
        moveInDirection.dir = Vector3.MoveTowards (moveInDirection.dir, states [stateNum].dir, states [stateNum].dirChangeSpeed * Time.deltaTime); // Changing the direction
		if (timeToNextState <= Time.time) { // Seeing the time has elapsed
			NextState ();
		}
	}

	void NextState () {
		stateNum++; // Increasing the state number
		if (stateNum >= states.Length) { // If the state number is greater than the length
			stateNum = 0; // Going back to state 1
		}
		timeToNextState = Time.time + states [stateNum].duration; // Setting the duration
	}
}
