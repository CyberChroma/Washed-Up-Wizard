using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [HideInInspector]
    public bool taskComplete = false;

    void OnTriggerEnter (Collider other){
        taskComplete = true;
    }
}
