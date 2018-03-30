using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WendigoAI : MonoBehaviour {

    public enum AttackState {
        SnowballBowl,
        IceSpread,
        IceBreath,
        LungeClaw,
        BullCharge,
        StompShockwave
    }

    public float stateChangeTime;
    public float normalSpeed;
    public Transform player;
    public Transform center;

    public float chaseSpeed;
    public float timeBetweenIceSpread;
    public GameObject iceShotsEmitter;

    public float timeBetweenSnowballs;
    public GameObject snowBallEmitter;

    public float iceBreathTurnSpeed;
    public GameObject iceBreathEmitter;

    public float lungeForce;
    public float timeBetweenLunges;

    public float chargeSpeed;
    public float chargeTurnSpeed;
    public float chargeTurnDistance;

    public float timeBetweenShockwaves;
    public GameObject shockwaveEmitter;
    public float timeBetweenHarpiesPhase1;
    public float timeBetweenHarpiesPhase2;
    public float timeBetweenHarpiesPhase3;
    public GameObject harpyEmitter;

    private int phase = 1;
    private AttackState attackState;
    private bool canAttack = false;
    private bool centerReached = false;
    private bool canSpawnHarpies = false;
    private Transform movePos;
    private MoveByForce moveByForce;
    private Vector3 moveDir;
    private Health health;
    private Rigidbody rb;
    private Vector3 dir;

    // Use this for initialization
    void Awake () {
        iceShotsEmitter.SetActive(false);
        snowBallEmitter.SetActive(false);
        iceBreathEmitter.SetActive(false);
        shockwaveEmitter.SetActive(false);
        moveByForce = GetComponent<MoveByForce>();
        moveByForce.enabled = true;
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        attackState = AttackState.SnowballBowl;
    }

    void OnEnable () {
        ChangeAttackState();
    }

    void OnDisable () {
        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene("Wendigo Death");
        }
    }

    void ChangeAttackState () {
        StopAllCoroutines();
        canAttack = false;
        centerReached = false;
        canSpawnHarpies = false;
        moveByForce.enabled = true;
        if (attackState == AttackState.IceSpread)
        {
            moveByForce.force = chaseSpeed;
            movePos = player;
            StartCoroutine(WaitToAttack(timeBetweenIceSpread));
        }
        else if (attackState == AttackState.SnowballBowl || attackState == AttackState.IceBreath || attackState == AttackState.StompShockwave)
        {
            moveByForce.force = normalSpeed;
            movePos = center;
            centerReached = false;
        }
        else if (attackState == AttackState.LungeClaw)
        {
            moveByForce.enabled = false;
            dir = (player.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            StartCoroutine(WaitToAttack(timeBetweenLunges));
        }
        else if (attackState == AttackState.BullCharge)
        {
            moveByForce.force = chargeSpeed;
            dir = (player.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
        }
        StartCoroutine(WaitToSpawnHarpies());
        StartCoroutine(WaitToChangeAttackState(stateChangeTime));
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (attackState == AttackState.IceSpread)
        {
            if (canAttack)
            {
                iceShotsEmitter.SetActive(true);
                canAttack = false;
                StartCoroutine(WaitToAttack(timeBetweenIceSpread));
            }
            dir = (movePos.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            moveByForce.dir = dir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
        else if (attackState == AttackState.SnowballBowl)
        {
            if (!centerReached)
            {
                dir = (movePos.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                moveByForce.dir = dir;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
                if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f)
                {
                    moveByForce.dir = Vector3.zero;
                    centerReached = true;
                    StartCoroutine(WaitToAttack(timeBetweenSnowballs));
                }
            }
            else if (canAttack)
            {
                snowBallEmitter.SetActive(true);
                canAttack = false;
                StartCoroutine(WaitToAttack(timeBetweenSnowballs));
            }
            else
            {
                dir = (player.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            }
        }
        else if (attackState == AttackState.IceBreath)
        {
            if (centerReached)
            {
                dir = (player.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), iceBreathTurnSpeed);
            }
            else
            {
                dir = (movePos.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                moveByForce.dir = dir;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
                if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f)
                {
                    moveByForce.dir = Vector3.zero;
                    centerReached = true;
                    iceBreathEmitter.SetActive(true);
                }
            }
        }
        else if (attackState == AttackState.LungeClaw)
        {
            if (canAttack)
            {
                dir = (player.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                rb.AddForce(dir * lungeForce);
                canAttack = false;
                StartCoroutine(WaitToAttack(timeBetweenLunges));
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
        else if (attackState == AttackState.BullCharge)
        {
            if (Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), new Vector3(transform.position.x, 0, transform.position.z)) > chargeTurnDistance)
            {
                dir = Vector3.Slerp(dir, (player.position - transform.position), chargeTurnSpeed);
            }
            else
            {
                dir = Vector3.Slerp(dir, (player.position - transform.position), 0.02f);
            }
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            moveByForce.dir = dir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
        else if (attackState == AttackState.StompShockwave)
        {
            if (!centerReached)
            {
                dir = (movePos.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                moveByForce.dir = dir;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
                if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f)
                {
                    moveByForce.dir = Vector3.zero;
                    centerReached = true;
                    StartCoroutine(WaitToAttack(timeBetweenShockwaves));
                }
            }
            else if (canAttack)
            {
                shockwaveEmitter.SetActive(true);
                canAttack = false;
                StartCoroutine(WaitToAttack(timeBetweenShockwaves));
            }
            else
            {
                dir = (player.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            }
            if (transform.position.y <= -10)
            {
                rb.velocity = Vector3.zero;
                transform.position = Vector3.up * 20;
            }
        }

        if (canSpawnHarpies) {
            harpyEmitter.transform.position = new Vector3(Random.Range (-19, 19), 1, Random.Range (-19, 19));
            harpyEmitter.SetActive(true);
            canSpawnHarpies = false;
            StartCoroutine(WaitToSpawnHarpies());
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        CalculatePhase ();
    }

    void CalculatePhase () {
        if ((health.currentHealth <= health.startHealth / 3) && phase == 2) {
            StopAllCoroutines ();
            phase = 3;
            StartCoroutine (WaitToChangeAttackState(1));
        } else if ((health.currentHealth <= health.startHealth / 3 * 2) && phase == 1) {
            phase = 2;
            StartCoroutine (WaitToChangeAttackState(1));
        }
    }

    IEnumerator WaitToChangeAttackState (float time) {
        iceBreathEmitter.SetActive(false);
        yield return new WaitForSeconds(time);
        if (phase == 1)
        {
            if (attackState == AttackState.SnowballBowl)
            {
                attackState = AttackState.IceSpread;
            }
            else
            {
                attackState = AttackState.SnowballBowl;
            }
        }
        else if (phase == 2)
        {
            if (attackState == AttackState.IceBreath)
            {
                attackState = AttackState.LungeClaw;
            }
            else
            {
                attackState = AttackState.IceBreath;
            }
        }
        else if (phase == 3)
        {
            if (attackState == AttackState.BullCharge)
            {
                attackState = AttackState.StompShockwave;
            }
            else
            {
                attackState = AttackState.BullCharge;
            }
        }
        ChangeAttackState();
    }

    IEnumerator WaitToAttack (float delay) {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    IEnumerator WaitToSpawnHarpies () {
        if (phase == 1) {
            yield return new WaitForSeconds(timeBetweenHarpiesPhase1);
        } else if (phase == 2) {
            yield return new WaitForSeconds(timeBetweenHarpiesPhase2);
        } else if (phase == 3) {
            yield return new WaitForSeconds(timeBetweenHarpiesPhase2);
        } 
        canSpawnHarpies = true;
    }
}