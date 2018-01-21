using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationsMenu : MonoBehaviour {

	public PlayerSpellsReference playerSpellReference;
	public float highestCombo = 120;
	public float spaceBetweenCombos = 60;
	public float combosPerPage = 6;

	private int spellNum = 0;
	private Transform spellCombo;

	// Use this for initialization
	void Start () {
		SetUpCombinations ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetUpCombinations () {
		spellNum = 0;
		for (int i = 0; i < playerSpellReference.spells.Length; i++) {
			for (int j = 0; j < playerSpellReference.spells [i].componentSpells.Length; j++) {
				if (playerSpellReference.spells [i].componentSpells [j] != null) {
					spellCombo = transform.Find (playerSpellReference.spells [i].componentSpells [j].name);
					print (spellCombo.gameObject.name);
					spellCombo.localPosition = new Vector3 (600, highestCombo - spaceBetweenCombos * spellNum, 0);
					spellNum++;
				}
			}
		}
	}


}
