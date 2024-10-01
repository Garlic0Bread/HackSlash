using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class _PlayerHealth : MonoBehaviour
{
    private float maxhealth;
    [SerializeField] private float health;
    [SerializeField] private Animator anim;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (this.CompareTag("Player"))
        {
            healthBar.fillAmount = health / 100f;
        }
    }

    private IEnumerator visualIndicator(Color color)
    {
        GetComponentInChildren<SpriteRenderer>().color = color;
        yield return new WaitForSeconds(0.15f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
    public void Damage(float amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }
        this.health -= amount;
        StartCoroutine(visualIndicator(Color.red));

        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Healing");
        }

        bool wouldBeOverMaHealth = health + amount > maxhealth;
        StartCoroutine(visualIndicator(Color.green));
        if (wouldBeOverMaHealth)
        {
            this.health = maxhealth;
        }
        else
        {
            this.health += amount;
        }
    }
    private void Die()
    {
        anim.SetTrigger("Dead");
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }

}
