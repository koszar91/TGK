using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MovementBase
{
    private Vector3 startPosition;
    private Rigidbody2D rb;
    public float speed = 6.0f;

    private GameObject player;
    public float chaseSpeedMultiplier = 1.2f;
    public float chaseRange = 3.0f;

    private Vector3 patrolPoint;
    private float lastTimePatrolPointSet;
    public float patrolSpeedMultiplier = 1.0f;
    public float patrolRange = 5.0f;
    public float patrolPointChangePeriod = 4.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");

        patrolPoint = ComputePatrolPoint();

        float patrolOffset = UnityEngine.Random.Range(0f, patrolPointChangePeriod);
        lastTimePatrolPointSet = Time.realtimeSinceStartup - patrolOffset;
    }

    void FixedUpdate()
    {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= chaseRange) Chase();
        else                                Patrol();
    }

    private void Chase()
    {
        Vector3 destinationDir = (player.transform.position - transform.position).normalized;
        Move(destinationDir * speed * chaseSpeedMultiplier);
    }

    private void Patrol()
    {
        if (Time.fixedTime - lastTimePatrolPointSet >= patrolPointChangePeriod)
        {
            lastTimePatrolPointSet = Time.fixedTime;
            patrolPoint = ComputePatrolPoint();
        }
        Vector3 destinationDir = (patrolPoint - transform.position).normalized;
        Move(destinationDir * speed * patrolSpeedMultiplier);
    }

    private Vector3 ComputePatrolPoint()
    {
        float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);
        Vector3 randomPointOnCircle = new Vector3(x, y, 0);
        return startPosition + randomPointOnCircle * patrolRange;
    }
}
