using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int healthLvl = 10;
    public int currentHealth;
    public int maxHealth;

    Animator anim;
    Collider collision;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        collision = GetComponent<Collider>();
    }
    void Start()
    {
        maxHealth = SetMaxHealth_FromHealthLevel();
        currentHealth = maxHealth;
        collision.enabled = true;
    }

    private int SetMaxHealth_FromHealthLevel()
    {
        maxHealth = healthLvl * 10;
        return maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        anim.Play("Damaged");

        if (currentHealth <= 0)
        {
            collision.enabled = false;
            currentHealth = 0;
            anim.Play("Death");
        }
    }
}
