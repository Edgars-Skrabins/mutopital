using UnityEngine;
using Random = UnityEngine.Random;

public class MutationController : MonoBehaviour
{
    [SerializeField] private Mutation[] m_mutations;

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
        m_mutations[randomNum].enabled = true;
    }
}
