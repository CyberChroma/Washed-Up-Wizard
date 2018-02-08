using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Used to change scenes

public class PlayerDeathSceneReset : MonoBehaviour {

	public float delay = 2; // How long after the player has died should the scene reset

	private GameObject player; // Reference to the player

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player"); // Gets a reference to the player
	}
	
	// Update is called once per frame
	void Update () {
        if (player == null) { // If the player is not in the scene (has died and been destroyed)
			StartCoroutine (ResetScene ());
		}
	}

	IEnumerator ResetScene () {
		yield return new WaitForSeconds (delay); // Waits...
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name); // Resets the scene
	}
}
