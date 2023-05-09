using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControllerBase : MonoBehaviour
{
    protected void Move(Vector2 movement)
    {
        // Change rb position
        var rb = GetComponent<Rigidbody2D>();
        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

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
