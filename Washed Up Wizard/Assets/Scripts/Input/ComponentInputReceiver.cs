using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentInputReceiver : MonoBehaviour {

	public KeyCode[] components = new KeyCode[] {  KeyCode.Keypad0, KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad8, KeyCode.Keypad9 };
	public KeyCode[] componentsAlt = new KeyCode[] {  KeyCode.Alpha0, KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7, KeyCode.Alpha8, KeyCode.Alpha9 };

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool[] inputC;

	void Awake () {
		inputC = new bool[components.Length];
	}

	// Update is called once per frame
	void Update () {
		if (components.Length != inputC.Length) {
			inputC = new bool[components.Length];
		}

 		for (int i = 0; i < inputC.Length; i++) {
			if (Input.GetKeyDown (components [i]) || Input.GetKeyDown (componentsAlt [i])) {
				inputC [i] = true; // Getting input for component
			} else {
				inputC [i] = false; // Getting input for component
			}
		}
	}

	public void TriggerInput (int componentNum) {
		inputC [componentNum] = true;
	}
}
