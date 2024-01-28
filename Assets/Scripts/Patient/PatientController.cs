using UnityEngine;
using UnityEngine.AI;

public class PatientController : MonoBehaviour
{
    public MedBayManager m_medBayManager;
    public bool m_inTrasitionToMedBay;

    [SerializeField] private bool m_isHealed;
    [SerializeField] private float m_obstacleCheckRadius = 2f;

    private NavMeshAgent m_navMeshAgent;
    [SerializeField] private PatientStats m_stats;
    [SerializeField] private MedBayController m_freeMedBay;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(!m_isHealed && !m_inTrasitionToMedBay)
        {
            m_stats.SetPatienceLevel(m_stats.GetPatienceLevel() - Time.deltaTime);
        }

        if (!m_isHealed)
        {
            if (Vector3.Distance(transform.position, m_medBayManager.GetWaitPoint()) < .1f
                && m_medBayManager.IsMedBayAvailable())
            {
                m_inTrasitionToMedBay = true;
            }

            if (IsObstacleAround() && !m_inTrasitionToMedBay)
            {
                m_navMeshAgent.isStopped = true;
            }
            else if (!IsObstacleAround() && !m_inTrasitionToMedBay)
            {
                Wait();
            }
        }

        if (m_inTrasitionToMedBay && !IsAtMedBay())
        {
            FindFreeMedBay();

        }

        if(IsAtMedBay() && m_freeMedBay)
        {
            if (Vector3.Distance(transform.position, m_freeMedBay.transform.position) < .1f) m_navMeshAgent.isStopped = true;
        }

        if(m_isHealed || m_stats.GetPatienceLevel() <= 0)
        {
            Leave();

        }

        if(m_stats.GetPatienceLevel() <= 0)
        {
            m_stats.SetPatienceLevel(0);
        }

        m_isHealed = m_stats.IsHealed();
    }

    private bool IsObstacleAround()
    {
        Vector3 aheadPosition = transform.position + transform.forward * 2f;

        Collider[] colliders = Physics.OverlapSphere(aheadPosition, m_obstacleCheckRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<PatientController>(out PatientController _patient)
                && collider != this.GetComponent<Collider>())
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
                return true; // MedBay detected
            }
        }
        return false; // No Medbay detected
    }

    private void FindFreeMedBay()
    {
        m_freeMedBay = m_medBayManager.GetUnoccupiedMedBay();
        if (m_freeMedBay != null)
        {
            SetDestination(m_freeMedBay.transform);
            m_freeMedBay.m_IsMedbayOccupied = true;
        }
    }

    private void Wait()
    {
        m_navMeshAgent.SetDestination(m_medBayManager.GetWaitPoint());
        m_navMeshAgent.isStopped = false;
    }

    private void Leave()
    {
        m_inTrasitionToMedBay = false;
        m_navMeshAgent.SetDestination(m_medBayManager.GetExitPoint());
        m_navMeshAgent.isStopped = false;
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

    public void UpdatePatientMoveSpeedMultiplier(float _multiplier)
    {
        m_navMeshAgent.speed *= _multiplier;
    }

    public void SetPatientMutationColor(Renderer rend)
    {
        rend = GetComponent<Renderer>();
        
        Color displayColor = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f));

        rend.material.color = displayColor;
    }
}
