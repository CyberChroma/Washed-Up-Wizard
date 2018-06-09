using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FileLoader: MonoBehaviour {

    public void ContinueGame () {
        GameSaver.instance.ContinueGame();
    }

    public void LevelSelect () {
        GameSaver.instance.LevelSelect();
    }
}