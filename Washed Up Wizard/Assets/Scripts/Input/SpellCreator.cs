using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SpellCreator : MonoBehaviour {

	public GameObject[] craftingSlot1Components;
	public GameObject[] craftingSlot2Components;
	public GameObject[] craftingSlot3Components;
	public ActivateFollowTarget spellCraftingPanel;

	[HideInInspector] public bool creatingSpell = false;
	private int currentArrayNum;
	private int[] spellID = new int[3];
	private bool spellSetUp = false;
	private PlayerSpellsReference playerSpellsReference;
	private ComponentInputReceiver componentInputReceiver;
	private SpellInputReceiver spellInputReceiver;
	private SpawnObjectByInput tempSpawnObjectByInput;
	private GameObject[] activeEmitters = new GameObject[3];

	// Use this for initialization
	void Start () {
		playerSpellsReference = GetComponent<PlayerSpellsReference> ();
		componentInputReceiver = GameObject.Find ("Input Controller").GetComponent<ComponentInputReceiver> ();
		spellInputReceiver = GameObject.Find ("Input Controller").GetComponent<SpellInputReceiver> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (spellSetUp) {
			CreateSpell ();
		} else { 	
			SetUpSpell ();
		}
		if (currentArrayNum == 1 || currentArrayNum == 2) {
			for (int i = 0; i < spellInputReceiver.inputSD.Length; i++) {
				if (spellInputReceiver.inputSD [i] && !EventSystem.current.IsPointerOverGameObject ()) {
					craftingSlot1Components [spellID [0]].SetActive (false);
					if (currentArrayNum == 2) {
						craftingSlot2Components [spellID [1]].SetActive (false);
					}
					spellCraftingPanel.Activate ();
					currentArrayNum = 0;
					spellSetUp = false;
					creatingSpell = false;
				}
			}
		}
	}

	void SetUpSpell () {
		for (int i = 0; i < componentInputReceiver.inputC.Length; i++) {
			if (componentInputReceiver.inputC [i]) {
				creatingSpell = true;
				spellID [currentArrayNum] = i;
				currentArrayNum++;
				if (currentArrayNum == 1) {
					craftingSlot1Components [spellID [currentArrayNum - 1]].SetActive (true);
					spellCraftingPanel.Activate ();
				} else if (currentArrayNum == 2) {
					craftingSlot2Components [spellID [currentArrayNum - 1]].SetActive (true);
				} else if (currentArrayNum == 3) {
					craftingSlot3Components [spellID [currentArrayNum - 1]].SetActive (true);
					spellSetUp = true;
				}
			}
		}
	}

	void CreateSpell () {
		for (int i = 0; i < spellInputReceiver.inputSD.Length; i++) {
			if (spellInputReceiver.inputSD [i]) {
				if (playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]] != null) {
					if (activeEmitters [i] != null) {
						activeEmitters [i].GetComponent<SpawnObjectByInput> ().emitterNum = 0;
					}
					if (GameObject.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name + " Emitter")) {
						tempSpawnObjectByInput = GameObject.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name + " Emitter").GetComponent<SpawnObjectByInput> ();
						tempSpawnObjectByInput.emitterNum = i + 1;
						activeEmitters [i] = tempSpawnObjectByInput.gameObject;
					}
				}
				spellCraftingPanel.Activate ();
				StartCoroutine (WaitToCreateSpell ());
			}
		}
	}

	IEnumerator WaitToCreateSpell () {
		yield return new WaitForSeconds (0.2f);
		craftingSlot1Components [spellID [0]].SetActive (false);
		craftingSlot2Components [spellID [1]].SetActive (false);
		craftingSlot3Components [spellID [2]].SetActive (false);
		currentArrayNum = 0;
		spellSetUp = false;
		spellID = new int[3];
		creatingSpell = false;
	}
}
