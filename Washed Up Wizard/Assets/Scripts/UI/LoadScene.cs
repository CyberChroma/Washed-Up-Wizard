using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public string sceneToLoad = "";
    public float delay = 0;

    void OnEnable () {
        if (delay != 0)
        {
            StartCoroutine(WaitToLoadScene());
        }
    }

    IEnumerator WaitToLoadScene () {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneToLoad);
    }

    public void GoToScene (string scene) {
        SceneManager.LoadScene(scene);
    }
}
