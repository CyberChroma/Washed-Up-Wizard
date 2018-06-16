using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EvilWitchAI : MonoBehaviour
{

    public enum AttackState
    {
        Fireball,
        SplitShot,
        Boomerang,
        ForwardBomb,
        DropAndEmit,
        MoveAndEmit,
        DropExplode,
        HomingMulti,
        SpinMulti
    }

    public float stateChangeTime;
    public Transform player;
    public Transform[] waypoints;

    public float timeBetweenFireball;
    public GameObject fireballEmitter;

    public float timeBetweenSplitShot;
    public GameObject splitShotEmitter;

    public float timeBetweenBoomerang;
    public GameObject boomerangEmitter;

    public float timeBetweenForwardBomb;
    public GameObject forwardBombEmitter;

    public float timeBetweenDropAndEmit;
    public GameObject dropAndEmitEmitter;

    public float timeBetweenMoveAndEmit;
    public GameObject moveAndEmitEmitter;

    public float timeBetweenDropExplode;
    public GameObject dropExplodeEmitter;

    public float timeBetweenHomingMulti;
    public GameObject homingMultiEmitter;

    public float timeBetweenSpinMulti;
    public GameObject spinMultiEmitter;

    private int phase = 1;
    private AttackState attackState;
    private bool canAttack = false;
    private bool targetReached = false;
    private Transform movePos;
    private MoveByForce moveByForce;
    private Vector3 moveDir;
    private Health health;
    private Rigidbody rb;
    private Vector3 dir;

    // Use this for initialization
    void Awake()
    {
        fireballEmitter.SetActive(false);
        splitShotEmitter.SetActive(false);
        boomerangEmitter.SetActive(false);
        forwardBombEmitter.SetActive(false);
        dropAndEmitEmitter.SetActive(false);
        moveAndEmitEmitter.SetActive(false);
        dropExplodeEmitter.SetActive(false);
        homingMultiEmitter.SetActive(false);
        spinMultiEmitter.SetActive(false);
        moveByForce = GetComponent<MoveByForce>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        attackState = AttackState.Fireball;
    }

    void OnEnable()
    {
        ChangeAttackState();
    }

    void OnDisable()
    {
        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene("Evil Witch Death");
        }
    }

    void ChangeAttackState()
    {
        StopAllCoroutines();
        canAttack = false;
        targetReached = false;
        movePos = waypoints[Random.Range(0, 9)];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!targetReached)
        {
            dir = (movePos.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            moveByForce.dir = dir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            if (Vector3.Distance(new Vector3(movePos.position.x, 0, movePos.position.z), new Vector3(transform.position.x, 0, transform.position.z)) <= 0.1f)
            {
                moveByForce.dir = Vector3.zero;
                targetReached = true;
                StartCoroutine(WaitToAttack(1));
                StartCoroutine(WaitToChangeAttackState(stateChangeTime));
            }
        }
        else
        {
            if (canAttack)
            {
                canAttack = false;
                if (attackState == AttackState.Fireball)
                {
                    fireballEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenFireball));
                }
                else if (attackState == AttackState.SplitShot)
                {
                    splitShotEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenSplitShot));
                }
                else if (attackState == AttackState.Boomerang)
                {
                    boomerangEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenBoomerang));
                }
                else if (attackState == AttackState.ForwardBomb)
                {
                    forwardBombEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenForwardBomb));
                }
                else if (attackState == AttackState.DropAndEmit)
                {
                    dropAndEmitEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenDropAndEmit));
                }
                else if (attackState == AttackState.MoveAndEmit)
                {
                    moveAndEmitEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenMoveAndEmit));
                }
                else if (attackState == AttackState.DropExplode)
                {
                    dropExplodeEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenDropExplode));
                }
                else if (attackState == AttackState.HomingMulti)
                {
                    homingMultiEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenHomingMulti));
                }
                else if (attackState == AttackState.SpinMulti)
                {
                    spinMultiEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenSpinMulti));
                }
            }
            dir = (player.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        }
        if (transform.position.y <= -10)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.up * 20;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        CalculatePhase();
    }

    void CalculatePhase()
    {
        if ((health.currentHealth <= health.startHealth / 3) && phase == 2)
        {
            StopAllCoroutines();
            phase = 3;
            StartCoroutine(WaitToChangeAttackState(1));
        }
        else if ((health.currentHealth <= health.startHealth / 3 * 2) && phase == 1)
        {
            phase = 2;
            StartCoroutine(WaitToChangeAttackState(1));
        }
    }

    IEnumerator WaitToChangeAttackState(float time)
    {
        yield return new WaitForSeconds(time);
        if (phase == 1)
        {
            if (attackState == AttackState.Fireball)
            {
                attackState = AttackState.SplitShot;
            }
            else if (attackState == AttackState.SplitShot)
            {
                attackState = AttackState.Boomerang;
            }
            else
            {
                attackState = AttackState.Fireball;
            }
        }
        else if (phase == 2)
        {
            if (attackState == AttackState.ForwardBomb)
            {
                attackState = AttackState.DropAndEmit;
            }
            else if (attackState == AttackState.DropAndEmit)
            {
                attackState = AttackState.MoveAndEmit;
            }
            else
            {
                attackState = AttackState.ForwardBomb;
            }
        }
        else if (phase == 3)
        {
            if (attackState == AttackState.DropExplode)
            {
                attackState = AttackState.HomingMulti;
            }
            else if (attackState == AttackState.HomingMulti)
            {
                attackState = AttackState.SpinMulti;
            }
            else
            {
                attackState = AttackState.DropExplode;
            }
        }
        ChangeAttackState();
    }

    IEnumerator WaitToAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }
}