using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SpawnAgent
{
    public int id;
    public string name;

    public GameObject prefab;
}

public class SpawnManager : MonoBehaviour
{
    public float m_SpawnRate = 1f; //Spawn Rate in Seconds
    public int m_MaxAgentsCount = 5;

    [SerializeField] private Transform m_target;
    [SerializeField] private List<SpawnAgent> m_spawnAgentPrefabs;
    [SerializeField] private Transform m_spawnPoint;
    private float m_currentTime = 0;
    private int m_currentAgentsCount = 0;

    private void Update()
    {
        m_currentAgentsCount = m_spawnPoint.childCount;

        if (m_currentAgentsCount < m_MaxAgentsCount)
        {
            SpawnOverTime();
        }
    }

    private void SpawnOverTime()
    {
        m_currentTime += Time.deltaTime;

        if (m_currentTime > m_SpawnRate)
        {
            m_currentTime = 0;

            GameObject spawnedAgent = Instantiate(m_spawnAgentPrefabs[Random.Range(0, m_spawnAgentPrefabs.Count)].prefab,
                m_spawnPoint.position, Quaternion.identity);

            spawnedAgent.transform.SetParent(m_spawnPoint);

            spawnedAgent.GetComponent<NavMeshAgent>().SetDestination(m_target.position);
        }
    }
}
