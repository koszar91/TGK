using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float maxHealth = 10.0f;
    public float currentHealth;

    public bool isDead = false;
    private float deadAnimationTime = 0.4f;
    private float deathTime;

    private GameObject player;
    private float lastTimeAttacked;
    public float knockbackForce = 25.0f;
    public float attackDamage = 1.0f;
    public float attackCooldown = 2.5f;
    public float attackRange = 1.5f;

    private Animator animator;
    private Transform attackPoint;
    private Rigidbody2D rb;
    private GameController gameController;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackPoint = transform.Find("AttackPoint");
        currentHealth = maxHealth;
        player = GameObject.FindWithTag("Player");
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void FixedUpdate()
    {
        if (isDead && Time.fixedTime - deathTime >= deadAnimationTime)
        {
            gameController.EnemyDead(this.gameObject);
        }

        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer <= attackRange && Time.fixedTime - lastTimeAttacked >= attackCooldown)
        {
            animator.SetTrigger("attack");
            lastTimeAttacked = Time.fixedTime;
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

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (var collider in colliders)
        {
            GameObject other = collider.gameObject;
            if (other != gameObject && other.tag == "Player")
            {
                Vector3 hitDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<PlayerCombat>().Hit(attackDamage, hitDirection * knockbackForce);
            }
        }
    }
}
