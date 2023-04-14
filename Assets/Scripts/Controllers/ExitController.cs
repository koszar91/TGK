using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ExitController : MonoBehaviour
{
    public GameController levelGenerator;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
            levelGenerator.PlayerExit();
    }
}
