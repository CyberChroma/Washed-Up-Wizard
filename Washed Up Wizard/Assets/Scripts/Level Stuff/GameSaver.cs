using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSaver : MonoBehaviour {
    
    public static GameSaver instance = null;

    [HideInInspector] public KeyCode[] keys;
    [HideInInspector] public SpellUnlockState[] spellUnlockStates;

    private int unlockedLevel = 1;
    private GameObject[] levelButtons;
    private bool[] spellsUnlocked;
    private float volume = 1f;
    private VolumeUI volumeUI;
    private GameObject inputController;
    private MoveInputReceiver moveInputReceiver;
    private SpellInputReceiver spellInputReceiver;
    private SpecialInputReceiver specialInputReceiver;

    // Use this for initialization
    void Awake ()
    {
        Time.timeScale = 1;
        if (instance == null)
        {
            instance = this;
            spellsUnlocked = new bool[20];
            inputController = GameObject.Find("Input Controller");
            volumeUI = FindObjectOfType<VolumeUI>();
            if (inputController && keys.Length == 0)
            {
                moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
                spellInputReceiver = inputController.GetComponent<SpellInputReceiver>();
                specialInputReceiver = inputController.GetComponent<SpecialInputReceiver>();
                keys = new KeyCode[] { moveInputReceiver.moveForward, moveInputReceiver.moveBack, moveInputReceiver.moveLeft, moveInputReceiver.moveRight, spellInputReceiver.spellSlots[0], spellInputReceiver.spellSlots[1], spellInputReceiver.spellSlots[2], spellInputReceiver.toggleSpellBook, spellInputReceiver.previousPage, spellInputReceiver.nextPage, specialInputReceiver.teleport, specialInputReceiver.pause, specialInputReceiver.advanceText, specialInputReceiver.skipCutscenes }; 
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name != "Level Select")
        {
            instance.NewScene();
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void NewScene ()
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
            spellUnlockStates[9] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Bounce").GetComponent<SpellUnlockState>();
            spellUnlockStates[10] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Up and Down").GetComponent<SpellUnlockState>();
            spellUnlockStates[11] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Drop and Emit").GetComponent<SpellUnlockState>();
            spellUnlockStates[12] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Heal Over Time").GetComponent<SpellUnlockState>();
            spellUnlockStates[13] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Forward Bomb").GetComponent<SpellUnlockState>();
            spellUnlockStates[14] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Boomerang").GetComponent<SpellUnlockState>();
            spellUnlockStates[15] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Acid Pool").GetComponent<SpellUnlockState>();
            spellUnlockStates[16] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Protection Field").GetComponent<SpellUnlockState>();
            spellUnlockStates[17] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Move and Emit").GetComponent<SpellUnlockState>();
            spellUnlockStates[18] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("Drop and Explode").GetComponent<SpellUnlockState>();
            spellUnlockStates[19] = GameObject.Find("Spell UI").transform.Find("Combinations Panel").Find("5 Way Spin").GetComponent<SpellUnlockState>();
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
        }
        if (SceneManager.GetActiveScene().name == "Level Select")
        {
            levelButtons = new GameObject[11];
            levelButtons[0] = GameObject.Find("Hospital");
            levelButtons[1] = GameObject.Find("Slime Queen");
            levelButtons[2] = GameObject.Find("Frozen Tundra");
            levelButtons[3] = GameObject.Find("Wendigo");
            levelButtons[4] = GameObject.Find("Circus");
            levelButtons[5] = GameObject.Find("Ringmaster");
            levelButtons[6] = GameObject.Find("Nursery");
            levelButtons[7] = GameObject.Find("The Twins");
            levelButtons[8] = GameObject.Find("Glitch Realm");
            levelButtons[9] = GameObject.Find("Owl Man");
            levelButtons[10] = GameObject.Find("Evil Witch");
            for (int i = unlockedLevel; i < levelButtons.Length; i++)
            {
                if (levelButtons[i])
                {
                    levelButtons[i].SetActive(false);
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == ("Slime Queen Boss") && unlockedLevel < 2)
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
        else if (SceneManager.GetActiveScene().name == ("The Twins Boss") && unlockedLevel < 8)
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
        inputController = GameObject.Find("Input Controller");
        if (inputController && keys.Length != 0)
        {
            moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
            spellInputReceiver = inputController.GetComponent<SpellInputReceiver>();
            specialInputReceiver = inputController.GetComponent<SpecialInputReceiver>();
            moveInputReceiver.moveForward = keys[0];
            moveInputReceiver.moveBack = keys[1];
            moveInputReceiver.moveLeft = keys[2];
            moveInputReceiver.moveRight = keys[3];
            spellInputReceiver.spellSlots[0] = keys[4];
            spellInputReceiver.spellSlots[1] = keys[5];
            spellInputReceiver.spellSlots[2] = keys[6];
            spellInputReceiver.toggleSpellBook = keys[7];
            spellInputReceiver.previousPage = keys[8];
            spellInputReceiver.nextPage = keys[9];
            specialInputReceiver.teleport = keys[10];
            specialInputReceiver.pause = keys[11];
            specialInputReceiver.advanceText = keys[12];
            specialInputReceiver.skipCutscenes = keys[13]; 
        }
        if (GameObject.Find("Pause Menu") != null)
        {
            volumeUI = GameObject.Find("Pause Menu").transform.Find("Pause Screen").Find("Volume Slider").GetComponent<VolumeUI>();
            if (volumeUI != null)
            {
                volumeUI.GetComponent<Slider>().value = volume;
                AudioListener.volume = volume;
            }
        }
        if (TempSaver.instance != null)
        {
            TempSaver.instance.NewScene();
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.dat");
        SaveData data = new SaveData();
        data.unlockedLevel = unlockedLevel;
        for (int i = 0; i < spellsUnlocked.Length; i++)
        {
            data.spellsUnlocked[i] = spellsUnlocked[i];
        }
        for (int i = 0; i < keys.Length; i++)
        {
            data.keys[i] = keys[i];
        }
        bf.Serialize(file, data);
        file.Close();
    }

    public void UpdateSpells () {
        for (int i = 0; i < spellUnlockStates.Length; i++)
        {
            spellsUnlocked[i] = spellUnlockStates[i].unlocked;
        }
    }

    public void ContinueGame () {
        if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            unlockedLevel = data.unlockedLevel;
            for (int i = 0; i < spellsUnlocked.Length; i++)
            {
                spellsUnlocked[i] = data.spellsUnlocked[i];
            }
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = data.keys[i];
            }
        }
        LevelSelect();
    }

    public void UpdateVolume (float newVolume) {
        volume = newVolume;
    }

    public void LevelSelect () {
        GameObject.Find("Main Menu").SetActive(false);
        GameObject.Find("Canvas").transform.Find("Level Select").gameObject.SetActive(true);
        NewScene(); 
    }
}

[System.Serializable]
class SaveData
{
    public int unlockedLevel;
    public bool[] spellsUnlocked = new bool[20];
    public KeyCode[] keys = new KeyCode[14];
    public float volume = 1f;
}