using UnityEngine;
using UnityEngine.UI;

public abstract class PatientHealth : MonoBehaviour
{
    [SerializeField] private float m_healthValue = 50f;
    [SerializeField] private Slider m_healthSlider;

    protected virtual void UpdateHealth()
    {
        m_healthSlider.value = m_healthValue;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
