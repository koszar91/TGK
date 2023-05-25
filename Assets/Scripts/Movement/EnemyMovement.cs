using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MovementBase
{
    private Vector3 startPosition;
    public float speed = 6.0f;

    private GameObject player = null;
    public float chaseSpeedMultiplier = 1.2f;
    public float chaseStateRange = 4.0f;

    private Vector3 patrolPoint;
    private float lastTimePatrolPointSet;
    public float patrolSpeedMultiplier = 1.0f;
    public float patrolPointChangePeriod = 4.0f;
    public float patrolPointRange = 5.0f;

    public float attackStateRange = 1.5f;

    public void SetPlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    void Start()
    {
        startPosition = transform.position;
        // player = GameObject.FindWithTag("Player");

        patrolPoint = ComputePatrolPoint();

        float patrolOffset = UnityEngine.Random.Range(0f, patrolPointChangePeriod);
        lastTimePatrolPointSet = Time.realtimeSinceStartup - patrolOffset;
    }

    void FixedUpdate()
    {
        if (player == null)
            return;
        
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if      (distanceToPlayer <= attackStateRange) AttackState();
        else if (distanceToPlayer <= chaseStateRange)  ChaseState();
        else                                           PatrolState();
    }

    private void AttackState()
    {
        Move(Vector2.zero);
    }

    private void ChaseState()
    {
        Vector3 destinationDir = (player.transform.position - transform.position).normalized;
        Move(destinationDir * speed * chaseSpeedMultiplier);
    }

    private void PatrolState()
    {
        if (Time.fixedTime - lastTimePatrolPointSet >= patrolPointChangePeriod)
        {
            lastTimePatrolPointSet = Time.fixedTime;
            patrolPoint = ComputePatrolPoint();
        }
        Vector3 destination = patrolPoint - transform.position;
        if (destination.magnitude < 0.05) destination = Vector3.zero;
        else destination = destination.normalized;
        
        Move(destination * speed * patrolSpeedMultiplier);
    }

    private Vector3 ComputePatrolPoint()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector3 randomPointOnCircle = new Vector3(x, y, 0);
        return startPosition + randomPointOnCircle * patrolPointRange;
    }
}
