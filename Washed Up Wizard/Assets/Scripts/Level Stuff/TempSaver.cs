﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempSaver : MonoBehaviour {

    public static TempSaver instance = null;

    private float currentHealth = 10;
    private static Health health;
    private static SpellCreator spellCreator;
    private int[] spellID1 = new int[3];
    private int[] spellID2 = new int[3];
    private int[] spellID3 = new int[3];

	// Use this for initialization
	void Awake () {
        health = GameObject.Find("Player").GetComponent<Health>();
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            if (SceneManager.GetActiveScene().name == "Hospital")
            {
                Destroy(instance.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start () {
        if (SceneManager.GetActiveScene().name == "Hospital")
        {
            spellID1 = new int[] {2, 0, 6};
            spellID2 = new int[] {3, 7, 9};
            spellID3 = new int[] {9, 1, 1};
            TransferSpells();
            spellCreator = GameObject.Find("Spell Crafting System").GetComponent<SpellCreator>();
            spellCreator.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (health && health.currentHealth != currentHealth) {
            currentHealth = health.currentHealth;
        }
        if (SceneManager.GetActiveScene().name == "Level Select")
        {
            Destroy(gameObject);
        }
	}

    public void TransferHealth () {
        if (currentHealth > 0) {
            health.currentHealth = currentHealth;
        }
    }

    public void UpdateSpells (int[] currentID, int slotNum) {
        switch (slotNum) {
            case 0:
                spellID1 = currentID;
                break;
            case 1:
                spellID2 = currentID;
                break;
            case 2:
                spellID3 = currentID;
                break;
        }
    }

    public void TransferSpells () {
        if (!spellCreator)
        {
            spellCreator = GameObject.Find("Spell Crafting System").GetComponent<SpellCreator>();
        }
        spellCreator.CreateSpell(spellID1, 0);
        spellCreator.CreateSpell(spellID2, 1);
        spellCreator.CreateSpell(spellID3, 2);
    }
}
