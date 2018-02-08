using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInputReceiver : MonoBehaviour {

    // Button references
	public KeyCode[] spellSlots = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Space };

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool[] inputSD;
	[HideInInspector] public bool[] inputSU;

	void Awake () {
		inputSD = new bool[spellSlots.Length];
		inputSU = new bool[spellSlots.Length];
	}

	void OnDisable () {
		for (int i = 0; i < inputSD.Length; i++) {
			inputSD [i] = false; // Getting input for spell slots
			inputSU [i] = false; // Getting input for spell slots
		}
	}

	// Update is called once per frame
	void Update () {
        if (spellSlots.Length != inputSD.Length || spellSlots.Length != inputSU.Length) { // If these arrays have different lengths
			inputSD = new bool[spellSlots.Length]; // Makes array same length
			inputSU = new bool[spellSlots.Length]; // Makes array same length
		}

 		for (int i = 0; i < inputSD.Length; i++) {
			inputSD [i] = Input.GetKeyDown (spellSlots [i]); // Getting input for spell slots
		}

		for (int i = 0; i < inputSU.Length; i++) {
			inputSU [i] = Input.GetKeyUp (spellSlots [i]); // Getting input for spell slots
		}
	}
}
