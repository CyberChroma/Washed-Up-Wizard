using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinationsMenu : MonoBehaviour {

	public PlayerSpellsReference playerSpellReference;
	public float highestCombo = 120;
	public float spaceBetweenCombos = 60;
	public float combosPerPage = 2;

	private int spellNum = 0;
	private int currentPage = 0;
	private int maxPage;
	private Transform tempCombo;
	private List<Transform> combos = new List<Transform> ();

	// Use this for initialization
	void Start () {
		SetUpCombinations ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetUpCombinations () {
		spellNum = 0;
		currentPage = 0;
		combos.Clear ();
		for (int i = 0; i < playerSpellReference.spells.Length; i++) {
			for (int j = 0; j < playerSpellReference.spells [i].componentSpells.Length; j++) {
				if (playerSpellReference.spells [i].componentSpells [j] != null) {
					if (spellNum - combosPerPage * currentPage >= combosPerPage) {
						currentPage++;
					}
					tempCombo = transform.Find (playerSpellReference.spells [i].componentSpells [j].name);
					tempCombo.localPosition = new Vector3 (600, highestCombo - spaceBetweenCombos * (spellNum - combosPerPage * currentPage), 0);
					combos.Add (tempCombo);
					spellNum++;

				}
			}
		}
		maxPage = currentPage;
		SetPage (0);
	}

	void SetPage (int page) {
		foreach (Transform combo in combos) {
			combo.gameObject.SetActive (false);
		}
		for (int i = 0; i < combos.Count; i++) {
			if (i >= combosPerPage * page && i < combosPerPage * (page + 1)) {
				combos [i].gameObject.SetActive (true);
			}
			/*if (i > combosPerPage * page + 1) {
				break;
			}*/
		}
		currentPage = page;
	}

	public void NextPage () {
		//print (currentPage);
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
