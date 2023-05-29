using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MovementBase
{
    public float speed = 10.0f;
    private Vector2 movementInput;

    void FixedUpdate()
    {
        Move(movementInput * speed);
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void ApplySpeedPotion(float value)
    {
        speed *= value;
        Debug.Log("Potion consumed. Current speed: " + speed);
    }
}
