using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour {

	private Transform player;

	void Awake () {
		player = GameObject.Find ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		if (player) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			Plane hPlane = new Plane (Vector3.up, player.position); // create a plane at 0,0,0 whose normal points to +Y
			float distance = 0; // Plane.Raycast stores the distance from ray.origin to the hit point in this variable
			if (hPlane.Raycast (ray, out distance)) { // if the ray hits the plane...
				transform.position = ray.GetPoint (distance); // get the hit point
			}
		}
	}
}
