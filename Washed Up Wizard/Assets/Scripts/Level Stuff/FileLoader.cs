using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileLoader: MonoBehaviour {

    void Update () {
        if (Input.GetKey(KeyCode.Escape) && GameObject.Find("Level Select")) {
            GameObject.Find("Level Select").SetActive(false);
            GameObject.Find("Canvas").transform.Find("Main Menu").gameObject.SetActive(true);
        }
    }
    public void ContinueGame () {
        GameSaver.instance.ContinueGame();
    }

    public void LevelSelect () {
        GameSaver.instance.LevelSelect();
    }

    public void Quit() {
        Application.Quit();
    }
}