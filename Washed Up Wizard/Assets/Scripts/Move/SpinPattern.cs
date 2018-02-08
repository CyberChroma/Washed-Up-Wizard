using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPattern : MonoBehaviour {

	[System.Serializable]
	public class State {
        public float rotSpeed = 100; // The rotational speed of the object
		public Vector3 dir = Vector3.zero; // The direction to spin
        public float rotSpeedChangeSpeed = 1; // How fast the speed changes
		public float duration = 1; // The duration of the state
	}
		
	public State[] states;

    private int stateNum = 0; // The state the object is currently in
	private Spin spin; // Reference to the spin script
    private float timeToNextState; // The time to change states

	// Use this for initialization
	void Awake () {
        spin = GetComponent<Spin> (); // Getting the reference
	}

	void OnEnable () {
        spin.rotSpeed = states [stateNum].rotSpeed; // Sets the speed to the first state
        timeToNextState = Time.time + states [stateNum].duration; // Sets the time to the first state
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        spin.rotSpeed = Mathf.MoveTowards (spin.rotSpeed, states [stateNum].rotSpeed, states [stateNum].rotSpeedChangeSpeed); // Changing the speed
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
