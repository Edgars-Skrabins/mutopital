using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientStats : PatientHealth
{
    [SerializeField] private float m_patienceLevel = 0f;
    [SerializeField] private MutationController m_activeMutation;

    public MutationController GetMutation()
    {
        return m_activeMutation;
    }

    private void Awake()
    {
        SetRandomPaitenceLevel();
        m_activeMutation = GetComponentInChildren<MutationController>();
    }

    private void LateUpdate()
    {
        UpdateHealth();
    }

    public void SetRandomPaitenceLevel()
    {
        m_patienceLevel = Random.Range(30f, 100f);
    }

    public float GetPatienceLevel()
    {
        return m_patienceLevel;
    }

    public void SetPatienceLevel(float _value)
    {
        m_patienceLevel = _value;
    }

    public void Heal()
    {
        MaxHealth();
    }

    public bool IsHealed()
    {
        if (GetHealth() >= 100f)
        {
            return true;
        }
        return false;
    }
}
