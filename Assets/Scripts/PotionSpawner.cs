using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    Dragon = 0,
    Eel = 1,
    Snake = 2,
    Taco = 3
}

public class PotionSpawner : MonoBehaviour
{
    [SerializeField] private Transform m_spawnTransform;
    [SerializeField] private float m_spawnRate = 3f;
    [SerializeField] private PotionType potionType;

    [SerializeField] private GameObject[] m_potionPrefabs;

    private float m_currentTime = 0;

    private void Update()
    {
        if (IsOccupied())
            return;

        SpawnOverTime();
    }

    private void SpawnOverTime()
    {
        m_currentTime += Time.deltaTime;

        if (m_currentTime > m_spawnRate)
        {
            m_currentTime = 0;

            SpawnPotion();
        }
    }

    private void SpawnPotion()
    {
        if (!IsOccupied())
        {
            Instantiate(m_potionPrefabs[(int)potionType], m_spawnTransform.position, Quaternion.identity);
        }
    }

    private bool IsOccupied()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (Collider collider in colliders)
        {
            if (collider != this.GetComponent<Collider>() && collider.CompareTag("Potion"))
            {
                return true; // Potion detected
            }
        }
        return false; // No Potion detected
    }
}
