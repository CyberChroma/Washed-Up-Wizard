using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPattern : MonoBehaviour {

	[System.Serializable]
	public class State {
		public float rotSpeed = 100;
		public Vector3 dir = Vector3.zero;
		public float rotSpeedChangeSpeed = 1;
		public float duration = 1;
	}
		
	public State[] states;

	private int stateNum = 0;
	private Spin spin;
	private float timeToNextState;

	// Use this for initialization
	void Awake () {
		spin = GetComponent<Spin> ();
	}

	void OnEnable () {
		spin.rotSpeed = states [stateNum].rotSpeed;
		timeToNextState = Time.time + states [stateNum].duration;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spin.rotSpeed = Mathf.MoveTowards (spin.rotSpeed, states [stateNum].rotSpeed, states [stateNum].rotSpeedChangeSpeed);
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
