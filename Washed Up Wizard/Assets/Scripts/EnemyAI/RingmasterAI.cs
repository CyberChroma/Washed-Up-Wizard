using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingmasterAI : MonoBehaviour {

    /*public enum State {
        flamingHoop;
        acrobat;
        rollingBallEmitter;
    }*/

    public float timeBetweenFlamingHoops;
    public float timeBetweenJumps;
    public GameObject flamingHoopEmitter;
    public Transform[] jumpPoints;
    public float jumpForce;
    //public GameObject acrobatEmitter;
    //public GameObject rollingBallEmitter;
    //public GameObject pedistal;

    private bool canSpawn = false;
    private bool isJumping = false;
    private Transform movePos;
    private Rigidbody rb;
    private MoveByForce moveByForce;

	// Use this for initialization
	void Awake () {
        flamingHoopEmitter.SetActive(false);
        //acrobatEmitter.SetActive(false);
        //rollingBallEmitter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        moveByForce = GetComponent<MoveByForce>();
	}
	
    void OnEnable () {
        StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
        StartCoroutine(WaitToJump());
    }

	// Update is called once per frame
    void FixedUpdate () {
        if (canSpawn && !isJumping) {
            flamingHoopEmitter.SetActive(true);
            canSpawn = false;
            StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
        }
        if (isJumping) {
            MoveToPos();
        }
	}

    IEnumerator WaitToSpawn (float delay) {
        yield return new WaitForSeconds(delay);
        canSpawn = true;
    }

    IEnumerator WaitToJump () {
        yield return new WaitForSeconds(timeBetweenJumps);
        isJumping = true;
        canSpawn = false;
        movePos = jumpPoints[Random.Range(0, jumpPoints.Length - 1)];
        rb.AddForce(Vector3.up * jumpForce * 100 * Time.deltaTime, ForceMode.Impulse);
    }

    void MoveToPos () {
        moveByForce.dir = (movePos.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, movePos.rotation, 0.05f);
        if (Vector3.Distance(movePos.position, transform.position) <= 0.1f) {
            transform.rotation = movePos.rotation;
            rb.velocity = Vector3.zero;
            moveByForce.dir = Vector3.zero;
            isJumping = false;
            canSpawn = true;
            StartCoroutine(WaitToJump());
        } else if (Vector3.Distance(new Vector3 (movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f) {
            moveByForce.dir = Vector3.zero;
            transform.position = new Vector3 (movePos.position.x, transform.position.y, movePos.position.z);
            rb.velocity = new Vector3 (0, rb.velocity.y, 0);
        }
    }
}
