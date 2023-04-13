using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float collisionOffset = 0.02f;
    public ContactFilter2D movementFilter;
    
    Vector2 movementInput;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void Move()
    {
        if (movementInput == Vector2.zero) return;

        // Check for vertical and horizontal collisions
        int collisionsVerticalCount = rb.Cast(
            new Vector2(0.0f, movementInput.y), movementFilter, castCollisions,
            movementSpeed * Time.fixedDeltaTime + collisionOffset
        );
        int collisionsHorizontalCount = rb.Cast(
            new Vector2(movementInput.x, 0.0f), movementFilter, castCollisions,
            movementSpeed * Time.fixedDeltaTime + collisionOffset
        );

        // Move if possible
        var movement = new Vector2(0.0f, 0.0f);
        if (collisionsVerticalCount == 0)   movement += new Vector2(0.0f, movementInput.y);
        if (collisionsHorizontalCount == 0) movement += new Vector2(movementInput.x, 0.0f);
        
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
