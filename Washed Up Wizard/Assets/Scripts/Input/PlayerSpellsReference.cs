using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellsReference : MonoBehaviour {
	[System.Serializable]
	public class Spells {
		public GameObject[] componentSpells = new GameObject[100];
	}

	public Spells[] spells = new Spells[10];
}