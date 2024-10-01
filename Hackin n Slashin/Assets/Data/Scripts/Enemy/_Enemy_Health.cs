using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class _Enemy_Health : MonoBehaviour, IDamageable
{

    [SerializeField] private float timeToDrain = 0.25f;
    [SerializeField] private float enemy_ExpWorth;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private int enemyDeadPoint;
    private float currentHealth;
    private float target = 1f;

    private Animator anim;
    private Color newHealthBar_Color;
    [SerializeField] private Image healthImage;
    private Coroutine drainHealthBar_Coroutine;
    [SerializeField] private Gradient healthBar_Gradient;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healthImage.color = healthBar_Gradient.Evaluate(target);
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthBar(maxHealth, currentHealth);
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
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length + 1);
        GameCurrency updateCurrencies = FindObjectOfType<GameCurrency>();
        updateCurrencies.EnemiesLeft(enemyDeadPoint);
        updateCurrencies.IncreaseExp(enemy_ExpWorth);
    }

    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
        drainHealthBar_Coroutine = StartCoroutine(DrainHealthBar());
        CheckHealthBra_GradientAmount();
    }
    private void CheckHealthBra_GradientAmount()
    {
        newHealthBar_Color = healthBar_Gradient.Evaluate(target);

    }
    private IEnumerator DrainHealthBar()
    {//slowly drain the health bar
        float fillAmount = healthImage.fillAmount;
        Color currentColor = healthImage.color;
        float elapsedTime = 0f;
        while (elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;//lerp fill amount
            healthImage.fillAmount = Mathf.Lerp(fillAmount, target, (elapsedTime / timeToDrain));

            //lerp colour change
            healthImage.color = Color.Lerp(currentColor, newHealthBar_Color, (elapsedTime / timeToDrain));
            yield return null;
        }
    }
}
