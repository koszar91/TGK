using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    public float collisionOffset = 0.00005f;
    
    Vector2 movementInput;
    SpriteRenderer sprite;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("attack");
    }

    private void Move()
    {
        if (movementInput == Vector2.zero)
        {
            animator.SetBool("moving", false);
            return;
        }
        animator.SetBool("moving", true);

        // Check for vertical and horizontal collisions
        var castCollisions = new List<RaycastHit2D>();
        var movementFilter = new ContactFilter2D();
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

        // Flip sprite according to moving direction
        if (movement.x > 0) sprite.flipX = false;
        if (movement.x < 0) sprite.flipX = true;
        else {} // leave sprite as is
    }
}
