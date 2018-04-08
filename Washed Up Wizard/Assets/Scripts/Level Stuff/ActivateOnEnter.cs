using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnEnter : MonoBehaviour {

    public bool activate;
    public GameObject objectToActivate;

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player"))
        {
            if (activate)
            {
                objectToActivate.SetActive(true);
            }
            else
            {
                objectToActivate.SetActive(false);
            }
        }
    }
}
