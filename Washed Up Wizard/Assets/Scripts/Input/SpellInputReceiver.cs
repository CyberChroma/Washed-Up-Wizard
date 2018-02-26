using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        GameObject currentSelection = EventSystem.current.currentSelectedGameObject;
        for (int i = 0; i < inputSD.Length; i++) {
            inputSD[i] = Input.GetKey(spellSlots[i]); // Getting input for spell slots
            if (currentSelection && currentSelection.GetComponent<Button>() != null && spellSlots [i] == KeyCode.Mouse0) {
                inputSD [i] = false;
            }
        }
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
        if (currentSelection) {
            currentSelection = null;
        }
        for (int i = 0; i < inputSU.Length; i++) {
            inputSU[i] = Input.GetKeyUp(spellSlots[i]); // Getting input for spell slots
        }
    }
}
