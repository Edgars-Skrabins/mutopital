using UnityEngine;

public class PlayerUI : MonoBehaviour
{
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
        InputManager.I.OnPausePerformed += HandlePause;
    }

    private void UnSubscribeEvents()
    {
        InputManager.I.OnPausePerformed -= HandlePause;
    }

    private void HandlePause()
    {
        if(GameManager.I.IsGamePaused())
        {
            GameManager.I.PauseGame();
            return;
        }

        GameManager.I.UnPauseGame();
    }
}
