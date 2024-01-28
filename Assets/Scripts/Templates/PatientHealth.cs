using UnityEngine;
using UnityEngine.UI;

public abstract class PatientHealth : MonoBehaviour
{
    [SerializeField] private float m_healthValue = 0.01f;
    [SerializeField] private Slider m_healthSlider;

    protected virtual void UpdateHealth()
    {
        m_healthSlider.value = m_healthValue;
    }

    protected virtual void MaxHealth()
    {
        m_healthValue = 100f;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual float GetHealth()
    {
        return m_healthValue;
    }
}
