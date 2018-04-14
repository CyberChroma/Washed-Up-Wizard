using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour {
    
    public static GameSaver instance = null;

    private int unlockedLevel = 1;
    private Button[] levelButtons;
    public bool[] spellsUnlocked;
    private SpellUnlockState[] spellUnlockStates;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            spellsUnlocked = new bool[20];
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        instance.NewScene();
        DontDestroyOnLoad(this.gameObject);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level Select" && Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].interactable = true;
            }
        }
    }

    public void NewScene()
    {
        if (GameObject.Find("Spell UI"))
        {
            spellUnlockStates = new SpellUnlockState[20];
            spellUnlockStates[0] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Fireball").GetComponent<SpellUnlockState>();
            spellUnlockStates[1] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Melee Attack").GetComponent<SpellUnlockState>();
            spellUnlockStates[2] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Heal Instant").GetComponent<SpellUnlockState>();
            spellUnlockStates[3] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Protection Ring").GetComponent<SpellUnlockState>();
            spellUnlockStates[4] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Forward Relative To Player").GetComponent<SpellUnlockState>();
            spellUnlockStates[5] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Bomb").GetComponent<SpellUnlockState>();
            spellUnlockStates[6] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Forward and Back").GetComponent<SpellUnlockState>();
            spellUnlockStates[7] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Homing Towards Mouse").GetComponent<SpellUnlockState>();
            spellUnlockStates[8] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Multi Shot").GetComponent<SpellUnlockState>();
            spellUnlockStates[9] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("5 Way Spin").GetComponent<SpellUnlockState>();
            spellUnlockStates[10] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Up and Down").GetComponent<SpellUnlockState>();
            spellUnlockStates[11] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Drop and Emit").GetComponent<SpellUnlockState>();
            spellUnlockStates[12] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Heal Over Time").GetComponent<SpellUnlockState>();
            spellUnlockStates[13] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Forward Bomb").GetComponent<SpellUnlockState>();
            spellUnlockStates[14] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Boomerang").GetComponent<SpellUnlockState>();
            spellUnlockStates[15] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Acid Pool").GetComponent<SpellUnlockState>();
            spellUnlockStates[16] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Protection Field").GetComponent<SpellUnlockState>();
            spellUnlockStates[17] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Move and Emit").GetComponent<SpellUnlockState>();
            spellUnlockStates[18] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Drop and Explode").GetComponent<SpellUnlockState>();
            spellUnlockStates[19] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Bounce").GetComponent<SpellUnlockState>();
        }
        if (!spellsUnlocked[0])
        {
            for (int i = 0; i < 6; i++)
            {
                spellsUnlocked[i] = true;
            }
        }
        else
        {
            for (int i = 0; i < spellUnlockStates.Length; i++)
            {
                spellUnlockStates[i].unlocked = spellsUnlocked[i];
            }
        }
        if (SceneManager.GetActiveScene().name == "Level Select")
        {
            levelButtons = new Button[11];
            levelButtons[0] = GameObject.Find("Hospital").GetComponent<Button>();
            levelButtons[1] = GameObject.Find("Slime Cube").GetComponent<Button>();
            levelButtons[2] = GameObject.Find("Frozen Tundra").GetComponent<Button>();
            levelButtons[3] = GameObject.Find("Wendigo").GetComponent<Button>();
            levelButtons[4] = GameObject.Find("Circus").GetComponent<Button>();
            levelButtons[5] = GameObject.Find("Ringmaster").GetComponent<Button>();
            levelButtons[6] = GameObject.Find("Nursery").GetComponent<Button>();
            levelButtons[7] = GameObject.Find("Big Limbed Babies").GetComponent<Button>();
            levelButtons[8] = GameObject.Find("Glitch Realm").GetComponent<Button>();
            levelButtons[9] = GameObject.Find("Owl Man").GetComponent<Button>();
            levelButtons[10] = GameObject.Find("Evil Witch").GetComponent<Button>();
            for (int i = 0; i < unlockedLevel; i++)
            {
                levelButtons[i].interactable = true;
            }
        }
        else if (SceneManager.GetActiveScene().name == ("Slime Cube Boss") && unlockedLevel < 2)
        {
            unlockedLevel = 2;
        }
        else if (SceneManager.GetActiveScene().name == ("Frozen Tundra") && unlockedLevel < 3)
        {
            unlockedLevel = 3;
        }
        else if (SceneManager.GetActiveScene().name == ("Wendigo Boss") && unlockedLevel < 4)
        {
            unlockedLevel = 4;
        }
        else if (SceneManager.GetActiveScene().name.StartsWith("Circus") && unlockedLevel < 5)
        {
            unlockedLevel = 5;
        }
        else if (SceneManager.GetActiveScene().name == ("Ringmaster Boss") && unlockedLevel < 6)
        {
            unlockedLevel = 6;
        }
        else if (SceneManager.GetActiveScene().name.StartsWith("Nursery") && unlockedLevel < 7)
        {
            unlockedLevel = 7;
        }
        else if (SceneManager.GetActiveScene().name == ("Big Limbed Babies Boss") && unlockedLevel < 8)
        {
            unlockedLevel = 8;
        }
        else if (SceneManager.GetActiveScene().name == ("Glitch Realm") && unlockedLevel < 9)
        {
            unlockedLevel = 9;
        }
        else if (SceneManager.GetActiveScene().name == ("Owl Man Boss") && unlockedLevel < 10)
        {
            unlockedLevel = 10;
        }
        else if (SceneManager.GetActiveScene().name == ("Evil Witch Boss") && unlockedLevel < 11)
        {
            unlockedLevel = 11;
        }
    }

    public void UpdateSpells () {
        for (int i = 0; i < spellUnlockStates.Length; i++)
        {
            spellsUnlocked[i] = spellUnlockStates[i].unlocked;
        }
    }
}
