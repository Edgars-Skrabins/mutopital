using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnAgent
{
    public string name;
    public int id;
    public Color patientColor;
    public GameObject prefab;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private MedBayManager m_medBayManager;

    public float m_SpawnRate = 1f; //Spawn Rate in Seconds
    public int m_MaxAgentsCount = 7;

    [SerializeField] private Transform m_target;
    [SerializeField] private List<SpawnAgent> m_spawnAgentPrefabs;
    [SerializeField] private Transform m_spawnPoint;
    private float m_currentTime = 0;
    private int m_currentAgentsCount = 0;

    private void Start()
    {
        if(m_medBayManager == null)
        {
            FindObjectOfType<MedBayManager>();
        }
    }

    private void Update()
    {
        if (!GameManager.I.HasGameStarted() || GameManager.I.IsGamePaused()) return;

        m_currentAgentsCount = FindObjectsOfType<PatientController>().Length;

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

            SpawnPatient();
        }
    }

    private void SpawnPatient()
    {
        
        PatientController spawnedAgent = Instantiate(m_spawnAgentPrefabs[Random.Range(0, m_spawnAgentPrefabs.Count)].prefab,
                m_spawnPoint.position, Quaternion.identity).GetComponent<PatientController>();

        //spawnedAgent.SetPatientMutationColor(spawnedAgent.GetComponent<Renderer>());
        spawnedAgent.SetDestination(m_target);
        spawnedAgent.m_medBayManager = m_medBayManager;
    }
}
