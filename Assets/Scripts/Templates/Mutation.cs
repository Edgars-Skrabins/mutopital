using UnityEngine;

public abstract class Mutation : MonoBehaviour
{
    [SerializeField] protected GameObject m_mutationGFX;
    private bool m_canShootEffects;
    [SerializeField] protected GameObject m_effectWarningGFX;
    [SerializeField] private float m_warningDuration = 1;
    [SerializeField] protected Transform m_effectSpawnRotation;
    [SerializeField] protected Transform m_effectSpawnLocation;
    [SerializeField] protected GameObject m_effectPrefab;
    [SerializeField] protected float m_effectFrequency;
    [SerializeField] protected string m_healPotionName;
    private float m_effectTimer;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        MutationGFXSetActive(false);
    }

    private void Initialize()
    {
        MutationGFXSetActive(true);
    }

    private void MutationGFXSetActive(bool _isOn)
    {
        m_mutationGFX.SetActive(_isOn);
    }

    protected virtual void Update()
    {
        if(!m_canShootEffects)
        {
            return;
        }

        CountEffectTimer();
        HandleEffect();
    }

    public void EnableEffects()
    {
        m_canShootEffects = true;
    }

    private void CountEffectTimer()
    {
        m_effectTimer += Time.deltaTime;
        if(m_effectTimer >= m_effectFrequency - m_warningDuration)
        {
            ShowWarning();
        }

        HideWarning();
    }

    private void ShowWarning()
    {
        m_effectWarningGFX.SetActive(true);
    }

    private void HideWarning()
    {
        m_effectWarningGFX.SetActive(false);
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

    public string GetMutationHealPotionName()
    {
        return m_healPotionName;
    }
}
