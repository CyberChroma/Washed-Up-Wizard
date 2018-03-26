using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePath : MonoBehaviour {

    private Animator anim;
    private MeshRenderer mr;
    private BoxCollider[] bcs;


	// Use this for initialization
	void Awake () {
        anim = GetComponentInChildren<Animator>();
        mr = GetComponentInChildren<MeshRenderer>();
        bcs = GetComponents<BoxCollider>();
        mr.enabled = false;
        foreach (BoxCollider bc in bcs)
        {
            if (!bc.isTrigger)
            {
                bc.enabled = false;
            }
        }
	}

    void OnTriggerEnter (Collider other) {
        print("Running");
        if (other.CompareTag("Player")) {
            anim.SetTrigger("Fall");
            mr.enabled = true;
            foreach (BoxCollider bc in bcs)
            {
                if (!bc.isTrigger)
                {
                    bc.enabled = true;
                }
            }        
        }
    }
}
