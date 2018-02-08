using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsReference : MonoBehaviour {
    
	[System.Serializable]
	public class Spells {
		public GameObject[] componentSpells = new GameObject[100]; // List of 100 spells
	}

    public Spells[] spells = new Spells[10]; // 10 lists of 100 (for 1000 spell combinations)
}