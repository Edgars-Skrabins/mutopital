using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatientController : MonoBehaviour
{
    [SerializeField] private bool m_healed = false;
    private NavMeshAgent m_navmeshAgent;
    [SerializeField] private float obstacleCheckRadius = 2f;

    void Start()
    {
        m_navmeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsObstacleAround())
        {
            m_navmeshAgent.isStopped = true;
        }
        else
        {
            m_navmeshAgent.isStopped = false;
        }
    }

    private bool IsObstacleAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, obstacleCheckRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<NavMeshAgent>() && collider != GetComponent<Collider>())
            {
                return true; // Obstacle detected
            }
        }

        return false; // No obstacle detected
    }

    public void SetDestination(Transform _target)
    {
        m_navmeshAgent.SetDestination(_target.position);
        m_navmeshAgent.isStopped = false;
    }
}
