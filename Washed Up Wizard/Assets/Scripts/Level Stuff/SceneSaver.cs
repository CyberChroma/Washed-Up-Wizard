using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour {

    public static SceneSaver instance = null;

    private float currentHealth = 10;
    private static Health health;
    private static SpellCreator spellCreator;
    private int[] spellID1 = new int[3];
    private int[] spellID2 = new int[3];
    private int[] spellID3 = new int[3];

	// Use this for initialization
	void Awake () {
        health = GameObject.Find("Player").GetComponent<Health>();
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);  
        }
        DontDestroyOnLoad(this.gameObject);
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

    public void UpdateSpells (int[] currentID, int spellNum) {
        switch (spellNum) {
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
        spellCreator = GameObject.Find("Spell UI").GetComponent<SpellCreator>();
        spellCreator.CreateSpell(spellID1, 0);
        spellCreator.CreateSpell(spellID2, 1);
        spellCreator.CreateSpell(spellID3, 2);
    }
}
