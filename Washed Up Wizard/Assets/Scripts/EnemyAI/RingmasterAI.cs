using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingmasterAI : MonoBehaviour {

    public enum AttackState {
        FlamingHoop,
        RollingBall,
        HatBomb,
        Stomp,
        UnicycleCharge,
        BalloonGun
    }

    public float airSpeed;
    public Transform player;

    public float timeBetweenFlamingHoops;
    public float timeBetweenHoopJumps;
    public GameObject flamingHoopEmitter;
    public Transform[] jumpPoints;

    public float timeBetweenRollingBalls;
    public GameObject rollingBallEmitter;
    public Transform pedistalJumpPoint;
    public GameObject pedistal;
    public Animator[] curtains;

    public float timeBetweenHatBombs;
    public float hatBombFollowSpeed;
    public GameObject bombEmitter;

    public float timeBetweenAcrobats;
    public GameObject acrobatEmitter;
   
    public float timeBetweenStompJumps;
    public Transform playerOldPos;

    public float timeBetweenCharges;
    public float chargeSpeed;

    public float timeBetweenShots;
    public float gunFollowSpeed;
    public GameObject balloonGunEmitter;

    public AttackState attackState;
    private bool canSpawn = false;
    private bool isJumping = false;
    private bool canCharge = true;
    private bool charging = false;
    private bool canSpawnAcrobats = false;
    private Transform movePos;
    private Rigidbody rb;
    private MoveByForce moveByForce;
    private LaunchToTarget launchToTarget;
    private Vector3 moveDir;

	// Use this for initialization
	void Awake () {
        flamingHoopEmitter.SetActive(false);
        acrobatEmitter.SetActive(false);
        rollingBallEmitter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        moveByForce = GetComponent<MoveByForce>();
        launchToTarget = GetComponent<LaunchToTarget>();
        launchToTarget.enabled = false;
	}
	
    void OnEnable () {
        if (attackState == AttackState.FlamingHoop)
        {
            StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
            StartCoroutine(WaitToJump(timeBetweenHoopJumps));
            moveByForce.force = airSpeed;
            moveByForce.enabled = false;
        }
        else if (attackState == AttackState.RollingBall)
        {
            StartCoroutine(WaitToJump(1));
            moveByForce.force = airSpeed;
        }
        else if (attackState == AttackState.HatBomb)
        {
            StartCoroutine(WaitToSpawn(timeBetweenHatBombs));
            movePos = player;
            moveByForce.force = hatBombFollowSpeed;
        }
        else if (attackState == AttackState.Stomp)
        {
            StartCoroutine(WaitToJump(timeBetweenStompJumps));
            moveByForce.enabled = false;
        }
        else if (attackState == AttackState.UnicycleCharge)
        {
            StartCoroutine(WaitToCharge());
            moveByForce.enabled = false;
        }
        else if (attackState == AttackState.BalloonGun)
        {
            StartCoroutine(WaitToSpawn(timeBetweenHatBombs));
            movePos = player;
            moveByForce.force = gunFollowSpeed;
        }
        StartCoroutine(WaitToSpawnAcrobats());
    }

	// Update is called once per frame
    void FixedUpdate () {
        if (attackState == AttackState.FlamingHoop)
        {
            if (canSpawn && !isJumping)
            {
                flamingHoopEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenFlamingHoops));
            }
            if (isJumping)
            {
                MoveToPos();
            }
        }
        else if (attackState == AttackState.RollingBall)
        {
            if (canSpawn && !isJumping)
            {
                int curtainNum = Random.Range(0, curtains.Length);
                rollingBallEmitter.transform.rotation = Quaternion.Euler(new Vector3(0, curtainNum * 45, 0));
                curtains[curtainNum].SetTrigger("Open and Close");
                StartCoroutine(WaitToOpenCurtain(curtainNum));
                rollingBallEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenRollingBalls));
            }
            if (isJumping)
            {
                MoveToPos();
            }
        }
        else if (attackState == AttackState.HatBomb)
        {
            if (canSpawn && !isJumping)
            {
                bombEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenHatBombs));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.UnicycleCharge)
        {
            if (charging)
            {
                if (transform.position.magnitude > 19 && canCharge)
                {
                    charging = false; 
                    canCharge = false;
                    StartCoroutine(WaitToCharge());
                }
                MoveToPos();
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.1f);
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
            }
        }
        else if (attackState == AttackState.BalloonGun)
        {
            if (canSpawn && !isJumping)
            {
                balloonGunEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenShots));
            }
            MoveToPos();
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

    void MoveToPos () {
        if (attackState != AttackState.UnicycleCharge)
        {
            moveByForce.dir = (movePos.position - transform.position);
            if (Vector3.Distance(movePos.position, transform.position) <= 0.1f && rb.velocity.y <= 0)
            {
                moveByForce.dir = Vector3.zero;
                isJumping = false;
                canSpawn = true;
                if (attackState == AttackState.FlamingHoop)
                {
                    StartCoroutine(WaitToJump(timeBetweenHoopJumps));
                }
                else if (attackState == AttackState.Stomp)
                {
                    StartCoroutine(WaitToJump(timeBetweenStompJumps));
                }
            }
            else if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f)
            {
                moveByForce.dir = Vector3.zero;
                transform.position = new Vector3(movePos.position.x, transform.position.y, movePos.position.z);
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
        if (attackState == AttackState.FlamingHoop || attackState == AttackState.RollingBall)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, movePos.rotation, 0.02f);
        }
        else if (attackState == AttackState.HatBomb || attackState == AttackState.BalloonGun)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.1f);
        }
        else if (attackState == AttackState.Stomp)
        {
            if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) >= 1)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movePos.position - transform.position), 0.1f);
            }
        }
        else if (attackState == AttackState.UnicycleCharge)
        {
            rb.velocity = moveDir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), 0.1f);
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

    }

    IEnumerator WaitToJump (float delay) {
        yield return new WaitForSeconds(delay);
        canSpawn = false;
        if (attackState == AttackState.FlamingHoop)
        {
            movePos = jumpPoints[Random.Range(0, jumpPoints.Length - 1)];
            launchToTarget.target = movePos;
            if (Vector3.Distance(transform.position, movePos.position) > 1)
            {
                launchToTarget.enabled = true;
            }
            else
            {
                rb.AddForce(Vector3.up * 2000 * Time.deltaTime, ForceMode.Impulse);
            }
        }
        else if (attackState == AttackState.Stomp)
        {
            playerOldPos.position = new Vector3(player.position.x, 0, player.position.z);
            movePos = playerOldPos;
            launchToTarget.enabled = true;
        }
        else if (attackState == AttackState.RollingBall)
        {
            rb.AddForce(Vector3.up * 2000 * Time.deltaTime, ForceMode.Impulse);
            movePos = pedistalJumpPoint;
        }
        yield return new WaitForSeconds(2 * Time.deltaTime);
        isJumping = true;
    }

    void OnCollisionEnter (Collision other) {
        if (other.collider.CompareTag("Environment") && isJumping)
        {
            moveByForce.dir = Vector3.zero;
            isJumping = false;
            canSpawn = true;
            if (attackState == AttackState.FlamingHoop)
            {
                StartCoroutine(WaitToJump(timeBetweenHoopJumps));
            }
            else if (attackState == AttackState.Stomp)
            {
                StartCoroutine(WaitToJump(timeBetweenStompJumps));
            }
        }
    }

    IEnumerator WaitToCharge () {
        yield return new WaitForSeconds(timeBetweenCharges);
        moveDir = (player.position - transform.position).normalized * chargeSpeed;
        moveDir = new Vector3(moveDir.x, 0, moveDir.z);
        rb.velocity = moveDir;
        charging = true;
        yield return new WaitForSeconds(timeBetweenCharges + 3f);
        canCharge = true;
    }

    IEnumerator WaitToOpenCurtain (int curtainNum) {
        yield return new WaitForSeconds(3.4f);
        if (curtainNum + 4 >= curtains.Length) {
            curtainNum -= 4;
        } else {
            curtainNum += 4;
        }
        curtains[curtainNum].SetTrigger("Open and Close");
    }

    IEnumerator WaitToSpawnAcrobats () {
        yield return new WaitForSeconds(timeBetweenAcrobats);
        canSpawnAcrobats = true;
    }
}
