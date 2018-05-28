using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiggler : MonoBehaviour {

    public float smoothing = 0.1f;

    private Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(startPos.x + Random.Range(-1f, 1f), startPos.y + Random.Range(-1f, 1f), startPos.z + Random.Range(-1f, 1f)), smoothing * Time.deltaTime);
    }
}
