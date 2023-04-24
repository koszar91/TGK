using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator animator;
    public Transform attackPoint;
    public float attackRange = 1.2f;
    public float damage = 3.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnFire()
    {
        animator.SetTrigger("attack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyCombat>().Hit(damage);
            }
        }
    }
}
