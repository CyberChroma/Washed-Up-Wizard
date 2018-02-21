using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public float minIntesity = 0;
    public float maxIntesity = 1;
    public float smoothing = 1;

    private Light lightComp;

	// Use this for initialization
	void Awake () {
        lightComp = GetComponent<Light>();
        lightComp.intensity = Random.Range(minIntesity, maxIntesity);
	}

	// Update is called once per frame
	void Update () {
        lightComp.intensity = Mathf.Lerp (lightComp.intensity, Random.Range(minIntesity, maxIntesity), smoothing * Time.deltaTime);
	}
}
