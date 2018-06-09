using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    
    public string sceneToLoad = "";
    public float delay = 0;

    private SpecialInputReceiver specialInputReceiver;

    void OnEnable () {
        if (GameObject.Find("Input Controller") != null)
        {
            specialInputReceiver = GameObject.Find("Input Controller").GetComponent<SpecialInputReceiver>();
        }
        if (delay != 0)
        {
            StartCoroutine(WaitToLoadScene());
        }
    }

    void Update () {
        if (delay != 0 && specialInputReceiver.inputS)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    IEnumerator WaitToLoadScene () {
        Time.timeScale = 1;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void GoToScene (string scene) {
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);
    }
}
