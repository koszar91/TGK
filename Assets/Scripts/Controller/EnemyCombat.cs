using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CombatBase
{
    private float deadAnimationTime = 0.4f;
    private bool isDead = false;
    private float deadlyHitTime = -1;

    void FixedUpdate()
    {
        if (deadlyHitTime > 0 && Time.fixedTime - deadlyHitTime >= deadAnimationTime)
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
            deadlyHitTime = Time.fixedTime;
            isDead = true;
        }
    }
}
