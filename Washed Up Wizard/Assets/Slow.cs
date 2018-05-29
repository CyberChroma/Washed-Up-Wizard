using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Slow : MonoBehaviour {

    public float slowSpeed = 3f;

    private float hold;
    private PlayerMoveInput player;

	void Awake () {
        player = FindObjectOfType<PlayerMoveInput>();
	}
	
    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
        hold = player.moveSpeed;
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
