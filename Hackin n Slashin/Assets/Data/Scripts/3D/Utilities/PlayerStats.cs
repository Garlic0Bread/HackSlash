using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int currentHealth;
    public int maxHealth;
    private Animator anim;

    public HealthBar healthBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        SetMaxHealth_FromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealth_FromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.SetCurrentHealth(currentHealth);
        anim.SetBool("Damage", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            anim.SetBool("Dead", true);
        }
    }
}
