using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth = 10;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    public void Hit(float damage)
    {
        health -= damage;
        Debug.Log("Enemy hit with " + damage + " damage!");
        
        if (health <= 0)
        {
            Debug.Log("Enemy dead!");
            Destroy(this.gameObject);
        }
    }
}
