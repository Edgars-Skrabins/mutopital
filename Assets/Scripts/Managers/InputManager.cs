using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputActions m_inputActions;

    private InputAction m_playerInteractIA;
    private InputAction m_playerMovementIA;

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

    public event Action OnInteractPerformed;

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
    }

    private void InitializeInputEvents()
    {
        m_playerInteractIA.performed += Interact_Action;
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

    public Vector2 GetMovementVector2Normalized()
    {
        Vector2 movement = m_playerMovementIA.ReadValue<Vector2>();
        return movement;
    }
}
