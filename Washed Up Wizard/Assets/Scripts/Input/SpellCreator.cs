using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Used to access events
using UnityEngine.UI; // Used to access UI

public class SpellCreator : MonoBehaviour {

	public GameObject[] craftingComponents; // The crafting component images
	public GameObject[] glows; // The glow images
	public GameObject[] spellSlots; // The active spell slot
	public Slider[] cooldownWheels; // The cooldown wheel slider
	public ActivateFollowTarget componentsPanel; // The script that moves the panel
	public Transform activeSpellsPanel; // The active spells panel
    public GameObject inputController;

	[HideInInspector] public bool creatingSpell = false; // Whether the player is creating a spell
    [HideInInspector] public GameObject[] activeEmitters = new GameObject[3]; // References to the active spell emitter
    private GameObject[] activeSpellSprites = new GameObject[3]; // References to the spell sprites
    private Transform tempSpellSprite; // Temporary reference to the spell sprite
	private bool canCreate = true; // Whether the player can create a spell
	private int currentArrayNum; // Used to determine how many components have been entered
	private int[] spellID = new int[3]; // Used to access the spell id
    private bool spellSetUp = false; // Whether the player has set up a spell (and is assigning)
    // Script references
	private PlayerSpellsReference playerSpellsReference; 
	private ComponentInputReceiver componentInputReceiver;
	private SpellInputReceiver spellInputReceiver;
	private SpawnObjectByInput tempSpawnObjectByInput;
    private int componentsPanelTimesActivated = 0;

	// Use this for initialization
	void Awake () {
        // Getting references
		playerSpellsReference = GetComponent<PlayerSpellsReference> ();
        componentInputReceiver = inputController.GetComponent<ComponentInputReceiver> ();
        spellInputReceiver = inputController.GetComponent<SpellInputReceiver> ();
        GameObject.Find("Scene Saver").GetComponent<SceneSaver>().TransferSpells ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canCreate) { // If the player can create a spell
			if (spellSetUp) { // If the player has set up a spell
                for (int i = 0; i < spellInputReceiver.inputSD.Length; i++) { // Goes through each active spell button
                    if (spellInputReceiver.inputSD[i] && componentsPanelTimesActivated % 2 != 0) { // If the button was pressed
                        CreateSpell(spellID, i);
                        componentsPanel.Activate ();
                        componentsPanelTimesActivated++;
                    }
                }
			} else { // If the player has not set up a spell
				SetUpSpell ();
			}
			if (currentArrayNum == 1 || currentArrayNum == 2) { // If the player has input components 1 or 2
				for (int i = 0; i < spellInputReceiver.inputSD.Length; i++) { // Goes through the active spell buttons 
                    if (spellInputReceiver.inputSD [i] && componentsPanelTimesActivated % 2 != 0) { // If the player has pressed a button and this button press was not clicking a UI button
						// Resetting the spell creation
                        glows [0].SetActive (false);
						if (currentArrayNum == 2) {
							glows [1].SetActive (false);
						}
						componentsPanel.Activate ();
                        componentsPanelTimesActivated++;
						currentArrayNum = 0;
						spellSetUp = false;
						creatingSpell = false;
					}
				}
			}
		}
	}

	void SetUpSpell () {
		for (int i = 0; i < componentInputReceiver.inputC.Length; i++) { // Goes through each component button 
            if (componentInputReceiver.inputC [i]) { // If the button was pressed
				creatingSpell = true; // The player is creating the a spell
				spellID [currentArrayNum] = i;
				currentArrayNum++;
				if (currentArrayNum == 1) { // If this is the first component
					glows [0].transform.position = craftingComponents [i].transform.position;
					glows [0].SetActive (true);
                    if (componentsPanelTimesActivated % 2 == 0) {
					    componentsPanel.Activate ();
                        componentsPanelTimesActivated++;
                    }
				} else if (currentArrayNum == 2) { // If this is the second component
					glows [1].transform.position = craftingComponents [i].transform.position;
					glows [1].SetActive (true);
				} else if (currentArrayNum == 3) { // If this is the third component
					glows [2].transform.position = craftingComponents [i].transform.position;
					glows [2].SetActive (true);
					spellSetUp = true;
				}
			}
		}
	}

    public void CreateSpell (int[] currentSpellID, int slotNum) {
        if (playerSpellsReference.spells [currentSpellID [0]].componentSpells [currentSpellID [1] * 10 + currentSpellID [2]] != null) { // If the requested spell exists 
			if (activeEmitters [slotNum] != null) { // If there is a spell in this slot
				activeEmitters [slotNum].GetComponent<SpawnObjectByInput> ().emitterNum = 0; // Unlinking the emitter to the key
			}
			if (activeSpellSprites [slotNum] != null) { // If there is a spell image active
				activeSpellSprites [slotNum].SetActive (false); // Disables the image
			}
            if (GameObject.Find ((playerSpellsReference.spells [currentSpellID [0]].componentSpells [currentSpellID [1] * 10 + currentSpellID [2]]).name + " Emitter")) { // If the spell emitter exists
                tempSpawnObjectByInput = GameObject.Find ((playerSpellsReference.spells [currentSpellID [0]].componentSpells [currentSpellID [1] * 10 + currentSpellID [2]]).name + " Emitter").GetComponent<SpawnObjectByInput> (); // Gets a reference to the emitter
				tempSpawnObjectByInput.emitterNum = slotNum + 1; // Setting up the emitter to the button
				tempSpawnObjectByInput.cooldownWheel = cooldownWheels [slotNum]; // Assigning the cooldown wheel
                tempSpawnObjectByInput.cooldownWheel.value = tempSpawnObjectByInput.cooldownValue;
                cooldownWheels [slotNum].maxValue = tempSpawnObjectByInput.cooldown; // Setting the max value of the cooldown wheel
                if (activeSpellsPanel.Find ((playerSpellsReference.spells [currentSpellID [0]].componentSpells [currentSpellID [1] * 10 + currentSpellID [2]]).name)) { // If the spell image exists
                    tempSpellSprite = activeSpellsPanel.Find ((playerSpellsReference.spells [currentSpellID [0]].componentSpells [currentSpellID [1] * 10 + currentSpellID [2]]).name); // Getting the reference
					tempSpellSprite.position = spellSlots [slotNum].transform.position; // Setting the position of the sprite
					tempSpellSprite.gameObject.SetActive (true); // Enabling the sprite
				}
                // Removing references if the spell was active in another spell slot
                for (int j = 0; j < activeEmitters.Length; j++) {
                    if (activeEmitters [j] == tempSpawnObjectByInput.gameObject) {
                        activeEmitters [j] = null;
                    }
                }
                for (int j = 0; j < activeSpellSprites.Length; j++) {
                    if (activeSpellSprites [j] == tempSpellSprite.gameObject) {
                        activeSpellSprites [j] = null;
                        cooldownWheels [slotNum].gameObject.SetActive(true);
                        cooldownWheels [j].value = 0;
                        cooldownWheels [j].gameObject.SetActive(false);
                    }
                }
                if (tempSpawnObjectByInput.cooldownValue <= 0) {
                    cooldownWheels[slotNum].gameObject.SetActive(false);
                } else {
                    cooldownWheels[slotNum].gameObject.SetActive(true);
                }
                // Setting references
				activeEmitters [slotNum] = tempSpawnObjectByInput.gameObject;
				activeSpellSprites [slotNum] = tempSpellSprite.gameObject;
			}
		}
		StartCoroutine (WaitToCreateSpell ());
        GameObject.Find("Scene Saver").GetComponent<SceneSaver>().UpdateSpells (currentSpellID, slotNum);
	}

	IEnumerator WaitToCreateSpell () { // Waits then resets spell crafting
		canCreate = false;
		yield return new WaitForSeconds (0.5f);
		glows [0].SetActive (false);
		glows [1].SetActive (false);
		glows [2].SetActive (false);
		currentArrayNum = 0;
		spellSetUp = false;
		spellID = new int[3];
		creatingSpell = false;
		canCreate = true;
	}
}
