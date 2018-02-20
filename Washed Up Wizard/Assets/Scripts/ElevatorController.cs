using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {

    public bool end;
    public GameObject player;

    private Animator anim;
    private PlayerMoveInput playerMoveInput;

	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animator>();
        playerMoveInput = player.GetComponent<PlayerMoveInput>();
        if (!end) {
            anim.SetTrigger("Open");
        }
	}
	
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player") && end) {
            StartCoroutine(Close ());
        }
    }

    IEnumerator Close () {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(0.1f);
        playerMoveInput.v = 0;
        playerMoveInput.h = 0;
        yield return new WaitForSeconds(1f);
        player.SetActive(false);
    }
}
