using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy_Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 3f;
    private Animator anim;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; 
        anim = GetComponent<Animator>();
    }

    public void Damage(float damageAmount)
    {
        anim.SetTrigger("Hurt");
        currentHealth -= damageAmount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetTrigger("Death");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
