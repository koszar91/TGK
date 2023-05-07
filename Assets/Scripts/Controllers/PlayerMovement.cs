using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 7.0f;
    Vector2 movementInput;

    public CollidableMovement collidableMovement;

    void FixedUpdate()
    {
        collidableMovement.TryMove(movementInput, movementSpeed);
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
