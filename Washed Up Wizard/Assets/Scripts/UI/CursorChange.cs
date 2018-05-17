using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorChange : MonoBehaviour {

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    private Vector2 hotSpot = Vector2.zero;
    private GameObject PauseManager;
    void Start()
    {
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
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
    }
}
