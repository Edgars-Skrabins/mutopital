using UnityEngine;

public abstract class Mutation : MonoBehaviour
{
    [SerializeField] protected GameObject m_mutationGFX;
    [SerializeField] protected Transform m_effectSpawnRotation;
    [SerializeField] protected Transform m_effectSpawnLocation;
    [SerializeField] protected GameObject m_effectPrefab;
    [SerializeField] protected float m_effectFrequency;
    private float m_effectTimer;

    protected virtual void Update()
    {
        CountEffectTimer();
        HandleEffect();
    }

    private void CountEffectTimer()
    {
        m_effectTimer += Time.deltaTime;
    }

    private void ResetTimer()
    {
        m_effectTimer = 0;
    }

    private void HandleEffect()
    {
        bool notOnCoolDown = m_effectTimer >= m_effectFrequency;
        if(notOnCoolDown)
        {
            ResetTimer();
            FireEffect();
        }
    }

    public virtual void FireEffect()
    {
        Instantiate(m_effectPrefab, m_effectSpawnLocation.position, m_effectSpawnRotation.rotation);
    }
}
