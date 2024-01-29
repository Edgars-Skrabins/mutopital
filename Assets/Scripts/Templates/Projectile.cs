using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float m_projectileSpeed;
    [SerializeField] protected int m_projectileStrikeDamage;
    [SerializeField] private Rigidbody m_projectileRB;
    [SerializeField] private Transform m_projectileTF;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_projectileRB = GetComponent<Rigidbody>();
        m_projectileTF = GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        LaunchProjectile();
    }

    protected virtual void LaunchProjectile()
    {
        m_projectileRB.velocity = m_projectileTF.forward * m_projectileSpeed;
    }

    private void OnTriggerEnter(Collider _collider)
    {
        HandleCollision(_collider);
    }

    protected virtual void HandleCollision(Collider _collider)
    {
        if(!_collider.TryGetComponent(out PlayerStrikes playerStrikes)) return;

        playerStrikes.StrikePlayer(m_projectileStrikeDamage);
        DestroyProjectile();
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
