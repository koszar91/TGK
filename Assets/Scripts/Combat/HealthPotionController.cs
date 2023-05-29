using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionController : MonoBehaviour
{
    public int value = 10;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerCombat combatController = collider.gameObject.GetComponent<PlayerCombat>();
            combatController.ApplyHealthPotion(value);
            gameController.PotionConsumed(gameObject);
        }
    }
}
