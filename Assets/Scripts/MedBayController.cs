using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBayController : MonoBehaviour
{
    [SerializeField] private Transform m_exitBayTF;
    [SerializeField] private float castRadius = 1f;
    [SerializeField] private PatientStats m_occupant;
    [SerializeField] private MedBayManager m_medBayManager;

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
                m_occupant.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                m_IsMedbayOccupied = true;
                m_occupant.GetMutation().EnableMutationGFX();

                return true; // Obstacle detected
            }
        }

        m_IsMedbayOccupied = false;
        m_occupant = null;
        return false; // No obstacle detected
    }

    private void Update()
    {
        IsMedbayOccupied();

        FixOverlapingPatients();
    }

    private void FixOverlapingPatients()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, castRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PatientStats>() && collider != GetComponent<Collider>() && !collider.GetComponent<PatientStats>().IsHealed())
            {
                if (m_occupant != collider.GetComponent<PatientStats>())
                {
                   m_occupant.GetComponent<PatientController>().FindNewFreeMedBay();
                }
            }
        }
    }
}
