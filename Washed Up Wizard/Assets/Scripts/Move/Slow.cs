using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slow : MonoBehaviour {

    public float slowSpeed = 3f;

    private PlayerMoveInput player;
    private float hold;

	void Awake () {
        player = FindObjectOfType<PlayerMoveInput>();
        hold = player.moveSpeed;
	}
	
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
        player.moveSpeed = slowSpeed;
        }
	}

    void OnTriggerExit(Collider other){
        if(other.CompareTag("Player"))
        {
            player.moveSpeed = hold;
        }
    }
}
