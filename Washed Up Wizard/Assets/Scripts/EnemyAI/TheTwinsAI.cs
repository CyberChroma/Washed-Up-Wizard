using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheTwinsAI : MonoBehaviour {

    public enum AttackState {
        StrideStomp,
        BlockFall,
        ClapShockwave,
        SlamSpread,
        Windmill,
        SlamShockwave
    }

    public float stateChangeTime;
    public Transform player;

    public float strideSpeed;
    public float timeBetweenStomps;
    public GameObject stompEmitter;

    public float stompSpeed;
    public float timeBetweenBlocks;
    public GameObject blockEmitter;

    public float clapSpeed;
    public float timeBetweenClaps;
    public GameObject clapEmitter;

    public float timeBetweenSlams;
    public GameObject slamSpreadEmitter;

    public float windmillSpeed;
    public float timeBetweenWindmills;
    public GameObject windmillEmitter;

    public float slamMultiSpeed;
    public float timeBetweenSlamsMulti;
    public GameObject slamMultEmitter;

    public float timeBetweenToysPhase1;
    public float timeBetweenToysPhase2;
    public float timeBetweenToysPhase3;
    public GameObject toysEmitter;

    public GameObject leg;
    public GameObject arm;
    public GameObject both;

    private int phase = 1;
    private AttackState attackState;
    private bool canSpawn = false;
    private bool canSpawnToys = false;
    private Rigidbody rb;
    private MoveByForce moveByForce;
    private Vector3 moveDir;
    private Health health;

	// Use this for initialization
	void Awake () {
        stompEmitter.SetActive(false);
        blockEmitter.SetActive(false);
        slamSpreadEmitter.SetActive(false);
        windmillEmitter.SetActive(false);
        slamMultEmitter.SetActive(false);
        rb = GetComponent<Rigidbody>();
        moveByForce = GetComponent<MoveByForce>();
        moveByForce.enabled = true;
        health = GetComponent<Health>();
        attackState = AttackState.StrideStomp;
	}
	
    void OnEnable () {
        leg.transform.localPosition = Vector3.up * 0.3f;
        arm.transform.localPosition = Vector3.up * -10;
        both.transform.localPosition = Vector3.up * -10;
        leg.SetActive(true);
        arm.SetActive(false);
        both.SetActive(false);
        ChangeAttackState ();
    }

    void OnDisable () {
        if (health.currentHealth <= 0) {
            SceneManager.LoadScene("The Twins Death");
        }
    }

	// Update is called once per frame
    void FixedUpdate () {
        if (attackState == AttackState.StrideStomp)
        {
            if (canSpawn)
            {
                stompEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenStomps));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.BlockFall)
        {
            if (canSpawn)
            {
                blockEmitter.transform.position = new Vector3 (Random.Range(-14, 14), 10, Random.Range (-25, 25));
                blockEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenStomps));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.ClapShockwave)
        {
            if (canSpawn)
            {
                clapEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenClaps));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.SlamSpread)
        {
            if (canSpawn)
            {
                slamSpreadEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenSlams));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.Windmill)
        {
            if (canSpawn)
            {
                windmillEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenWindmills));
            }
            MoveToPos();
        }
        else if (attackState == AttackState.SlamShockwave)
        {
            if (canSpawn)
            {
                slamMultEmitter.SetActive(true);
                canSpawn = false;
                StartCoroutine(WaitToSpawn(timeBetweenSlamsMulti));
            }
            MoveToPos();
        }
        if (canSpawnToys) {
            toysEmitter.transform.position = new Vector3 (Random.Range (-14, 14), 0, Random.Range (-25, 25));
            toysEmitter.SetActive(true);
            canSpawnToys = false;
            StartCoroutine(WaitToSpawnToys());
        }
        CalculatePhase();
        if (transform.position.y <= -10)
        {
            rb.velocity = Vector3.zero;
            transform.position = Vector3.up * 20;
        }
    }

    void CalculatePhase () {
        if ((health.currentHealth <= health.startHealth / 3) && phase == 2) {
            StopAllCoroutines ();
            phase = 3;
            leg.transform.localPosition = Vector3.up * -10;
            arm.transform.localPosition = Vector3.up * -10;
            both.transform.localPosition = Vector3.up * 0.3f;
            leg.SetActive(false);
            arm.SetActive(false);
            both.SetActive(true);
            StartCoroutine (WaitToChangeAttackState(1));
        } else if ((health.currentHealth <= health.startHealth / 3 * 2) && phase == 1) {
            phase = 2;
            leg.transform.localPosition = Vector3.up * -10;
            arm.transform.localPosition = Vector3.up * 0.3f;
            both.transform.localPosition = Vector3.up * -10;
            leg.SetActive(false);
            arm.SetActive(true);
            both.SetActive(false);
            StartCoroutine (WaitToChangeAttackState(1));
        }
    }

    void ChangeAttackState () {
        StopAllCoroutines();
        canSpawn = false;
        canSpawnToys = false;
        moveByForce.enabled = true;
        if (attackState == AttackState.StrideStomp)
        {
            moveByForce.force = strideSpeed;
            StartCoroutine(WaitToSpawn(timeBetweenStomps));
        }
        else if (attackState == AttackState.BlockFall)
        {
            moveByForce.force = stompSpeed;
            StartCoroutine(WaitToSpawn(timeBetweenBlocks));
        }
        else if (attackState == AttackState.ClapShockwave)
        {
            moveByForce.force = clapSpeed;
            StartCoroutine(WaitToSpawn(timeBetweenClaps));
        }
        else if (attackState == AttackState.SlamSpread)
        {
            moveByForce.dir = Vector3.zero;
            moveByForce.force = 0;
            StartCoroutine(WaitToSpawn(timeBetweenSlams));
        }
        else if (attackState == AttackState.Windmill)
        {
            moveByForce.force = windmillSpeed;
            StartCoroutine(WaitToSpawn(timeBetweenWindmills));
        }
        else if (attackState == AttackState.SlamShockwave)
        {
            moveByForce.force = slamMultiSpeed;
            StartCoroutine(WaitToSpawn(timeBetweenSlamsMulti));
        }
        StartCoroutine(WaitToSpawnToys());
        StartCoroutine(WaitToChangeAttackState(stateChangeTime));
    }

    IEnumerator WaitToChangeAttackState (float time) {
        yield return new WaitForSeconds(time);
        if (phase == 1)
        {
            if (attackState == AttackState.StrideStomp)
            {
                attackState = AttackState.BlockFall;
            }
            else
            {
                attackState = AttackState.StrideStomp;
            }
        }
        else if (phase == 2)
        {
            if (attackState == AttackState.ClapShockwave)
            {
                attackState = AttackState.SlamSpread;
            }
            else
            {
                attackState = AttackState.ClapShockwave;
            }
        }
        else if (phase == 3)
        {
            if (attackState == AttackState.Windmill)
            {
                attackState = AttackState.SlamShockwave;
            }
            else
            {
                attackState = AttackState.Windmill;
            }
        }
        ChangeAttackState();
    }

    IEnumerator WaitToSpawn (float delay) {
        yield return new WaitForSeconds(delay);
        canSpawn = true;
    }

    void MoveToPos () {
        moveByForce.dir = (player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), 0.05f);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }

    IEnumerator WaitToSpawnToys () {
        if (phase == 1) {
            yield return new WaitForSeconds(timeBetweenToysPhase1);
        } else if (phase == 2) {
            yield return new WaitForSeconds(timeBetweenToysPhase2);
        } else if (phase == 3) {
            yield return new WaitForSeconds(timeBetweenToysPhase3);
        } 
        canSpawnToys = true;
    }
}
