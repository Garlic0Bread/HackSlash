using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class _PlayerHealth : MonoBehaviour, IDamageable
{
    public bool isDead;
    public GameManager GM;
    private Rigidbody2D rb2;
    private float maxhealth;
    public float deathForce = 10f;
    _PlayerMovement playerMovement;
    [SerializeField] private float health;
    [SerializeField] private Animator anim;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject death_ExplosionVFX;
    private void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        isDead = false;
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<_PlayerMovement>();
    }
    private void Update()
    {
        if (this.CompareTag("Player1"))
        {
            healthBar.fillAmount = health / 100f;
            if (health <= 0f)
            {
                GM.isGameOver = true;
            }
        }
        else if (this.CompareTag("Player2"))
        {
            healthBar.fillAmount = health / 100f;
            if (health <= 0f)
            {
                GM.isGameOver = true;
            }
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
        death_ExplosionVFX.SetActive(true);

        GM.isGameOver = true;
        //anim.SetTrigger("Dead");
        Destroy(gameObject, 1f);
    }

    
}
