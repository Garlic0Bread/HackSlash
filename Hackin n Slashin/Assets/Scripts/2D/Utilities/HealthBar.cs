using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;

public class HealthBar : MonoBehaviour
{
    private Image image;
    private float target = 1f;
    private Color newHealthBar_Color;
    [SerializeField] private float timeToDrain = 0.25f;
    [SerializeField] private Gradient healthBar_Gradient;
    private Coroutine drainHealthBar_Coroutine;

    private void Start()
    {
        image = GetComponent<Image>();
        image.color = healthBar_Gradient.Evaluate(target);
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
    private IEnumerator DrainHealthBar()//slowly drain the health bar
    {
        float fillAmount = image.fillAmount;
        Color currentColor = image.color;
        float elapsedTime = 0f;
        while(elapsedTime < timeToDrain)
        {
            elapsedTime += Time.deltaTime;//lerp fill amount
            image.fillAmount = Mathf.Lerp(fillAmount, target, (elapsedTime / timeToDrain));

            //lerp colour change
            image.color = Color.Lerp(currentColor, newHealthBar_Color, (elapsedTime / timeToDrain));
            yield return null;
        }
    }
}
