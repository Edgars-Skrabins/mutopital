using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActions m_inputActions;
    public event Action OnInteractPerformed;
    private InputAction m_playerInteractIA;

    private InputAction m_playerMovementIA;
    public event Action OnPausePerformed;
    private InputAction m_playerPauseIA;

    protected override void Awake()
    {
        base.Awake();
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
        EventManager.I.OnGamePaused += UnHideCursor;
        EventManager.I.OnGameUnPaused += HideCursor;
    }

    private void UnSubscribeEvents()
    {
        EventManager.I.OnGamePaused -=  UnHideCursor;
        EventManager.I.OnGameUnPaused -= HideCursor;
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnHideCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Initialize()
    {
        InitializeInputActions();
        InitializeInputEvents();
    }

    private void InitializeInputActions()
    {
        m_inputActions = new InputActions();
        m_inputActions.Enable();

        m_playerMovementIA = m_inputActions.Player.Movement;
        m_playerMovementIA.Enable();

        m_playerInteractIA = m_inputActions.Player.Interact;
        m_playerInteractIA.Enable();

        m_playerPauseIA = m_inputActions.Player.Pause;
        m_playerPauseIA.Enable();
    }

    private void InitializeInputEvents()
    {
        m_playerInteractIA.performed += Interact_Action;
        m_playerPauseIA.performed += Pause_Action;
    }

    private void Interact_Action(InputAction.CallbackContext _inputCtx)
    {
        switch (_inputCtx.phase)
        {
            case InputActionPhase.Performed:
                OnInteractPerformed?.Invoke();
                break;
            default: return;
        }
    }

    private void Pause_Action(InputAction.CallbackContext _inputCtx)
    {
        switch (_inputCtx.phase)
        {
            case InputActionPhase.Performed:
                OnPausePerformed?.Invoke();
                break;
            default: return;
        }
    }

    public Vector2 GetMovementVector2Normalized()
    {
        Vector2 movement = m_playerMovementIA.ReadValue<Vector2>();
        return movement;
    }
}
