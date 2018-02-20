using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLightFlash : MonoBehaviour {

    public float minIntesity = 0;
    public float maxIntesity = 1;
    public float smoothing = 0.25f;

    private Light light;

	// Use this for initialization
	void Awake () {
        light = GetComponent<Light>();
        light.intensity = Random.Range(minIntesity, maxIntesity);
	}

	// Update is called once per frame
	void Update () {
        light.intensity = Mathf.Lerp (light.intensity, Random.Range(minIntesity, maxIntesity), smoothing);
	}
}
