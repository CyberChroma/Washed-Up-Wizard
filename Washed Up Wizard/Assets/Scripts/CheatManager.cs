using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour {

    public Health player;

    private bool stopDamage = false;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U)) {
            if (player.enabled) {
                stopDamage = true;
            }
            else {
                stopDamage = false;
            }
        }
        if (stopDamage && player.currentHealth < player.startHealth) {
            player.currentHealth = player.startHealth;
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            SceneManager.LoadScene("Hospital Area");
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            SceneManager.LoadScene("Wendigo Boss");
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            SceneManager.LoadScene("Ice Cave");
        }
	}
}
