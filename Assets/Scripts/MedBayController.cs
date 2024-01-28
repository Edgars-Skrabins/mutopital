using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBayController : MonoBehaviour
{
    [SerializeField] private Transform m_exitBayTF;
    [SerializeField] private float castRadius = 1f;
    [SerializeField] private PatientStats m_occupant;
    [SerializeField] private MedBayManager m_medBayManager;
    private bool m_nearPotion = false;
    public bool m_IsMedbayOccupied = false;

    public void SetMedBayManager(MedBayManager _medBayManager)
    {
        m_medBayManager = _medBayManager;
    }

    public bool IsMedbayOccupied()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, castRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PatientStats>() && collider != GetComponent<Collider>())
            {
                m_occupant = collider.GetComponent<PatientStats>();
                m_IsMedbayOccupied = true;
                return true; // Obstacle detected
            }

            if(collider.GetComponent<Potion>() && m_occupant)
            {
                m_nearPotion = true;
                m_occupant.Heal();
            }
            else
            {
                m_nearPotion = false;
            }
        }
        m_occupant = null;
        return false; // No obstacle detected
    }
    private bool IsNearPotion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, castRadius*1.5f);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Potion>() && m_occupant)
            {
                m_nearPotion = true;
                return true; // Obstacle detected
            }
        }
        m_nearPotion = false;
        return false; // No obstacle detected
    }

    private void Update()
    {
        if(IsMedbayOccupied() && IsNearPotion())
        m_occupant.Heal();
    }
}
