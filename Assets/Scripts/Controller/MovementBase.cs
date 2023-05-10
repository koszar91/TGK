using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBase : MonoBehaviour
{
    public float movementForce = 1.0f;

    protected void Move(Vector2 movement)
    {
        // Change rb position
        var rb = GetComponent<Rigidbody2D>();
        movement.Normalize();
        rb.AddForce(movement * movementForce * Time.fixedTime);

        // Rotate
        if (movement.x > 0) transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (movement.x < 0) transform.localRotation = Quaternion.Euler(0, 180, 0);
        else {} // leave rotation as is

        // Animator trigger
        var animator = GetComponent<Animator>();
        if (movement.magnitude > 0)  animator?.SetBool("moving", true);
        if (movement.magnitude <= 0) animator?.SetBool("moving", false);
    }
}
