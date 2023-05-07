using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableMovement : MonoBehaviour
{
    public float collisionOffset = 0.0f;
    
    SpriteRenderer sprite;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Can be null. If so, it's not used
    }

    public void TryMove(Vector2 movement, float speed)
    {
        if (movement == Vector2.zero)
        {
            animator?.SetBool("moving", false);
            return;
        }

        animator?.SetBool("moving", true);

        // Check for vertical and horizontal collisions
        var castCollisions = new List<RaycastHit2D>();
        var movementFilter = new ContactFilter2D();
        int collisionsVerticalCount = rb.Cast(
            new Vector2(0.0f, movement.y), movementFilter, castCollisions,
            speed * Time.fixedDeltaTime + collisionOffset
        );
        int collisionsHorizontalCount = rb.Cast(
            new Vector2(movement.x, 0.0f), movementFilter, castCollisions,
            speed * Time.fixedDeltaTime + collisionOffset
        );

        // Move if possible
        var finalMovement = new Vector2(0.0f, 0.0f);
        if (collisionsVerticalCount == 0)   finalMovement += new Vector2(0.0f, movement.y);
        if (collisionsHorizontalCount == 0) finalMovement += new Vector2(movement.x, 0.0f);

        rb.MovePosition(rb.position + finalMovement * speed * Time.fixedDeltaTime);

        // Rotate according to moving direction
        if (finalMovement.x > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (finalMovement.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else {} // leave rotation as is
    }
}
