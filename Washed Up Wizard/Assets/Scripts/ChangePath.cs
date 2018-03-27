using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePath : MonoBehaviour {

    public bool block = true;

    private Animator anim;
    private MeshRenderer mr;
    private BoxCollider[] bcs;

	// Use this for initialization
	void Awake () {
        anim = GetComponentInChildren<Animator>();
        mr = GetComponentInChildren<MeshRenderer>();
        bcs = GetComponents<BoxCollider>();
        if (block)
        {
            mr.enabled = false;
            foreach (BoxCollider bc in bcs)
            {
                if (!bc.isTrigger)
                {
                    bc.enabled = false;
                }
            }
        }
        else
        {
            anim.SetTrigger("Fall");
        }
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Player")) {
            if (block)
            {
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
            else
            {
                anim.SetTrigger("Rise");
                mr.enabled = true;
                foreach (BoxCollider bc in bcs)
                {
                    if (!bc.isTrigger)
                    {
                        bc.enabled = false;
                    }
                }
            }
        }
    }
}
