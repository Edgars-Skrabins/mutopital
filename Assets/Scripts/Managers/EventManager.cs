using System;

public class EventManager : Singleton<EventManager>
{
    public event Action OnGamePaused;
    public event Action OnGameUnPaused;

    public void OnGamePaused_Invoke()
    {
        OnGamePaused?.Invoke();
    }

    public void OnGameUnPaused_Invoke()
    {
        OnGameUnPaused?.Invoke();
    }
}
