using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientInteraction : MonoBehaviour
{
    private PatientStats m_stats;
    private Potion m_potion;

    private void Start()
    {
        m_stats = GetComponent<PatientStats>();
    }

    private bool IsNearPotion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Potion>(out m_potion))
            {
                if(m_potion)
                    return true; // Potion detected
            }
        }
        m_potion = null;
        return false; // No Potion detected
    }

    private void Update()
    {
        if(IsNearPotion() && !m_potion.IsPicked())
        {
            m_stats.GetMutation().AttemptRemoveMutation(m_potion.GetPotionName());
            m_stats.Heal();
            Destroy(m_potion.gameObject);
        }
    }
}
