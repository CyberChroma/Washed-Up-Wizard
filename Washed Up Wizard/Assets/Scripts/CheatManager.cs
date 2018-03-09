using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {

    public Health player;

    private bool stopDamage = false;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U)) {
            if (player.enabled) {
                stopDamage != stopDamage;
            }
        }
        if (stopDamage && player.currentHealth < player.startHealth) {
            player.currentHealth = player.startHealth;
        }
	}
}
