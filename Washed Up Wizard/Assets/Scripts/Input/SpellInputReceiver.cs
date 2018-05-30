using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellInputReceiver : MonoBehaviour {

    // Button references
	public KeyCode[] spellSlots = new KeyCode[] { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Space };
    public KeyCode toggleSpellBook = KeyCode.Tab; // The key to toggle spell book
    public KeyCode nextPage = KeyCode.E; // The key to flip to the next page
    public KeyCode previousPage = KeyCode.Q; // The key to flip to the previous page

	// Bools for whether the object is pressing the button
	[HideInInspector] public bool[] inputSD;
	[HideInInspector] public bool[] inputSU;
    [HideInInspector] public bool inputT;
    [HideInInspector] public bool inputN;
    [HideInInspector] public bool inputP;

    void Awake () {
		inputSD = new bool[spellSlots.Length];
		inputSU = new bool[spellSlots.Length];
        inputT = false;
        inputN = false;
        inputP = false;
	}

	void OnDisable () {
		for (int i = 0; i < inputSD.Length; i++) {
			inputSD [i] = false; // Getting input for spell slots
			inputSU [i] = false; // Getting input for spell slots
		}
        inputT = false;
        inputN = false;
        inputP = false;
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
        inputT = Input.GetKeyDown (toggleSpellBook); // Getting input for toggling spell book
        inputN = Input.GetKeyDown (nextPage); // Getting input for flipping to the next page
        inputP = Input.GetKeyDown (previousPage); // Getting input for flipping to the previous page
    }
}
