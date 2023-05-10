using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    private Animator animator;
    private Transform attackPoint;
    private Rigidbody2D rb;

    public float attackDamage = 2.0f;
    public float attackRange = 1.0f;
    public float knockbackForce = 1.0f;

    public float maxHealth = 10.0f;
    public float currentHealth;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackPoint = transform.Find("AttackPoint");
        currentHealth = maxHealth;
    }

    protected void Attack()
    {
        animator.SetTrigger("attack");

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
        foreach (var collider in colliders)
        {
            GameObject collidedObject = collider.gameObject;
            if (collidedObject != gameObject && collidedObject.GetComponent<CombatBase>() != null)
            {
                Vector3 hitDirection = (collidedObject.transform.position - transform.position).normalized;
                collidedObject.GetComponent<CombatBase>().Hit(attackDamage, hitDirection);
            }
        }
    }

    protected virtual void Hit(float damage, Vector3 hitDirection)
    {
        animator.SetTrigger("hit");
        currentHealth -= damage;
        Debug.Log(gameObject.name + " hit with " + damage.ToString() + ". Health left: " + currentHealth.ToString());
        rb.AddForce(hitDirection * knockbackForce, ForceMode2D.Impulse);
    }
}
