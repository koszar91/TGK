using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected const float MOVEMENT_FORCE_MULTIPLIER = 1000.0f;

    protected void Move(Vector2 movement)
    {
        GetComponent<Rigidbody2D>().AddForce(movement * MOVEMENT_FORCE_MULTIPLIER * Time.fixedDeltaTime);
        GetComponent<Animator>()?.SetBool("moving", movement.magnitude > 0);
        if (movement.x > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (movement.x < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
    }
}
