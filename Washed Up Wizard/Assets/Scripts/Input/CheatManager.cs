using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

    public Health player;

    private bool stopDamage = false;
    private GameSaver gameSaver;
	
    void Start () {
        gameSaver = FindObjectOfType<GameSaver>();
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (player.enabled) {
                stopDamage = !stopDamage;
            }
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            if (GameObject.Find("Spell UI"))
            {
                for (int i = 0; i < gameSaver.spellUnlockStates.Length; i++)
                {
                    gameSaver.spellUnlockStates[i].unlocked = true;
                }
                GameObject.Find("Combinations Panel").GetComponent<CombinationsMenu>().SetUpCombinations();
                gameSaver.UpdateSpells();
            }
        }
        if (stopDamage && player.currentHealth < player.startHealth) {
            player.currentHealth = player.startHealth;
            player.ChangeHealth();
        }
	}
}
