using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private float currentHealth;


    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public event Action<float> OnHealthchanged = delegate { };

    public event Action OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;

    }

    public void ChangeHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0.0f, maxHealth);

        float currentHealthpercentage = currentHealth / maxHealth;
        if (OnHealthchanged != null)
            OnHealthchanged(currentHealthpercentage);

        if (currentHealth <= 0.0f && OnDeath != null)
            OnDeath();
    }

    public void Applydamage(float amount)
    {
        ChangeHealth(-amount);
    }
}
