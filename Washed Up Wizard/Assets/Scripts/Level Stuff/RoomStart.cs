using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStart : MonoBehaviour {

    public GameObject player;
    public GameObject inputController;
    public FollowTargetLerp cameraMove;
    public float runTime = 1.2f;

    private PlayerMoveInput playerMoveInput;

	// Use this for initialization
	void Awake () {
        playerMoveInput = player.GetComponent<PlayerMoveInput>();
	}

    void Start () {
        StartCoroutine(StartDemo());
    }

    IEnumerator StartDemo () {
            cameraMove.enabled = false;
            inputController.SetActive(false);
            playerMoveInput.overrideInput = true;
        if (runTime > 0)
        {
            playerMoveInput.v = 1;
            playerMoveInput.h = 0;
            yield return new WaitForSeconds(runTime);
            playerMoveInput.v = 0;
            playerMoveInput.h = 0;
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }
        cameraMove.enabled = true;
        inputController.SetActive(true);
        playerMoveInput.overrideInput = false;
    }
}
