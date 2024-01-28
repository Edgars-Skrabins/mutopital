using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnGamePaused;
    public event Action OnGameUnPaused;
    public event Action<int> OnPlayerStrike;
    public event Action<int> OnScoreUpdate;

    public void OnGamePaused_Invoke()
    {
        OnGamePaused?.Invoke();
    }

    public void OnGameUnPaused_Invoke()
    {
        OnGameUnPaused?.Invoke();
    }

    public void OnPlayerStrike_Invoke(int _playerStrikeAmount)
    {
        OnPlayerStrike?.Invoke(_playerStrikeAmount);
    }

    public void OnScoreUpdate_Invoke(int _score)
    {
        OnScoreUpdate?.Invoke(_score);
    }
}
