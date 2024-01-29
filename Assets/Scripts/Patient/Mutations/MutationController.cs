using UnityEngine;
using Random = UnityEngine.Random;

public class MutationController : MonoBehaviour
{
    [SerializeField] private Mutation[] m_mutations;
    [SerializeField] private Mutation m_activeMutation;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        PickRandomMutation();
    }

    private void PickRandomMutation()
    {
        int randomNum = Random.Range(0, m_mutations.Length - 1);
        m_activeMutation = m_mutations[randomNum];
        m_mutations[randomNum].enabled = true;
    }

    private void RemoveActiveMutation()
    {
        m_activeMutation.enabled = false;
    }

    public void AttemptRemoveMutation(string _potionName)
    {
        if(m_activeMutation.GetMutationHealPotionName() == _potionName)
        {
            RemoveActiveMutation();
        }
        Debug.Log("Attemptin To Remove Mutation");
        // TODO: Alternative functionality for when potion is wrong
    }

    public string GetActiveMutationName()
    {
        return m_activeMutation.GetMutationHealPotionName();
    }
}
