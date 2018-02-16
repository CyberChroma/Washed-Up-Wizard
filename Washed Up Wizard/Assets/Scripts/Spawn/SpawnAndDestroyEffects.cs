using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndDestroyEffects : MonoBehaviour {

    public GameObject spawnParticles;
    public GameObject deathParticles;

    void OnEnable () {
        if (spawnParticles) {
            Instantiate(spawnParticles, transform.position, Quaternion.identity);
        }
    }

    void OnDisable () {
        if (deathParticles) {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
        }
    }
}
