using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnDeath : MonoBehaviour {

    public AudioClip audioClip; // The death sound
    public GameObject soundPlayer; // The object that plays the sound
    public float volume = 0.5f; // The volume of the sound

    private Health health;
    private AudioSource audioSource; // Reference to the sound player

	// Use this for initialization
	void Awake () {
        health = GetComponentInChildren<Health>();
	}
	
	// Update is called once per frame
	void Update () {
        if (health.currentHealth <= 0)
        {
            if (audioClip)
            { // If the audio clip is not null
                audioSource = Instantiate(soundPlayer, transform.position, Quaternion.identity).GetComponent<AudioSource>(); // Creates the sound player and gets the reference to the audio source
            }
            if (audioSource)
            {
                audioSource.volume = volume; // Sets the volume
                audioSource.clip = audioClip; // Sets the clip
                audioSource.Play(); // Plays the sound
            }
            enabled = false;
        }
	}
}
