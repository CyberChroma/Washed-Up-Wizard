using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

	public GameObject pauseScreen; // Reference to the pause screen panel
	public GameObject controlsScreen; // Reference to the control screen panel
    public GameObject changeControlsScreen;
    public GameObject player;
    public SpecialInputReceiver specialInputReceiver;

	[HideInInspector] public bool isPaused; // Bool for if the game is paused


	// Use this for initialization
	void Start () {
		Resume ();
	}

	// Update is called once per frame
	void Update () {
        if (specialInputReceiver.inputP) {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
		}
	}

	void OnApplicationPause () { // Runs when the application is paused
		Pause ();
	}

	void Pause () { // Pausing the game
		pauseScreen.SetActive (true); // Activates the pause screen panel
		Time.timeScale = 0; // Freezes time
		isPaused = true; // Setting the bool
	}

	public void Resume () { // Resuming the game
        changeControlsScreen.SetActive(false);
		controlsScreen.SetActive (false); // Deactivates the control screen panel
		pauseScreen.SetActive (false); // Deactivates the pause screen panel
		Time.timeScale = 1; // Unfreezes time
		isPaused = false; // Setting the bool

	}

    public void LevelSelect () {
        SceneManager.LoadScene("Level Select");
    }

    public void ChangeControls () {
        pauseScreen.SetActive(false);
        changeControlsScreen.SetActive(true);
    }

	public void Controls () { // Bringing up the controls screen
		controlsScreen.SetActive (true); // Activates the control screen panel
		pauseScreen.SetActive (false); // Deactivates the pause screen panel
	}

	public void Back () { // Going back to the pause screen
		controlsScreen.SetActive (false); // Deactivates the control screen panel
        changeControlsScreen.SetActive (false);
		pauseScreen.SetActive (true); // Activates the pause screen panel
	}

	public void Quit () { // Quitting the game
		Application.Quit (); // Quits the game
	}
}
