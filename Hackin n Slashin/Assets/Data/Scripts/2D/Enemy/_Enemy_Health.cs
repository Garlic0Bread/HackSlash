using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 3f;
    private Animator anim;
    private _HealthBar healthBar;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; 
        healthBar = GetComponentInChildren<_HealthBar>();
        anim = GetComponent<Animator>();
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            anim.SetTrigger("Hurt");
        }

    }

    private void Die()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
