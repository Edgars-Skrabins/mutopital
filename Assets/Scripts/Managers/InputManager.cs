using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActions m_inputActions;
    private InputAction m_playerInteractIA;

    private InputAction m_playerMovementIA;

    protected override void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
    }

    private void InitializeInputActions()
    {
        m_inputActions = new InputActions();
        m_inputActions.Enable();

        m_playerMovementIA = m_inputActions.Player.Movement;
        m_playerMovementIA.Enable();

        m_playerInteractIA = m_inputActions.Player.Interact;
        m_playerInteractIA.Enable();
    }
}
