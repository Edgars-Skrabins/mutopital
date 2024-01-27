using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedBayController : MonoBehaviour
{
    [SerializeField] private Transform m_exitBayTF;
    [SerializeField] private float castRadius = 1f;
    [SerializeField] private PatientController m_occupant;

    private void Update()
    {
        if(IsMedbayOccupied())
        {
            Invoke("UpdateTarget", 5f);
        }
    }

    public void UpdateTarget()
    {
        if(m_occupant!= null)
        {
            m_occupant.SetDestination(m_exitBayTF);
        }
        else
        {
            Debug.LogError("No Occupant To Update!");
        }
    }

    public bool IsMedbayOccupied()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, castRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<PatientController>() && collider != GetComponent<Collider>())
            {
                m_occupant = collider.GetComponent<PatientController>();
                return true; // Obstacle detected
            }
        }
        m_occupant = null;
        return false; // No obstacle detected
    }
}
