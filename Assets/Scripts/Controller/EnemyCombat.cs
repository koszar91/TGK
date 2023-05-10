using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatBase
{
    private float deadAnimationTime = 0.4f;
    private bool isDead = false;
    private float deathTime;

    void FixedUpdate()
    {
        if (isDead && Time.fixedTime - deathTime >= deadAnimationTime)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void Hit(float damage, Vector3 hitDirection)
    {
        if(isDead) return;

        base.Hit(damage, hitDirection);

        if (currentHealth <= 0)
        {
            Debug.Log(gameObject.name + " dead!");
            deathTime = Time.fixedTime;
            isDead = true;
        }
    }
}
