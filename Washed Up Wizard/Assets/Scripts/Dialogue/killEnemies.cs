﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies : MonoBehaviour {

    public GameObject[] enemiesToKill;
    [HideInInspector] public bool taskComplete = false;

    private int killCount = 0;

    void Update() {
        foreach (GameObject enemy in enemiesToKill)
        {
            if (enemy.GetComponent<Health>().dead)
            {
                killCount ++;
            }
        }

        if (killCount == enemiesToKill.Length)
        {
            taskComplete = true;    
        }
    }
}
