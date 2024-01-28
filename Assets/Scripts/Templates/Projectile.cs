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

    private void Start()
    {
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        m_projectileRB.velocity = m_projectileTF.forward * m_projectileSpeed;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        HandleCollision(_collision);
    }

    private void HandleCollision(Collision _collision)
    {
        if(!_collision.collider.TryGetComponent(out PlayerStrikes playerStrikes)) return;

        playerStrikes.StrikePlayer(m_projectileStrikeDamage);
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
