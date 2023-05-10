using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    protected const float MOVEMENT_FORCE_MULTIPLIER = 1000.0f;

    protected void Move(Vector2 movement)
    {
        // Add velocity to rigidbody
        var rb = GetComponent<Rigidbody2D>();
        rb.AddForce(movement * MOVEMENT_FORCE_MULTIPLIER * Time.fixedDeltaTime, ForceMode2D.Force);

        // Rotation of the entire game object
        if (movement.x > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (movement.x < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
        else {} // leave rotation as is

        // Animator parameters update
        var animator = GetComponent<Animator>();
        if (movement.magnitude > 0)  animator?.SetBool("moving", true);
        if (movement.magnitude <= 0) animator?.SetBool("moving", false);
    }
}
