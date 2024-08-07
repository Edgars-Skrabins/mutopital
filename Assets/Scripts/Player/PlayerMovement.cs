using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed;
    [SerializeField] private Rigidbody m_playerRB;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!GameManager.I.HasGameStarted() || GameManager.I.IsGamePaused() || !GameManager.I.m_isPlayerAlive) return;

        InputManager.I.GetMovementVector2Normalized();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (GameManager.I.IsGamePaused() || !GameManager.I.HasGameStarted() || !GameManager.I.m_isPlayerAlive)
        {
            return;
        }

        Vector2 movement = InputManager.I.GetMovementVector2Normalized();
        Vector3 velocity = new Vector3(movement.x, 0, movement.y) * m_moveSpeed;
        m_playerRB.velocity = velocity;

        if(velocity != Vector3.zero)
        {
            transform.forward = velocity;
        }
    }
}
