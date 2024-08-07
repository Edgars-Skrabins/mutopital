using JetBrains.Annotations;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform m_interactCenter;
    [SerializeField] private float m_interactRange;
    private float m_currentInteractableDistance;
    [CanBeNull] private Interactable m_currentInteractable;
    [CanBeNull] private Transform m_currentInteractableTF;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_currentInteractableDistance = m_interactRange;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputManager.I.OnInteractPerformed += Interact;
    }

    private void UnSubscribeEvents()
    {
        InputManager.I.OnInteractPerformed -= Interact;
    }

    private void Update()
    {
        if(GameManager.I.IsGamePaused())
        {
            return;
        }
        HandleInteract();
    }

    private void Interact()
    {
        if(GameManager.I.IsGamePaused())
        {
            return;
        }
        m_currentInteractable?.Interact();
    }

    private void HandleInteract()
    {
        const int maxColliders = 20;
        Collider[] colliders = new Collider[maxColliders];
        int size = Physics.OverlapSphereNonAlloc(m_interactCenter.position, m_interactRange, colliders);

        Interactable closestInteractable = null;
        float distanceToCurrentInteractable = m_currentInteractableDistance;

        for(int i = 0; i < size; i++)
        {
            if(!colliders[i].TryGetComponent(out Transform colliderTF) ||
               !colliders[i].TryGetComponent(out Interactable interactable))
                continue;

            float distanceToPlayer = Vector3.Distance(colliderTF.position, m_interactCenter.position);
            bool closerThanCurrentInteractable = distanceToPlayer < distanceToCurrentInteractable;
            if(closerThanCurrentInteractable)
            {
                closestInteractable = interactable;
                distanceToCurrentInteractable = distanceToPlayer;
            }
        }

        if(closestInteractable)
        {
            SetInteractable(closestInteractable, closestInteractable.transform);
        }

        UpdateInteractableDistance();
        CheckIfInteractableIsInRange();
    }

    private void UpdateInteractableDistance()
    {
        if(m_currentInteractableTF)
        {
            m_currentInteractableDistance = Vector3.Distance(m_currentInteractableTF.position, m_interactCenter.position);
            return;
        }

        ClearInteractable();
    }

    private void CheckIfInteractableIsInRange()
    {
        if(m_currentInteractableDistance > m_interactRange)
        {
            ClearInteractable();
        }
    }

    private void SetInteractable(Interactable _interactable, Transform _interactableTF)
    {
        ClearInteractable();
        m_currentInteractable = _interactable;
        m_currentInteractableTF = _interactableTF;
        m_currentInteractable?.EnableInteractGFX();
    }

    private void ClearInteractable()
    {
        m_currentInteractable?.DisableInteractGFX();
        m_currentInteractable = null;
        m_currentInteractableTF = null;
        m_currentInteractableDistance = m_interactRange;
    }
}
