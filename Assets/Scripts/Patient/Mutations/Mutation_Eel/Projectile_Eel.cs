using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Eel : Projectile
{
    [SerializeField] private float m_projectileDestroyCooldown;

    protected override void Start()
    {
        Invoke(nameof(DestroyProjectile),m_projectileDestroyCooldown);
    }

    protected override void LaunchProjectile()
    {

    }

    protected override void HandleCollision(Collider _collider)
    {
        if(!_collider.TryGetComponent(out PlayerStrikes playerStrikes)) return;

        playerStrikes.StrikePlayer(m_projectileStrikeDamage);
    }

}
