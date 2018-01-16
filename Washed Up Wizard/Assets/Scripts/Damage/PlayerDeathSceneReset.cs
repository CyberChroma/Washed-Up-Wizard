using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathSceneReset : MonoBehaviour {

	public float delay = 2;

	private GameObject player;

	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null) {
			StartCoroutine (ResetScene ());
		}
	}

	IEnumerator ResetScene () {
		yield return new WaitForSeconds (delay);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
