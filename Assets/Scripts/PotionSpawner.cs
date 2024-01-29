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
    [SerializeField] private PotionType m_potionType;

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
            Potion potion = Instantiate(m_potionPrefabs[(int)m_potionType], m_spawnTransform.position, Quaternion.identity).GetComponent<Potion>();
            if (m_potionType == PotionType.Dragon)
                potion.SetPotionName("Dragon");
            else if(m_potionType == PotionType.Eel)
                potion.SetPotionName("Eel");
            else if (m_potionType == PotionType.Snake)
                potion.SetPotionName("Snake");
            else if (m_potionType == PotionType.Taco)
                potion.SetPotionName("Taco");

            potion.transform.SetParent(m_spawnTransform);
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
