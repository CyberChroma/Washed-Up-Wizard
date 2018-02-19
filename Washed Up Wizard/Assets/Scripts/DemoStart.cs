using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoStart : MonoBehaviour {

    public GameObject player;
    public GameObject inputController;
    public FollowTargetLerp cameraMove;
    public GameObject demoStartScreen;
    public float runTime = 1.2f;

    private PlayerMoveInput playerMoveInput;

	// Use this for initialization
	void Awake () {
        playerMoveInput = player.GetComponent<PlayerMoveInput>();
        if (demoStartScreen) {
            demoStartScreen.SetActive(false);
        }
	}

    void Start () {
        StartCoroutine (StartDemo());
    }

    IEnumerator StartDemo () {
        cameraMove.enabled = false;
        inputController.SetActive(false);
        playerMoveInput.overrideInput = true;
        playerMoveInput.v = 1;
        playerMoveInput.h = 0;
        yield return new WaitForSeconds(runTime);
        playerMoveInput.v = 0;
        playerMoveInput.h = 0;
        yield return new WaitForSeconds(0.5f);
        cameraMove.enabled = true;
        inputController.SetActive(true);
        playerMoveInput.overrideInput = false;
        if (demoStartScreen) {
            demoStartScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
