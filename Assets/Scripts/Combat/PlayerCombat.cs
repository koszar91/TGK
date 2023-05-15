using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackDamage = 2.0f;
    public float attackRange  = 3.0f;
    public float knockbackForce = 50.0f;
    public float maxHealth = 10.0f;
    public float currentHealth = 10.0f;

    private Animator animator;
    private Transform attackPoint;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackPoint = transform.Find("AttackPoint");
        currentHealth = maxHealth;
    }

    void OnFire()
    {
        Attack();
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (var collider in colliders)
        {
            GameObject other = collider.gameObject;
            if (other != gameObject && other.tag == "Enemy")
            {
                Vector3 hitDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<EnemyCombat>().Hit(attackDamage, hitDirection * knockbackForce);
            }
        }
    }

    public void Hit(float attackDamage, Vector3 knockbackForce)
    {
        animator.SetTrigger("hit");
        currentHealth -= attackDamage;
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
    }
}
