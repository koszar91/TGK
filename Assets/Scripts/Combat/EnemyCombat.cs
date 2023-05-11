using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float currentHealth;

    private bool isDead = false;
    private float deadAnimationTime = 0.4f;
    private float deathTime;

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

    void FixedUpdate()
    {
        if (isDead && Time.fixedTime - deathTime >= deadAnimationTime)
        {
            Destroy(this.gameObject);
        }
    }

    public void Hit(float attackDamage, Vector3 knockbackForce)
    {
        if (isDead) return;

        animator.SetTrigger("hit");
        currentHealth -= attackDamage;
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            deathTime = Time.fixedTime;
            isDead = true;
        }
    }
}
