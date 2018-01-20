using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellCreator : MonoBehaviour {

	public GameObject[] craftingComponents;
	public GameObject[] glows;
	public GameObject[] spellSlots;
	public Slider[] cooldownWheels;

	public ActivateFollowTarget componentsPanel;
	public Transform activeSpellsPanel;

	[HideInInspector] public bool creatingSpell = false;
	private bool canCreate = true;
	private int currentArrayNum;
	private int[] spellID = new int[3];
	private bool spellSetUp = false;
	private PlayerSpellsReference playerSpellsReference;
	private ComponentInputReceiver componentInputReceiver;
	private SpellInputReceiver spellInputReceiver;
	private SpawnObjectByInput tempSpawnObjectByInput;
	private Transform tempSpellSprite;
	private GameObject[] activeEmitters = new GameObject[3];
	private GameObject[] activeSpellSprites = new GameObject[3];

	// Use this for initialization
	void Start () {
		playerSpellsReference = GetComponent<PlayerSpellsReference> ();
		componentInputReceiver = GameObject.Find ("Input Controller").GetComponent<ComponentInputReceiver> ();
		spellInputReceiver = GameObject.Find ("Input Controller").GetComponent<SpellInputReceiver> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canCreate) {
			if (spellSetUp) {
				CreateSpell ();
			} else { 	
				SetUpSpell ();
			}
			if (currentArrayNum == 1 || currentArrayNum == 2) {
				for (int i = 0; i < spellInputReceiver.inputSD.Length; i++) {
					if (spellInputReceiver.inputSD [i] && !EventSystem.current.IsPointerOverGameObject ()) {
						glows [0].SetActive (false);
						if (currentArrayNum == 2) {
							glows [1].SetActive (false);
						}
						componentsPanel.Activate ();
						currentArrayNum = 0;
						spellSetUp = false;
						creatingSpell = false;
					}
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
					glows [0].transform.position = craftingComponents [i].transform.position;
					glows [0].SetActive (true);
					componentsPanel.Activate ();
				} else if (currentArrayNum == 2) {
					glows [1].transform.position = craftingComponents [i].transform.position;
					glows [1].SetActive (true);
				} else if (currentArrayNum == 3) {
					glows [2].transform.position = craftingComponents [i].transform.position;
					glows [2].SetActive (true);
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
					if (activeSpellSprites [i] != null) {
						activeSpellSprites [i].SetActive (false);
					}
					if (GameObject.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name + " Emitter")) {
						tempSpawnObjectByInput = GameObject.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name + " Emitter").GetComponent<SpawnObjectByInput> ();
						tempSpawnObjectByInput.emitterNum = i + 1;
						tempSpawnObjectByInput.cooldownWheel = cooldownWheels [i];
						cooldownWheels [i].maxValue = tempSpawnObjectByInput.cooldown; 
						if (activeSpellsPanel.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name)) {
							tempSpellSprite = activeSpellsPanel.Find ((playerSpellsReference.spells [spellID [0]].componentSpells [spellID [1] * 10 + spellID [2]]).name);
							tempSpellSprite.position = spellSlots [i].transform.position;
							tempSpellSprite.gameObject.SetActive (true);
						}
						activeEmitters [i] = tempSpawnObjectByInput.gameObject;
						activeSpellSprites [i] = tempSpellSprite.gameObject;
					}
				}
				componentsPanel.Activate ();
				StartCoroutine (WaitToCreateSpell ());
			}
		}
	}

	IEnumerator WaitToCreateSpell () {
		canCreate = false;
		yield return new WaitForSeconds (0.2f);
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
