using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour {

    public string trigger;
    public Animator anim;

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger(trigger);
        }
    }
}
