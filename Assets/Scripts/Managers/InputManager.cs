using UnityEngine;
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
        EventManager.I.OnGamePaused += HideCursor;
        EventManager.I.OnGameUnPaused += UnHideCursor;
    }

    private void UnSubscribeEvents()
    {
        EventManager.I.OnGamePaused -= HideCursor;
        EventManager.I.OnGameUnPaused -= UnHideCursor;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }

    private void UnHideCursor()
    {
        Cursor.visible = true;
    }

    private void Initialize()
    {
        HideCursor();
        InitializeInputActions();
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
