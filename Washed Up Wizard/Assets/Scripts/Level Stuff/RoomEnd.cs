using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomEnd : MonoBehaviour {

    public GameObject player;
    public GameObject inputController;
    public FollowTargetLerp cameraMove;
    public GameObject demoCompleteScreen;
    public string nextLevel;

    private bool activated;
    private PlayerMoveInput playerMoveInput;

	// Use this for initialization
	void Awake () {
        playerMoveInput = player.GetComponent<PlayerMoveInput>();
        if (demoCompleteScreen) {
            demoCompleteScreen.SetActive(false);
        }
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            if (!activated) {
                StartCoroutine(EndDemo());
            }
        }
    }

    IEnumerator EndDemo () {
        activated = true;
        cameraMove.enabled = false;
        inputController.SetActive(false);
        playerMoveInput.overrideInput = true;
        playerMoveInput.v = 1;
        playerMoveInput.h = 0;
        yield return new WaitForSeconds(2.5f);
        if (demoCompleteScreen) {
            demoCompleteScreen.SetActive(true);
        }
        else {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
