﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoBossTrigger : MonoBehaviour {

    public GameObject wendigo;
    public GameObject wenidgoHealthBar;
    public GameObject harpy;
    public ActivateFollowTarget iceWall;

	// Use this for initialization
	void Awake () {
        wendigo.SetActive(false);
        wenidgoHealthBar.SetActive(false);
        harpy.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            wendigo.SetActive(true);
            wenidgoHealthBar.SetActive(true);
            harpy.SetActive(true);
            iceWall.Activate();
            Destroy(gameObject, 1f);
        }
    }
}