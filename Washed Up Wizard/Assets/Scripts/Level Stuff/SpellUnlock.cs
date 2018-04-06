using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUnlock : MonoBehaviour {

    public GameObject spellToUnlock;

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            //spellToUnlock.SetActive(true);
            //GameObject.Find("Combinations Panel").GetComponent<CombinationsMenu>().SetUpCombinations ();
        }
    }
}
