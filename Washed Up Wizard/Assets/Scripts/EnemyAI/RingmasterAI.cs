using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingmasterAI : MonoBehaviour {

    public enum AttackState {
        FlamingHoop,
        RollingBall
    }

    public float jumpForce;

    public float timeBetweenFlamingHoops;
    public float timeBetweenJumps;
    public GameObject flamingHoopEmitter;
    public Transform[] jumpPoints;

    public float timeBetweenAcrobats;
    public GameObject acrobatEmitter;

    public float timeBetweenRollingBalls;
    public GameObject rollingBallEmitter;
    public Transform pedistalJumpPoint;
    public GameObject pedistal;

    public AttackState attackState;
    private bool canSpawn = false;
    private bool isJumping = false;
    private bool canSpawnAcrobats = false;
    private Transform movePos;
    private Rigidbody rb;
    private MoveByForce moveByForce;

	// Use this for initialization
	void Awake () {
        flamingHoopEmitter.SetActive(false);
        acrobatEmitter.SetActive(false);
        rollingBallEmitter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        moveByForce = GetComponent<MoveByForce>();
	}
	
    void OnEnable () {
        attackState = AttackState.RollingBall;
        if (attackState == AttackState.FlamingHoop) {
            StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
            StartCoroutine(WaitToJump());
        }
        if (attackState == AttackState.RollingBall) {
            rb.AddForce(Vector3.up * jumpForce * 100 * Time.deltaTime, ForceMode.Impulse);
            movePos = pedistalJumpPoint;
            isJumping = true;
        }
        StartCoroutine(WaitToSpawnAcrobats());
    }

	// Update is called once per frame
    void FixedUpdate () {
        if (attackState == AttackState.FlamingHoop) {
            if (canSpawn && !isJumping) {
                flamingHoopEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
            }
            if (isJumping)
            {
                MoveToPos();
            }
        } else if (attackState == AttackState.RollingBall) {
            if (canSpawn && !isJumping) {
                rollingBallEmitter.transform.rotation = Quaternion.Euler(new Vector3 (0, 45 * Random.Range (0, 10), 0));
                rollingBallEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenRollingBalls));
            }
            if (isJumping) {
                MoveToPos();
            }
        }
        if (canSpawnAcrobats) {
            acrobatEmitter.transform.position = new Vector3 (Random.insideUnitCircle.x * 19, 0, Random.insideUnitCircle.y * 19);
            acrobatEmitter.SetActive(true);
            canSpawnAcrobats = false;
            StartCoroutine(WaitToSpawnAcrobats());
        }
	}

    IEnumerator WaitToSpawn (float delay) {
        yield return new WaitForSeconds(delay);
        canSpawn = true;
    }

    IEnumerator WaitToSpawnAcrobats () {
        yield return new WaitForSeconds(timeBetweenAcrobats);
        canSpawnAcrobats = true;
    }

    IEnumerator WaitToJump () {
        yield return new WaitForSeconds(timeBetweenJumps);
        canSpawn = false;
        if (attackState == AttackState.FlamingHoop)
        {
            movePos = jumpPoints[Random.Range(0, jumpPoints.Length - 1)];
        }
        rb.AddForce(Vector3.up * jumpForce * 100 * Time.deltaTime, ForceMode.Impulse);
        yield return new WaitForSeconds(2 * Time.deltaTime);
        isJumping = true;
    }

    void MoveToPos () {
        moveByForce.dir = (movePos.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, movePos.rotation, 0.05f);
        if (Vector3.Distance(movePos.position, transform.position) <= 0.1f && rb.velocity.y <= 0) {
            transform.rotation = movePos.rotation;
            rb.velocity = Vector3.zero;
            moveByForce.dir = Vector3.zero;
            isJumping = false;
            canSpawn = true;
            if (attackState == AttackState.FlamingHoop)
            {
                StartCoroutine(WaitToJump());
            }
        } else if (Vector3.Distance(new Vector3 (movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f) {
            moveByForce.dir = Vector3.zero;
            transform.position = new Vector3 (movePos.position.x, transform.position.y, movePos.position.z);
            rb.velocity = new Vector3 (0, rb.velocity.y, 0);
        }
    }
}
