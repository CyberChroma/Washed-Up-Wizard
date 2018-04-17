using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellUnlock : MonoBehaviour {

    public SpellUnlockState spellToUnlock;

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            spellToUnlock.unlocked = true;
            GameObject.Find("Combinations Panel").GetComponent<CombinationsMenu>().SetUpCombinations ();
            GameObject.Find("Game Saver").GetComponent<GameSaver>().UpdateSpells();
            gameObject.SetActive(false);
        }
    }
}
