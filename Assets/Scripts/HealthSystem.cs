using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public float health { get; private set; }
    public float maxHealth { get; private set; }
    public event EventHandler OnDamage;
    public event EventHandler OnDead;
    private void Awake()
    {
        health = maxHealth;
    }
    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        OnDamage?.Invoke(this, EventArgs.Empty);
        if(IsDead())
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsDead()
    {
        return health <= 0;
    }
    public float HealthNormalize()
    {
        return (float) health / maxHealth;
    }
    public void SetMaxHealth(float maxHealth, bool updateMaxHeat)
    {
        this.maxHealth = maxHealth;
        if(updateMaxHeat)
        {
            health = maxHealth;
        }
    }
}
