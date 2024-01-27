using UnityEngine;
using UnityEngine.AI;

public class PatientController : MonoBehaviour
{
    private MedBayManager m_medBayManager;
    [SerializeField] private bool m_isHealed, m_inHealingProcess;
    [SerializeField] private float m_obstacleCheckRadius = 2f;
    private NavMeshAgent m_navMeshAgent;
    [SerializeField]private MedBayController m_freeMedBay;

    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {

        if (!m_isHealed)
        {
            m_freeMedBay = m_medBayManager.GetUnoccupiedMedBay();

            if (m_freeMedBay != null)
            {
                if(!IsAtMedBay()) FindFreeMedBay();
            }
            else
            {
                if (!m_inHealingProcess)
                {
                    Wait();
                }
            }
        }
        else
        {
            m_inHealingProcess = false;
            m_navMeshAgent.SetDestination(m_medBayManager.GetExitPoint());
            m_navMeshAgent.isStopped = false;
        }
    }
    private bool IsObstacleAround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_obstacleCheckRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<PatientController>(out PatientController _patient))
            {
                return true; // Obstacle detected
            }
        }
        return false; // No obstacle detected
    }
    private bool IsAtMedBay()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_obstacleCheckRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<MedBayController>(out MedBayController _medBay))
            {
                m_inHealingProcess = true;
                return true; // MedBay detected
            }
        }
        return false; // No Medbay detected
    }

    public void SetMedBayManager(MedBayManager _medBayManager)
    {
        m_medBayManager = _medBayManager;
    }

    public void SetDestination(Transform _target)
    {
        m_navMeshAgent.SetDestination(_target.position);
        m_navMeshAgent.isStopped = false;
    }

    private void FindFreeMedBay()
    {
        MedBayController freeMedBay = m_medBayManager.GetUnoccupiedMedBay();
        if (freeMedBay != null)
        {
            m_navMeshAgent.SetDestination(freeMedBay.transform.position);
            m_navMeshAgent.isStopped = false;
            return;
        }
    }

    private void Wait()
    {
        m_navMeshAgent.SetDestination(m_medBayManager.GetWaitPoint());

        if (IsObstacleAround())
        {
            m_navMeshAgent.isStopped = true;
        }
        else
        {
            m_navMeshAgent.isStopped = false;
        }
    }
}
