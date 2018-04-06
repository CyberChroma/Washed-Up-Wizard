using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    void OnTriggerEnter (Collider other){
        gameObject.SetActive(false);
    }
}
