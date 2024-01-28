using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInteraction : Interactable
{
    private PatientStats m_stats;

    private void Start()
    {
        m_stats = GetComponent<PatientStats>();
    }

    public override void Interact()
    {
        m_stats.Heal();
    }

    private bool IsNearPotion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<BasePotion>())
            {
                Destroy(gameObject);
                return true; // Potion detected
            }
        }
        return false; // No Potion detected
    }

    private void Update()
    {
        if (IsNearPotion())
        {
            m_stats.Heal();
        }
    }
}
