using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorChange : MonoBehaviour {

    public Texture2D cursorTexture;

    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot;
    private PauseManager pauseManager;

    void Start()
    {
        hotSpot = new Vector2(cursorTexture.width/2, cursorTexture.width/2);
        pauseManager = GameObject.Find("Pause Manager").GetComponent<PauseManager>();
    }
    void Update ()
    {
        // Default cursor now set in player settings
        if (!pauseManager.isPaused)
        {
            //Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else if (pauseManager.isPaused)
        {
            //Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}
