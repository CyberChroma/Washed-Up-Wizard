using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour {

	public GameObject pauseScreen; // Reference to the pause screen panel
	public GameObject controlsScreen; // Reference to the control screen panel
    public GameObject inputController;
    public GameObject player;
    public KeyCode pause = KeyCode.Escape;  // The key to pause the game


	[HideInInspector] public bool isPaused; // Bool for if the game is paused
    [HideInInspector] public bool inputP;

    private MoveInputReceiver moveInputReceiver;
    private SpellInputReceiver spellInputReceiver;
    private ComponentInputReceiver componentInputReceiver;
    private PlayerAbilities playerAbilities;


	// Use this for initialization
	void Start () {
        moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
        spellInputReceiver = inputController.GetComponent<SpellInputReceiver>();
        componentInputReceiver = inputController.GetComponent<ComponentInputReceiver>();
        playerAbilities = player.GetComponent<PlayerAbilities>();
		Resume ();
	}

	// Update is called once per frame
	void Update () {
        inputP = Input.GetKeyDown (pause); // Getting input for pausing
		if (inputP && !isPaused) { // Getting input to pause the game and making sure the game is not already paused
			Pause ();
		}
	}

	void OnApplicationPause () { // Runs when the application is paused
		Pause ();
	}

	void Pause () { // Pausing the game
		pauseScreen.SetActive (true); // Activates the pause screen panel
		Time.timeScale = 0; // Freezes time
        if (moveInputReceiver) {
            moveInputReceiver.enabled = false;
            spellInputReceiver.enabled = false;
            componentInputReceiver.enabled = false;
            playerAbilities.enabled = false;
        }
		isPaused = true; // Setting the bool
	}

	public void Resume () { // Resuming the game
		controlsScreen.SetActive (false); // Deactivates the control screen panel
		pauseScreen.SetActive (false); // Deactivates the pause screen panel
		Time.timeScale = 1; // Unfreezes time
        if (moveInputReceiver) {
            moveInputReceiver.enabled = true;
            spellInputReceiver.enabled = true;
            componentInputReceiver.enabled = true;
            playerAbilities.enabled = true;
        }
		isPaused = false; // Setting the bool

	}

	public void Controls () { // Bringing up the controls screen
		controlsScreen.SetActive (true); // Activates the control screen panel
		pauseScreen.SetActive (false); // Deactivates the pause screen panel
	}

	public void Back () { // Going back to the pause screen
		controlsScreen.SetActive (false); // Deactivates the control screen panel
		pauseScreen.SetActive (true); // Activates the pause screen panel
	}

	public void Quit () { // Quitting the game
		Application.Quit (); // Quits the game
	}
}
