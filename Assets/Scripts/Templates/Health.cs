using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int m_startingHealth;
    [SerializeField] protected int m_currentHealth;

    public virtual void TakeDamage(int _damage)
    {
        m_currentHealth -= _damage;
        if(m_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }

    private void ResetHealth()
    {
        m_currentHealth = m_startingHealth;

    }

    protected void OnEnable()
    {
        ResetHealth();
    }
}
