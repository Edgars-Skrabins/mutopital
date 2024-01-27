using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform m_interactCenter;
    [SerializeField] private float m_interactRange;

    private Interactable m_currentInteractable;

    private void Update()
    {
        HandleInteract();
    }

    private void HandleInteract()
    {
        int maxColliders = 30;
        Collider[] colliders = new Collider[maxColliders];
        int size = Physics.OverlapSphereNonAlloc(m_interactCenter.position, m_interactRange, colliders);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Transform colliderTF))
            {
                float distanceToPlayer = Vector3.Distance(colliderTF.position, m_interactCenter.position);
            }
        }

        for (int i = 0; i < size; i++)
        {
            if (GetComponent<Collider>().TryGetComponent(out Transform colliderTF))
            {
                float distanceToPlayer = Vector3.Distance(colliderTF.position, m_interactCenter.position);
            }
        }
    }
}
