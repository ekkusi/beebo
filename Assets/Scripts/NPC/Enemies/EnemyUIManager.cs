using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    public Slider healthBarSlider;
    public Vector3 healthBarOffset;
    public StatsManager enemyStatsManager;
    public Color lowHealthColor;
    public Color highHealthColor;

    void Update()
    {
        Debug.Log(healthBarSlider);
        Debug.Log(transform.parent);
        Debug.Log(healthBarOffset);
        healthBarSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
    }

    public void UpdateHealthBar()
    {
        float maxHealth = enemyStatsManager.health.value;
        float currentHealth = enemyStatsManager.currentHealth;
        // healthBarSlider.gameObject.SetActive(currentHealth < maxHealth);
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        Debug.Log("Updating health to value: " + currentHealth);
        Debug.Log("And max health: " + maxHealth);

        Image fillRectImage = healthBarSlider.fillRect.GetComponentInChildren<Image>();
        fillRectImage.color = Color.Lerp(lowHealthColor, highHealthColor, healthBarSlider.normalizedValue);
    }
}
