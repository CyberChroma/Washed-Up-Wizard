using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTransform : MonoBehaviour {

	public float rotSpeed = 10; // The rotational speed
	public Vector3 dir = Vector3.zero; // The direction

	// Update is called once per frame
    void FixedUpdate () {
        transform.Rotate (rotSpeed * dir * Time.deltaTime); // Rotating the object
	}
}
