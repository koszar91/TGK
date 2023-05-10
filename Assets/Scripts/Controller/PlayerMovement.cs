using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementBase
{
    public float speed = 10.0f;
    private Vector2 movementInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move(movementInput * speed);
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
