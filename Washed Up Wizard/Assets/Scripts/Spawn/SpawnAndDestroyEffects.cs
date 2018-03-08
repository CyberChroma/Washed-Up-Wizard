﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDestroyEffects : MonoBehaviour {

    public GameObject spawnParticles;
    public GameObject deathParticles;

    private Transform parent;

    void Awake () {
        parent = GameObject.Find("Spells").transform;
    }

    void OnEnable () {
        if (parent.gameObject.activeSelf && spawnParticles) {
            Instantiate(spawnParticles, transform.position, Quaternion.identity, parent);
        }
    }

    void OnDestroy () {
        if (parent.gameObject.activeSelf && deathParticles) {
            Instantiate(deathParticles, transform.position, Quaternion.identity, parent);
        }
    }
}
