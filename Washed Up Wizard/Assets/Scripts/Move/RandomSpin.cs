using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpin : MonoBehaviour {

    public float maxSpeed = 10; // The max rotational speed
    public float smoothing = 1;

    void OnEnable () {
        transform.rotation = Quaternion.Euler(maxSpeed * new Vector3(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed))); // Setting the rotational velocity
    }

    // Update is called once per frame
    void FixedUpdate () {
        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, maxSpeed * new Vector3(Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed), Random.Range(-maxSpeed, maxSpeed)), smoothing * Time.deltaTime)); // Setting the rotational velocity
    }
}
