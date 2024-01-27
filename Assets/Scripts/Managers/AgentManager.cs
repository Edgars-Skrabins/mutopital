using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentManager : MonoBehaviour
{
    public List<NavMeshAgent> m_currentAgents;
    [SerializeField] private Transform m_medbayTF, m_exitTF;

    private void Update()
    {
        if(m_currentAgents.Count > 0)
        {
            foreach (NavMeshAgent agent in m_currentAgents)
            {
                agent.SetDestination(m_medbayTF.position);
            }
        }
    }
}
