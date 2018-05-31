using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChange : MonoBehaviour {

    public GameObject inputManager;
    public Text[] keyTexts;

    private MoveInputReceiver moveInputReceiver;
    private SpellInputReceiver spellInputReceiver;
    private SpecialInputReceiver specialInputReceiver;
    private Event keyEvent;
    private bool waitingForKey = false;
    private KeyCode newKey;
    private GameSaver gameSaver;

	// Use this for initialization
	void Awake () {
        moveInputReceiver = inputManager.GetComponent<MoveInputReceiver>();
        spellInputReceiver = inputManager.GetComponent<SpellInputReceiver>();
        specialInputReceiver = inputManager.GetComponent<SpecialInputReceiver>();
	}

    void Start () {
        gameSaver = GameSaver.instance;
        if (gameSaver.keys.Length == 0)
        {
            gameSaver.keys = new KeyCode[] { moveInputReceiver.moveForward, moveInputReceiver.moveBack, moveInputReceiver.moveLeft, moveInputReceiver.moveRight, spellInputReceiver.spellSlots[0], spellInputReceiver.spellSlots[1], spellInputReceiver.spellSlots[2], spellInputReceiver.toggleSpellBook, spellInputReceiver.previousPage, spellInputReceiver.nextPage, specialInputReceiver.teleport, specialInputReceiver.pause, specialInputReceiver.advanceText, specialInputReceiver.skipCutscenes };         
        }
        for (int i = 0; i < keyTexts.Length; i++)
        {
            keyTexts[i].text = gameSaver.keys[i].ToString();
        }
    }
    void OnGUI () {
        keyEvent = Event.current;
        if ((keyEvent.isKey || keyEvent.isMouse || keyEvent.shift) && waitingForKey) {
            if (keyEvent.isMouse) {
                if (keyEvent.button == 0) {
                    newKey = KeyCode.Mouse0;
                }
                else if (keyEvent.button == 1) {
                    newKey = KeyCode.Mouse1;
                }
                else {
                    newKey = KeyCode.Mouse2;
                }
            }
            else if (keyEvent.isKey) {
                newKey = keyEvent.keyCode;
            }
            if (keyEvent.shift) {
                newKey = KeyCode.LeftShift;
            }
            waitingForKey = false;
        }
    }

    public void ChangeInput (int keyNum) {
        waitingForKey = true;
        StartCoroutine(WaitForNewKey (keyNum));
    }

    IEnumerator WaitForNewKey (int keyNum) {
        while (waitingForKey) {
            yield return null;
        }
        AssignKey (keyNum, newKey);
        yield return null;
    }

    void AssignKey (int keyNum, KeyCode newKey) {
        keyTexts[keyNum].text = newKey.ToString();
        gameSaver.keys[keyNum] = newKey;
        switch (keyNum) {
            case 0:
                moveInputReceiver.moveForward = newKey;
                return;
            case 1:
                moveInputReceiver.moveBack = newKey;
                return;
            case 2:
                moveInputReceiver.moveLeft = newKey;
                return;
            case 3:
                moveInputReceiver.moveRight = newKey;
                return;
            case 4:
                spellInputReceiver.spellSlots [0] = newKey;
                return;
            case 5:
                spellInputReceiver.spellSlots [1] = newKey;
                return;
            case 6:
                spellInputReceiver.spellSlots [2] = newKey;
                return;
            case 7:
                spellInputReceiver.toggleSpellBook = newKey;
                return;
            case 8:
                spellInputReceiver.previousPage = newKey;
                 return;
            case 9:
                spellInputReceiver.nextPage = newKey;
                return;
            case 10:
                specialInputReceiver.teleport = newKey;
                return;
            case 11:
                specialInputReceiver.pause = newKey;
                return;
            case 12:
                specialInputReceiver.advanceText = newKey;
                return;
            case 13:
                specialInputReceiver.skipCutscenes = newKey;
                return;
            default:
                print("Invalid Key Number");
                return;
        }
    }
}
