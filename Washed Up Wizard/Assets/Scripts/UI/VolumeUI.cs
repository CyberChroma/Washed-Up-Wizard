using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour {

    public void VolumeChanged () {
        AudioListener.volume = GetComponent<Slider>().value;
        FindObjectOfType<GameSaver>().UpdateVolume(GetComponent<Slider>().value);
    }
}
