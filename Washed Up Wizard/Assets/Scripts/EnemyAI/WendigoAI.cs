using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WendigoAI : MonoBehaviour {

    public enum AttackState {
        IceSpread,
        SnowballBowl,
        IceBreath,
        LungeClaw,
        BullCharge,
        StompShockwave
    }

    public float normalSpeed;
    public Transform player;

    public float chaseSpeed;
    public float timeBetweenIceSpread;
    public GameObject iceShotsEmitter;

    public float timeBetweenSnowballs;
    public GameObject snowBallEmitter;
    public Transform center;

    public float timeBetweenHarpiesPhase1;
    public float timeBetweenHarpiesPhase2;
    public float timeBetweenHarpiesPhase3;
    public GameObject[] harpiesEmitters;

    private int phase = 1;
    private AttackState attackState;
    private bool canSpawn = false;
    private bool centerReached = false;
    private bool canSpawnHarpies = false;
    private Transform movePos;
    private MoveByForce moveByForce;
    private Vector3 moveDir;
    private Health health;

    // Use this for initialization
    void Awake () {
        iceShotsEmitter.SetActive(false);
        moveByForce = GetComponent<MoveByForce>();
        moveByForce.enabled = true;
        health = GetComponent<Health>();
        attackState = AttackState.SnowballBowl;
    }

    void OnEnable () {
        if (attackState == AttackState.IceSpread)
        {
            moveByForce.force = chaseSpeed;
            movePos = player;
            StartCoroutine(WaitToSpawn(timeBetweenIceSpread));
        }
        else if (attackState == AttackState.SnowballBowl)
        {
            moveByForce.force = normalSpeed;
            movePos = center;
            centerReached = false;
        }
    }

    void OnDisable () {
        if (health.currentHealth <= 0)
        {
            SceneManager.LoadScene("Wendigo Death");
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (attackState == AttackState.IceSpread)
        {
            if (canSpawn)
            {
                iceShotsEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenIceSpread));
            }
            Vector3 dir = (movePos.position - transform.position);
            dir = new Vector3(dir.x, 0, dir.z).normalized;
            moveByForce.dir = dir;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
        } else if (attackState == AttackState.SnowballBowl) {
            if (!centerReached)
            {
                Vector3 dir = (movePos.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                moveByForce.dir = dir;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
                if (Vector3.Distance(new Vector3 (movePos.position.x, 0, movePos.position.z), new Vector3 (transform.position.x, 0, transform.position.z)) <= 0.1f)
                {
                    moveByForce.dir = Vector3.zero;
                    centerReached = true;
                    StartCoroutine(WaitToSpawn(timeBetweenSnowballs));
                }
            }
            else if (canSpawn)
            {
                snowBallEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenSnowballs));
            }
            else
            {
                Vector3 dir = (player.position - transform.position);
                dir = new Vector3(dir.x, 0, dir.z).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);
            }
        }
        if (canSpawnHarpies) {
            harpiesEmitters [Random.Range(0, harpiesEmitters.Length)].SetActive(true);
            canSpawnHarpies = false;
            StartCoroutine(WaitToSpawnHarpies());
        }
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    IEnumerator WaitToSpawn (float delay) {
        yield return new WaitForSeconds(delay);
        canSpawn = true;
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