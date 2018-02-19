using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnd : MonoBehaviour {

    public GameObject player;
    public GameObject inputController;
    public FollowTargetLerp cameraMove;
    public GameObject demoCompleteScreen;

    private bool activated;
    private PlayerMoveInput playerMoveInput;

	// Use this for initialization
	void Awake () {
        playerMoveInput = player.GetComponent<PlayerMoveInput>();
        demoCompleteScreen.SetActive(false);
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
        yield return new WaitForSeconds(1);
        demoCompleteScreen.SetActive(true);
    }
}
