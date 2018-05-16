using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorChange : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    private Vector2 hotSpot;
    private GameObject PauseManager;
    void Start()
    {
        hotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.width / 2);
        PauseManager = GameObject.Find("Pause Manager");
    }
    void Update ()
    {
        if (!PauseManager.GetComponent<PauseManager>().isPaused)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else if (PauseManager.GetComponent<PauseManager>().isPaused)
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
