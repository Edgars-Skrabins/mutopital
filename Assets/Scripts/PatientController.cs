using UnityEngine;
using UnityEngine.AI;

public class PatientController : MonoBehaviour
{
    [SerializeField] private bool m_isHealed;
    [SerializeField] private float m_obstacleCheckRadius = 2f;
    private NavMeshAgent m_navMeshAgent;

    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (IsObstacleAround())
        {
            m_navMeshAgent.isStopped = true;
        }
        else
        {
            m_navMeshAgent.isStopped = false;
        }
    }

    private bool IsObstacleAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_obstacleCheckRadius);

        foreach (Collider collider in colliders)
        {
            // TODO: Can use TryGetComponent here instead
            if (collider.GetComponent<NavMeshAgent>() && collider != GetComponent<Collider>())
            {
                return true; // Obstacle detected
            }
        }

        return false; // No obstacle detected
    }

    public void SetDestination(Transform _target)
    {
        m_navMeshAgent.SetDestination(_target.position);
        m_navMeshAgent.isStopped = false;
    }
}
