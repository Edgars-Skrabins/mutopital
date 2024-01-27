using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActions m_inputActions;
    private InputAction m_onInteractPerform;

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
    }
}
