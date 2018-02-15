using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Used to change scenes

public class PlayerDeathSceneReset : MonoBehaviour {

	public float delay = 2; // How long after the player has died should the scene reset

    private Health health;
    private bool resetting = false;

	// Use this for initialization
	void Awake () {
        health = GameObject.Find ("Player").GetComponent<Health>(); // Gets a reference to the player
	}
	
	// Update is called once per frame
	void Update () {
        if (health.currentHealth <= 0 && !resetting) {
            StartCoroutine(ResetScene());
            resetting = true;
        }
	}

	IEnumerator ResetScene () {
		yield return new WaitForSeconds (delay); // Waits...
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); // Resets the scene
	}
}
