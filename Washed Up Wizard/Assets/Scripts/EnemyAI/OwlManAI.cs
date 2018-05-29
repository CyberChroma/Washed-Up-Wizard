using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OwlManAI : MonoBehaviour
{

    public enum AttackState
    {
        AllWayAccelerate,
        RapidFire,
        RapidUpDown,
        SpinAllWay,
        AllWayDecelerate,
        ForwardBackSpread,
    }

    public float stateChangeTime;
    public Transform player;
    public Transform[] waypoints;

    public float timeBetweenAllWayAccelerate;
    public GameObject allWayAccelerateEmitter;

    public float timeBetweenRapidFire;
    public GameObject rapidFireEmitter;

    public float timeBetweenRapidUpDown;
    public GameObject rapidUpDownEmitter;

    public float timeBetweenSpinAllWay;
    public GameObject spinAllWayEmitter;

    public float timeBetweenAllWayDecelerate;
    public GameObject allWayDecelerateEmitter;

    public float timeBetweenForwardBackSpread;
    public GameObject forwardBackSpreadEmitter;

    public float timeBetweenZombiesPhase1;
    public float timeBetweenZombiesPhase2;
    public float timeBetweenZombiesPhase3;
    public GameObject zombieEmitter;

    private int phase = 1;
    private AttackState attackState;
    private bool canAttack = false;
    private bool targetReached = false;
    private bool canSpawnZombies = false;
    private Transform movePos;
    private MoveByForce moveByForce;
    private Vector3 moveDir;
    private Health health;
    private Rigidbody rb;
    private Vector3 dir;

    // Use this for initialization
    void Awake()
    {
        allWayAccelerateEmitter.SetActive(false);
        rapidFireEmitter.SetActive(false);
        rapidUpDownEmitter.SetActive(false);
        spinAllWayEmitter.SetActive(false);
        allWayDecelerateEmitter.SetActive(false);
        forwardBackSpreadEmitter.SetActive(false);
        moveByForce = GetComponent<MoveByForce>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody>();
        attackState = AttackState.AllWayAccelerate;
    }

    void OnEnable()
    {
        ChangeAttackState();
    }

    void OnDisable()
    {
        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene("Owl Man Death");
        }
    }

    void ChangeAttackState()
    {
        StopAllCoroutines();
        canAttack = false;
        targetReached = false;
        movePos = waypoints[Random.Range(0, 9)];
        StartCoroutine(WaitToSpawnZombies());
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
                if (attackState == AttackState.AllWayAccelerate)
                {
                    allWayAccelerateEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenAllWayAccelerate));
                }
                else if (attackState == AttackState.RapidFire)
                {
                    rapidFireEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenRapidFire));
                }
                else if (attackState == AttackState.RapidUpDown)
                {
                    rapidUpDownEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenRapidUpDown));
                }
                else if (attackState == AttackState.SpinAllWay)
                {
                    spinAllWayEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenSpinAllWay));
                }
                else if (attackState == AttackState.AllWayDecelerate)
                {
                    allWayDecelerateEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenAllWayDecelerate));
                }
                else if (attackState == AttackState.ForwardBackSpread)
                {
                    forwardBackSpreadEmitter.SetActive(true);
                    StartCoroutine(WaitToAttack(timeBetweenForwardBackSpread));
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
        if (canSpawnZombies) {
            zombieEmitter.transform.position = new Vector3(Random.Range (-15, 15), 1, Random.Range (-15, 15));
            zombieEmitter.SetActive(true);
            canSpawnZombies = false;
            StartCoroutine(WaitToSpawnZombies());
        }
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
            if (attackState == AttackState.AllWayAccelerate)
            {
                attackState = AttackState.RapidFire;
            }
            else
            {
                attackState = AttackState.AllWayAccelerate;
            }
        }
        else if (phase == 2)
        {
            if (attackState == AttackState.RapidUpDown)
            {
                attackState = AttackState.SpinAllWay;
            }
            else
            {
                attackState = AttackState.RapidUpDown;
            }
        }
        else if (phase == 3)
        {
            if (attackState == AttackState.AllWayDecelerate)
            {
                attackState = AttackState.ForwardBackSpread;
            }
            else
            {
                attackState = AttackState.AllWayDecelerate;
            }
        }
        ChangeAttackState();
    }

    IEnumerator WaitToAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAttack = true;
    }

    IEnumerator WaitToSpawnZombies () {
        if (phase == 1) {
            yield return new WaitForSeconds(timeBetweenZombiesPhase1);
        } else if (phase == 2) {
            yield return new WaitForSeconds(timeBetweenZombiesPhase2);
        } else if (phase == 3) {
            yield return new WaitForSeconds(timeBetweenZombiesPhase2);
        } 
        canSpawnZombies = true;
    }
}