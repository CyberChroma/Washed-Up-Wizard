using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LaunchToTarget : MonoBehaviour {

    public float launchAngle;
    public Transform target;
    public string targetString;

    private Rigidbody rb;

    void Awake () {
        rb = GetComponent<Rigidbody>();
        if (targetString != "")
        {
            target = GameObject.Find(targetString).transform;
        }
    }

    void OnEnable () {
        float gravity = Physics.gravity.magnitude;
        // Selected angle in radians
        float angle = launchAngle * Mathf.Deg2Rad;
        // Positions of this object and the target on the same plane
        Vector3 planarTarget = new Vector3(target.position.x, 0, target.position.z);
        Vector3 planarPostion = new Vector3(transform.position.x, 0, transform.position.z);
        // Planar distance between objects
        float distance = Vector3.Distance(planarTarget, planarPostion);
        // Distance along the y axis between objects
        float yOffset = transform.position.y - target.position.y;
        float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
        // Rotate our velocity to match the direction between the two objects
        float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (target.position.x > transform.position.x ? 1 : -1);
        Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
        // Fire!
        rb.AddForce(finalVelocity * rb.mass * (rb.drag + 1), ForceMode.Impulse);
        enabled = false;
    }
}