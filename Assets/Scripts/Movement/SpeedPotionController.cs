using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotionController : MonoBehaviour
{
    public float value = 1.4f;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerMovement movement = collider.gameObject.GetComponent<PlayerMovement>();
            movement.ApplySpeedPotion(value);
            gameController.PotionConsumed(gameObject);
        }
    }
}
