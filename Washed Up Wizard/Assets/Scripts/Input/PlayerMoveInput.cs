using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveInput : MonoBehaviour {

	public float moveSensitivity = 0.5f;
	public float moveSpeed = 7f;
    public float iceSensitivity = 0.03f;
    public float stickySpeed = 5.5f;

    [HideInInspector] public float v = 0; // Vertical direction
    [HideInInspector] public float h = 0; // Horizontal direction
    [HideInInspector] public bool overrideInput = false;
    private float currentSensitivity;
    private Vector3 moveVector;

    // Getting refferences
	private Rigidbody rb;
    private Animator anim;
    private GameObject inputController;
	private MoveInputReceiver moveInputReceiver; // Reference to input manager
    private Health health;

	// Use this for initialization
	void Awake () {
        // Getting references
		rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        inputController = GameObject.Find("Input Controller");
        moveInputReceiver = inputController.GetComponent<MoveInputReceiver>();
        health = GetComponent<Health>();
        currentSensitivity = moveSensitivity;
	}

	void OnDisable () {
        // Resetting variables
		v = 0;
		h = 0;
		moveVector = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (health.currentHealth > 0) {
            // Calculating vertical direction
            if (!overrideInput) {
                if (moveInputReceiver.inputMB) {
                    v = Mathf.MoveTowards(v, -1, currentSensitivity);
                }
                else if (moveInputReceiver.inputMF) {
                    v = Mathf.MoveTowards(v, 1, currentSensitivity);
                }
                else {
                    v = Mathf.MoveTowards(v, 0, currentSensitivity);
                }
                // Calculating horizontal direction
                if (moveInputReceiver.inputML) {
                    h = Mathf.MoveTowards(h, -1, currentSensitivity);
                }
                else if (moveInputReceiver.inputMR) {
                    h = Mathf.MoveTowards(h, 1, currentSensitivity);
                }
                else {
                    h = Mathf.MoveTowards(h, 0, currentSensitivity);
                }
                if (transform.position.y > 1.5f)
                {
                    transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                }
                else if (transform.position.y < -5)
                {
                    health.currentHealth = 0;
                }
            }
            if (v != 0 || h != 0) {
                anim.SetBool("IsWalking", true);
            }
            else {
                anim.SetBool("IsWalking", false);
            }
            moveVector = new Vector3(h, 0, v); // Setting move vector for direction
            if (moveVector.magnitude > 1) { // If the move vector is greater than 1
                moveVector.Normalize(); // Setting direction
            }
            moveVector *= moveSpeed * Time.deltaTime; // Multiplying move vector for magnutude
            if (moveVector != Vector3.zero) { // Of the move vector is not zero
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveVector), 10f * Time.deltaTime); // Rotates in direction of movement
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0)); // Ignores x and z rotation
            }
            rb.MovePosition(rb.position + moveVector); // Moves player
        } else {
            v = 0;
            h = 0;
            moveVector = Vector3.zero;
            inputController.SetActive(false);
        }
	}

    void OnTriggerEnter (Collider other) {
        if (other.CompareTag("Ice"))
        {
            currentSensitivity = iceSensitivity;
        }
        else if (other.CompareTag("Sticky"))
        {
            moveSpeed = stickySpeed;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag("Ice"))
        {
            currentSensitivity = moveSensitivity;
        }
        else if (other.CompareTag("Sticky"))
        {
            moveSpeed = 7f;
        }
    }
}
