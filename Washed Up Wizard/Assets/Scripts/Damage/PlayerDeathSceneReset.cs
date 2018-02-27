using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Used to change scenes

public class PlayerDeathSceneReset : MonoBehaviour {

	public float reloadDelay = 2; // How long after the player has died should the scene reset

    private Health health;
    private bool resetting = false;
    private PauseManager pauseManager;

	// Use this for initialization
	void Awake () {
        health = GameObject.Find ("Player").GetComponent<Health>(); // Gets a reference to the player
	}
	
	// Update is called once per frame
	void Update () {
        if (health.currentHealth <= 0 && !resetting || health.transform.position.y < -50) {
            StartCoroutine(ResetScene ());
            resetting = true;
        }
	}

    IEnumerator ResetScene () {
        yield return new WaitForSeconds (reloadDelay); // Waits...
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); // Resets the scene
	}
}
