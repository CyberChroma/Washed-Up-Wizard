using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour {

    public float maxSpeed = 10;
    public float smoothing = 1;

    void OnEnable () {
        transform.rotation = Quaternion.Euler(new Vector3(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed))); // Setting the rotational velocity
    }

    // Update is called once per frame
    void FixedUpdate () {
        transform.Rotate (maxSpeed * new Vector3(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed)) * Time.deltaTime); // Spinning the object randomly
    }
}
