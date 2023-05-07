using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    Vector3 startPosition;
    GameObject player;
    public CollidableMovement collidableMovement;

    Vector3 patrolPoint;
    float lastTimePatrolPointSet;
    public float patrolMovementSpeed = 3.0f;
    public float patrolRange = 5.0f;
    public float patrolPointChangePeriod = 4.0f;

    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindWithTag("Player");
        patrolPoint = ComputePatrolPoint();
    }

    void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        if (Time.fixedTime - lastTimePatrolPointSet >= patrolPointChangePeriod)
        {
            lastTimePatrolPointSet = Time.fixedTime;
            patrolPoint = ComputePatrolPoint();
            Debug.Log("Patrol point changed to: " + patrolPoint);
        }
        Vector3 destinationDir = patrolPoint - transform.position;
        if (destinationDir.magnitude < 0.05) destinationDir = Vector3.zero;
        else destinationDir.Normalize();
        collidableMovement.TryMove(destinationDir, patrolMovementSpeed);
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
