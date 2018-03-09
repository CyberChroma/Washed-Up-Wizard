using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedAI : MonoBehaviour {

    public float meleeRadius = 5;

    private MeleeEnemyAI meleeEnemyAI;
    private RangedEnemyAI rangedEnemyAI;
    private MoveByConstantSpeed constantSpeed;
    private Transform player;

    void Awake () {
        player = GameObject.Find("Player").transform;
        rangedEnemyAI = GetComponent<RangedEnemyAI>();
        meleeEnemyAI = GetComponent<MeleeEnemyAI>();
        constantSpeed = GetComponent<MoveByConstantSpeed>();
        rangedEnemyAI.enabled = false;
        meleeEnemyAI.enabled = false;
    }
       

    void FixedUpdate () {
        if (player.position.x > transform.position.x - meleeRadius && player.position.x < transform.position.x + meleeRadius && player.position.z > transform.position.z - meleeRadius && player.position.z < transform.position.z + meleeRadius)
        {
            meleeEnemyAI.enabled = true;
            constantSpeed = true;
            rangedEnemyAI.enabled = false;
        }
        else{
            meleeEnemyAI.enabled = false;
            constantSpeed.enabled = false;
            rangedEnemyAI.enabled = true;
        }
    }
}
