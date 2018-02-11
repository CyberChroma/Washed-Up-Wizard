﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationsMenu : MonoBehaviour {

	public PlayerSpellsReference playerSpellReference; // Script reference
	public float highestCombo = 120; // How high the highest combo is placed
	public float spaceBetweenCombos = 60;
	public float combosPerPage = 2;

	private int spellNum = 0;
	private int currentPage = 0;
	private int maxPage;
	private Transform tempCombo;
	private List<Transform> combos = new List<Transform> (); // Different than an array because the length can be changed

	// Use this for initialization
	void Start () {
		SetUpCombinations ();
	}

	void SetUpCombinations () {
		spellNum = 0;
		currentPage = 0;
		combos.Clear ();
		for (int i = 0; i < playerSpellReference.spells.Length; i++) { // i and j go are used to go through each spell, which is an array inside of an array
			for (int j = 0; j < playerSpellReference.spells [i].componentSpells.Length; j++) {
				if (playerSpellReference.spells [i].componentSpells [j] != null) { // If the spell exists
					if (spellNum - combosPerPage * currentPage >= combosPerPage) { // If the spell should go to the next page
						currentPage++;
					}
					tempCombo = transform.Find (playerSpellReference.spells [i].componentSpells [j].name); // Reference to the spell combo sprites
					tempCombo.localPosition = new Vector3 (600, highestCombo - spaceBetweenCombos * (spellNum - combosPerPage * currentPage), 0); // Moving the spell combo sprites
					combos.Add (tempCombo); // Adding it to the list
					spellNum++;

				}
			}
		}
		maxPage = currentPage;
		SetPage (0);
	}

	void SetPage (int page) { // Sets the spells on the current page active and everything else inactive
		foreach (Transform combo in combos) {
			combo.gameObject.SetActive (false);
		}
		for (int i = 0; i < combos.Count; i++) {
			if (i >= combosPerPage * page && i < combosPerPage * (page + 1)) {
				combos [i].gameObject.SetActive (true);
			}
		}
		currentPage = page;
	}

	public void NextPage () {
		if (currentPage != maxPage) {
			SetPage (currentPage + 1);
		}
	}

	public void PreviousPage () {
		if (currentPage != 0) {
			SetPage (currentPage - 1);
		}
	}
}